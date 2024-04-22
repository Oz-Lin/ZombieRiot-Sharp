namespace ZombieRiot_Sharp;

public partial class ZombieRiotSharp
{

    Handle kvDays = INVALID_HANDLE;

    void LoadDayData(bool defaultconfig)
    {
        char path[PLATFORM_MAX_PATH];
        Format(path, sizeof(path), "%s/days.txt", gMapConfig);
    
        if (!defaultconfig && !FileExists(path))
        {
            return;
        }
    
        if (kvDays != INVALID_HANDLE)
        {
            CloseHandle(kvDays);
        }
    
        kvDays = CreateKeyValues("days");
        KvSetEscapeSequences(kvDays, true);
    
        if (!FileToKeyValues(kvDays, path))
        {
            SetFailState("\"%s\" failed to load", path);
        }
    
        KvRewind(kvDays);
        if (!KvGotoFirstSubKey(kvDays))
        {
            SetFailState("No day data defined in \"%s\"", path);
        }
    
        char display[32];
        char zombieoverride[32*MAXZOMBIES];
        char storyline[192];
    
        dCount = 0;
        do
        {
            KvGetSectionName(kvDays, display, sizeof(display));
            strcopy(arrayDays[dCount].data_display, 32, display);
        
            KvGetString(kvDays, "zombieoverride", zombieoverride, sizeof(zombieoverride));
            strcopy(arrayDays[dCount].data_zombieoverride, 32*MAXZOMBIES, zombieoverride);
        
            KvGetString(kvDays, "storyline", storyline, sizeof(storyline));
            strcopy(arrayDays[dCount].data_storyline, 192, storyline);
        
            arrayDays[dCount].data_count = KvGetNum(kvDays, "count", 25);
            arrayDays[dCount].data_count_ratio = KvGetFloat(kvDays, "count_ratio", 1.0);
            arrayDays[dCount].data_hp_ratio = KvGetFloat(kvDays, "hp_ratio", 1.0);
            arrayDays[dCount].data_healthboost = KvGetNum(kvDays, "healthboost");
            arrayDays[dCount].data_respawn = view_as<bool>(KvGetNum(kvDays, "respawn"));
            arrayDays[dCount].data_deaths_before_zombie = KvGetNum(kvDays, "deaths_before_zombie");
            arrayDays[dCount].data_fademin = KvGetFloat(kvDays, "fademin");
            arrayDays[dCount].data_fademax = KvGetFloat(kvDays, "fademax");
            arrayDays[dCount].data_maxzm = KvGetNum(kvDays, "maxzm", 0);
            arrayDays[dCount].data_maxzm_ratio = KvGetFloat(kvDays, "maxzm_ratio", 1.0);
        
            dCount++;
        } while (KvGotoNextKey(kvDays));
    }

    void GetDayDisplay(int day, char[] display, int len)
    {
        strcopy(display, len, arrayDays[day].data_display);
    }

    bool ExplodeZombieOverrides(int day)
    {
        if (adtZombies != INVALID_HANDLE)
        {
            CloseHandle(adtZombies);
            adtZombies = INVALID_HANDLE;
        }
    
        char zombieoverride[32*MAXZOMBIES];
        GetDayZombieOverride(day, zombieoverride, sizeof(zombieoverride));
        
        if (zombieoverride[0])
        {
            adtZombies = CreateArray();
            
            char sZombies[MAXZOMBIES][64];
            ExplodeString(zombieoverride, ",", sZombies, MAXZOMBIES, 64);
        
            for (int x = 0; x < MAXZOMBIES; x++)
            {
                if (!sZombies[x][0])
                    continue;
            
                TrimString(sZombies[x]);
                int zombieid = FindZombieIDByName(sZombies[x]);
            
                if (zombieid == -1)
                    continue;
            
                PushArrayCell(adtZombies, zombieid);
            }
        
            return true;
        }
    
        return false;
    }

    void GetDayZombieOverride(int day, char[] zombieoverride, int len)
    {
        strcopy(zombieoverride, len, arrayDays[day].data_zombieoverride);
    }

    void GetDayStoryLine(int day, char[] storyline, int len)
    {
        strcopy(storyline, len, arrayDays[day].data_storyline);
    }

    int GetDayCount(int day)
    {
        bool dynamic = GetConVarBool(gCvars.CVAR_DYNAMIC_ENABLE);

        if(!dynamic)
        {
            return arrayDays[day].data_count;
        }
        else
        {
            float multi = GetDayCountRatio(day);

            if(multi <= 1.0)
            {
                return arrayDays[day].data_count;
            }

            else
            {
                int total = RoundToNearest(arrayDays[day].data_count * Pow(multi, float(g_activeratio)));
                return total;
            }
        }
    }

    float GetDayCountRatio(int day)
    {
        return arrayDays[day].data_count_ratio;
    }

    float GetDayHPRatio(int day)
    {
        return arrayDays[day].data_hp_ratio;
    }

    int GetDayHealthBoost(int day)
    {
        return arrayDays[day].data_healthboost;
    }

    bool GetDayRespawn(int day)
    {
        return arrayDays[day].data_respawn;
    }

    int GetDayDeathsBeforeZombie(int day)
    {
        return arrayDays[day].data_deaths_before_zombie;
    }

    float GetDayMinFade(int day)
    {
        return arrayDays[day].data_fademin;
    }

    float GetDayMaxFade(int day)
    {
        return arrayDays[day].data_fademax;
    }

    int GetDayMaxZM(int day)
    {
        bool dynamic = GetConVarBool(gCvars.CVAR_DYNAMIC_ENABLE);

        if(!dynamic)
        {
            return arrayDays[day].data_maxzm;
        }
        else
        { 
            float multi = GetDayMaxZMRatio(day);
        
            if(multi <= 1.0)
            {
                return arrayDays[day].data_maxzm;
            }

            else
            {
                int total = RoundToNearest(arrayDays[day].data_maxzm * Pow(multi, float(g_activeratio)));
    
                if(total > 32)
                {
                    total = 32;
                }
                return total;
            }
        }
    }

    float GetDayMaxZMRatio(int day)
    {
        return arrayDays[day].data_maxzm_ratio;
    }

    void BeginDay()
    {
        gZombiesKilled = 0;
    
        int zombiecount = GetDayCount(gDay);
        int zombiemax = GetDayMaxZM(gDay);

        if (zombiemax <= 0)
        {
            zombiemax = GetConVarInt(gCvars.CVAR_ZOMBIEMAX);
        }

        int spawncount;
        bool respawn = GetDayRespawn(gDay);
    
        if (respawn)
        {
            spawncount = zombiemax;
        }
        else
        {
            spawncount = (zombiecount < zombiemax) ? zombiecount : zombiemax;
        }
    
        ServerCommand("bot_quota %d", spawncount);
    
        char display[32];
        GetDayDisplay(gDay, display, sizeof(display));
    
        bool override = ExplodeZombieOverrides(gDay);
    
        for (int x = 1; x <= MaxClients; x++)
        {
            if (!IsClientInGame(x))
            {
            continue;
            }
        
            ChangeClientDeathCount(x, 0);
        
            if (!IsPlayerZombie(x))
            {
                continue;
            }
        
            if (override)
            {
                int size = GetArraySize(adtZombies);
                if (size)
                {
                    int zombieid = GetRandomInt(0, size - 1);
                            
                    Zombify(x, GetArrayCell(adtZombies, zombieid));
                }
            }
        }
    }

}