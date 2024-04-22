using CounterStrikeSharp.API.Modules.Entities;
using CounterStrikeSharp.API.Modules.Events;

using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Cvars;

using CounterStrikeSharp.API.Modules.Utils;


namespace ZombieRiot_Sharp;

public partial class ZombieRiotSharp
{
    public void CreateCommands()
    {
        AddCommand("css_zriot_restrict", "Restrict a specified weapon", Command_Restrict);
        AddCommand("css_zriot_unrestrict", "Unrestrict a specified weapon", Command_UnRestrict);

        AddCommand("css_zriot_setday", "Sets the game to a certain day", Command_SetDay);

        AddCommand("css_zriot_zombie", "Turns player into zombie", Command_Zombie);
        AddCommand("css_zriot_human", "Turns player into human", Command_Human);

        AddCommand("css_zriot_setcount", "Sets Zombie count of current day", Command_SetCount);
        AddCommand("css_zriot_sethealthboost", "Sets additional HP of zombies of current day", Command_SetHealthBoost);
    }

    [RequiresPermissions(@"css/generic")]
    private void Command_Restrict(CCSPlayerController client, CommandInfo info) 
    {
        bool enabled = GetConVarBool(gCvars.CVAR_ENABLE);
        if (info.ArgCount < 1 || !enabled)
        {
            return;
        }

        char arg1[32];
        GetCmdArg(1, arg1, sizeof(arg1));

        WepRestrictQuery output = RestrictWeapon(arg1);

        if (output == Existing)
        {
            ZRiot_ReplyToCommand(client, "Weapon already restricted", arg1);
        }
        return;
    }

    [RequiresPermissions(@"css/generic")]
    private void Command_UnRestrict(CCSPlayerController client, CommandInfo info)
    {
        bool enabled = GetConVarBool(gCvars.CVAR_ENABLE);
        if (info.ArgCount < 1 || !enabled)
        {
            return;
        }

        char arg1[32];
        GetCmdArg(1, arg1, sizeof(arg1));

        WepRestrictQuery output = UnRestrictWeapon(arg1);

        if (output == Invalid)
        {
            ZRiot_ReplyToCommand(client, "Weapon invalid", arg1);
        }

        return;
    }

    [RequiresPermissions(@"css/generic")]
    private void Command_SetDay(CCSPlayerController client, CommandInfo info)
    {
        bool enabled = GetConVarBool(gCvars.CVAR_ENABLE);
        if (info.ArgCount < 1 || !enabled)
        {
            return;
        }

        char arg1[32];
        GetCmdArg(1, arg1, sizeof(arg1));

        int day = StringToInt(arg1) - 1;
        day = (day >= 0) ? day : 0;

        gDay = day;
        gDay = (gDay + 1 > dCount) ? dCount - 1 : gDay;

        ZRiot_PrintToChat(0, "Skip to day", gDay + 1);

        if (tHUD == INVALID_HANDLE)
        {
            return;
        }

        CS_TerminateRound(3.0, CSRoundEnd_Draw, true);

        return;
    }

    [RequiresPermissions(@"css/generic")]
    private void Command_Zombie(CCSPlayerController client, CommandInfo info)
    {
        bool enabled = GetConVarBool(gCvars.CVAR_ENABLE);
        if (info.ArgCount < 1 || !enabled)
        {
            return;
        }

        char arg1[32];
        GetCmdArg(1, arg1, sizeof(arg1));

        char target_name[MAX_TARGET_LENGTH];
        int targets[MAXPLAYERS];
        bool tn_is_ml;

        int tcount = ProcessTargetString(arg1, client, targets, MAXPLAYERS, COMMAND_FILTER_NO_BOTS, target_name, sizeof(target_name), tn_is_ml);
        if (tcount <= 0)
        {
            ReplyToTargetError(client, tcount);
            return;
        }

        for (int x = 0; x < tcount; x++)
        {
            ZRiot_Zombie(targets[x]);
        }

        if (GetLiveHumanCount() <= 0)
        {
            CS_TerminateRound(5.0, CSRoundEnd_TerroristWin, true);

            int score = CS_GetTeamScore(CS_TEAM_T);
            CS_SetTeamScore(CS_TEAM_T, score++);
        }

        return;
    }

    [RequiresPermissions(@"css/generic")]
    private void Command_Human(CCSPlayerController client, CommandInfo info)
    {
        bool enabled = GetConVarBool(gCvars.CVAR_ENABLE);
        if (info.ArgCount < 1 || !enabled)
        {
            return;
        }

        char arg1[32];
        GetCmdArg(1, arg1, sizeof(arg1));

        char target_name[MAX_TARGET_LENGTH];
        int targets[MAXPLAYERS];
        bool tn_is_ml;

        int tcount = ProcessTargetString(arg1, client, targets, MAXPLAYERS, COMMAND_FILTER_NO_BOTS, target_name, sizeof(target_name), tn_is_ml);
        if (tcount <= 0)
        {
            ReplyToTargetError(client, tcount);
            return;
        }

        for (int x = 0; x < tcount; x++)
        {
            ZRiot_Human(targets[x]);
        }

        return;
    }

    [RequiresPermissions(@"css/config")]
    private void Command_SetCount(CCSPlayerController client, CommandInfo info)
    {
        bool enabled = GetConVarBool(gCvars.CVAR_ENABLE);
        if (info.ArgCount < 1 || !enabled)
        {
            return;
        }

        char sArg1[16];

        GetCmdArg(1, sArg1, sizeof(sArg1));

        int count = StringToInt(sArg1);

        if (count <= 0)
        {
            ZRiot_ReplyToCommand(client, "Usage Zombie Count");
            return;
        }

        ZRiot_PrintToChat(0, "Set Zombie Count", count);
        arrayDays[gDay].data_count = count;

        return;
    }

    [RequiresPermissions(@"css/config")]
    private void Command_SetHealthBoost(CCSPlayerController client, CommandInfo info)
    {
        bool enabled = GetConVarBool(gCvars.CVAR_ENABLE);
        if (info.ArgCount < 1 || !enabled)
        {
            return;
        }

        char sArg1[16];

        GetCmdArg(1, sArg1, sizeof(sArg1));

        int hboost = StringToInt(sArg1);

        if (hboost < 0)
        {
            ZRiot_ReplyToCommand(client, "Usage Zombie Healthboost");
            return;
        }

        ZRiot_PrintToChat(0, "Set Zombie Healthboost", hboost);
        arrayDays[gDay].data_healthboost = hboost;

        return;
    }

}