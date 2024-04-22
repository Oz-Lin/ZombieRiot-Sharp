namespace ZombieRiot_Sharp;


public partial class ZombieRiotSharp
{
    #define DEFAULT_SPEED 300.0

    void SpawnProtectOnClientSpawn(int client)
    {
        if(!IsPlayerAlive(client))
        {
            return;
        }

        if(IsPlayerZombie(client))
        {
            return;
        }

        if(g_fClientProtectTimer[client] != INVALID_HANDLE)
        {
            g_fClientProtectTimer[client] = INVALID_HANDLE;
        }

        float g_fProtectTime = GetConVarFloat(gCvars.CVAR_SPAWNPROTECTTIME);
        float g_fSpawnSpeed = GetConVarFloat(gCvars.CVAR_SPAWNPROTECTSPEED);

        g_bClientProtected[client] = true;
        SetEntProp(client, Prop_Data, "m_takedamage", 0, 1);

        if(g_fClientProtectTimer[client] == INVALID_HANDLE)
        {
            g_fClientProtectTimer[client] = CreateTimer(1.0, Timer_Protect, client, TIMER_FLAG_NO_MAPCHANGE|TIMER_REPEAT);
        }

        g_iCountdown[client] = RoundToNearest(g_fProtectTime);
        ZRiot_PrintCenterText(client, "Spawn Protect Timer", g_iCountdown[client]);
        ZRiot_PrintToChat(client, "Still Protected");

        SetPlayerSpeed(client, g_fSpawnSpeed);
    }

    void SpawnProtectOnClientDeath(int client)
    {
        if(g_fClientProtectTimer[client] != INVALID_HANDLE)
        {
            KillTimer(g_fClientProtectTimer[client]);
        }
        g_fClientProtectTimer[client] = INVALID_HANDLE;
    }

    public Action Timer_Protect(Handle timer, any client)
    {
        if(!IsClientInGame(client))
        {
            g_fClientProtectTimer[client] = INVALID_HANDLE;
            return Plugin_Stop;
        }

        if(!IsPlayerHuman(client))
        {
            g_fClientProtectTimer[client] = INVALID_HANDLE;
            return Plugin_Stop;
        }

        g_iCountdown[client]--;

        ZRiot_PrintCenterText(client, "Spawn Protect Timer", g_iCountdown[client]);

        if(g_iCountdown[client] <= 0)
        {
            g_bClientProtected[client] = false;
            SetPlayerSpeed(client, DEFAULT_SPEED);

            g_fClientProtectTimer[client] = INVALID_HANDLE;
            ZRiot_PrintCenterText(client, "No Longer Protected");
            ZRiot_PrintToChat(client, "No Longer Protected");

            SetEntProp(client, Prop_Data, "m_takedamage", 2, 1);

            return Plugin_Stop;
        }

        return Plugin_Continue;
    }


}
