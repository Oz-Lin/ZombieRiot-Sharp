using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Modules.Cvars;
using CounterStrikeSharp.API.Modules.Cvars.Validators;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
//using static ZombieRiot_Sharp.Zriot.Cvars;

namespace ZombieRiot_Sharp;


public partial class ZombieRiotSharp
{
    public FakeConVar<bool> CVAR_ENABLE;
    public FakeConVar<bool> CVAR_AMBIENCE;
    public FakeConVar<string> CVAR_AMBIENCE_FILE;
    public FakeConVar<float> CVAR_AMBIENCE_LENGTH;
    public FakeConVar<float> CVAR_AMBIENCE_VOLUME;
    public FakeConVar<bool> CVAR_HOSTNAME_UPDATE;
    public FakeConVar<float> CVAR_ZVISION_REDISPLAY;
    public FakeConVar<bool> CVAR_ZVISION_ALLOW_DISABLE;
    public FakeConVar<bool> CVAR_NOBLOCK;
    public FakeConVar<int> CVAR_FREEZE;
    public FakeConVar<bool> CVAR_BOTQUOTA_SILENT;
    public FakeConVar<bool> CVAR_REGRESSION;
    public FakeConVar<int> CVAR_FIRST_RESPAWN;
    public FakeConVar<int> CVAR_RESPAWN;
    public FakeConVar<string> CVAR_ZOMBIETEAM;
    public FakeConVar<int> CVAR_ZOMBIEMAX;
    public FakeConVar<bool> CVAR_HUD;
    public FakeConVar<bool> CVAR_TARGETING;
    public FakeConVar<bool> CVAR_ROUNDFADE;
    public FakeConVar<bool> CVAR_OVERLAYS;
    public FakeConVar<string> CVAR_OVERLAYS_ZOMBIE;
    public FakeConVar<string> CVAR_OVERLAYS_HUMAN;
    public FakeConVar<int> CVAR_RAGDOLL_REMOVE;
    public FakeConVar<bool> CVAR_NAPALM;
    public FakeConVar<int> CVAR_NAPALM_TIME;
    public FakeConVar<bool> CVAR_DARK;
    public FakeConVar<string> CVAR_DARK_LEVEL;
    public FakeConVar<string> CVAR_DARK_SKY;
    public FakeConVar<bool> CVAR_ZMARKET_BUYZONE;
    public FakeConVar<bool> CVAR_CASHFILL;
    public FakeConVar<int> CVAR_CASHAMOUNT;
    public FakeConVar<bool> CVAR_TRIGGERMAPVOTE;
    public FakeConVar<int> CVAR_MAPVOTEDAYLEFT;
    public FakeConVar<bool> CVAR_INSTANTMAPCHANGE;
    public FakeConVar<bool> CVAR_SPAWNPROTECT;
    public FakeConVar<int> CVAR_SPAWNPROTECTTIME;
    public FakeConVar<float> CVAR_SPAWNPROTECTSPEED;
    public FakeConVar<bool> CVAR_DYNAMIC_ENABLE;
    public FakeConVar<int> CVAR_DYNAMIC_PLAYERRATIO;
    public FakeConVar<bool> CVAR_ALLOWHUMAN_RESPAWN;
    public FakeConVar<int> CVAR_DAYS_POSITION;

    void CreateCvars()
    {
        CVAR_ENABLE = new("zriot_enable", "Enable ZombieRiot gameplay (false: Disable)", true, ConVarFlags.FCVAR_NONE);
        CVAR_AMBIENCE = new("zriot_ambience", "Enable creepy ambience to be played throughout the game (false: Disable)", true, ConVarFlags.FCVAR_NONE);
        CVAR_AMBIENCE_FILE = new("zriot_ambience_file", "Path to ambient sound file that will be played throughout the game, when zriot_ambience is 1", "ambient/zr/zr_ambience.mp3");
        CVAR_AMBIENCE_LENGTH = new("zriot_ambience_length", "The length, in seconds, of the ambient sound file", 60.0f, ConVarFlags.FCVAR_NONE);
        CVAR_AMBIENCE_VOLUME = new("zriot_ambience_volume", "Volume of ambient sounds when zriot_ambience is 1 (0.0: Unhearable,  1.0: Max volume)", 0.6f, ConVarFlags.FCVAR_NONE, new RangeValidator<float>(0.0f, 1.0f));
        CVAR_HOSTNAME_UPDATE = new("zriot_hostname_update", "Updates the server's hostname to display the current day server is playing (false: Disable)", true, ConVarFlags.FCVAR_NONE);
        CVAR_ZVISION_REDISPLAY = new("zriot_zvision_redisplay", "Frequency, in seconds, to display zvision on the zombies' screen (Never go below 0.1, 0.2 seems safe)", 0.2f, ConVarFlags.FCVAR_NONE, new RangeValidator<float>(0.0f, 1.0f));
        CVAR_ZVISION_ALLOW_DISABLE = new("zriot_zvision_allow_disable", "Allow users to disable ZVision with their nightvision key (false: Disable)", false, ConVarFlags.FCVAR_NONE);
        CVAR_REGRESSION = new("zriot_regression", "If the zombies win the round, the game will regress one day (false: Disable)", true, ConVarFlags.FCVAR_NONE);
        CVAR_NOBLOCK = new("zriot_noblock", "Prevents zombies from getting stuck in each other (false: Disable)", true, ConVarFlags.FCVAR_NONE);
        CVAR_FREEZE = new("zriot_freeze", "Time, in seconds, to freeze zombies at round start to allow humans to get set up (0: Disable)", 10, ConVarFlags.FCVAR_NONE);
        CVAR_BOTQUOTA_SILENT = new("zriot_botquota_silent", "Blocks join/leave text for bots (false: Disable)", true, ConVarFlags.FCVAR_NONE);
        CVAR_FIRST_RESPAWN = new("zriot_first_respawn","Amount of time to wait before spawning a player into the game for the first time (0: Disable)", 10, ConVarFlags.FCVAR_NONE);
        CVAR_RESPAWN = new("zriot_respawn", "Amount of time each human has to wait before they will respawn into the game (0: Disable)", 30, ConVarFlags.FCVAR_NONE);
        CVAR_ZOMBIETEAM = new("zriot_zombieteam", "Which team zombie's will be on (t: Terrorist ct: Counter-Terrorist)", "t");
        CVAR_ZOMBIEMAX = new("zriot_zombiemax", "The max amount of zombies spawned at one time", 12, ConVarFlags.FCVAR_NONE);
        CVAR_HUD = new("zriot_hud", "Enable persistent display of the HUD which displays day, zombies left, and humans left (false: Disable)", true, ConVarFlags.FCVAR_NONE);
        CVAR_TARGETING = new("zriot_targeting", "Enables a system that tracks damage done to each zombie, and shows you each one's current health (false: Disable)", true, ConVarFlags.FCVAR_NONE);
        CVAR_ROUNDFADE = new("zriot_roundfade", "Player's screens fade blue if humans win, red if zombies in, and black in any other case (false: Disable)", false, ConVarFlags.FCVAR_NONE);
        CVAR_OVERLAYS = new("zriot_overlays", "Enable use of round end overlays to show the winner (false: Disable)", true, ConVarFlags.FCVAR_NONE);
        CVAR_OVERLAYS_ZOMBIE = new("zriot_overlays_zombie", "overlays/zr/zombies_win", "Path to overlay shown when Zombies win, when zriot_overlays is 1");
        CVAR_OVERLAYS_HUMAN = new("zriot_overlays_human", "overlays/zr/humans_win", "Path to overlay shown when Humans win, when zriot_overlays is 1");
        CVAR_RAGDOLL_REMOVE = new("zriot_ragdoll_remove", "The time, in seconds, before the ragdoll of dead zombies will be deleted (0: Disable)", 20, ConVarFlags.FCVAR_NONE);
        CVAR_NAPALM = new("zriot_napalm", "Turns grenades into napalm grenades that light zombies on fire (false: Disable)", true, ConVarFlags.FCVAR_NONE);
        CVAR_NAPALM_TIME = new("zriot_napalm_time", "How long the zombie burns when zr_napalm is true", 20, ConVarFlags.FCVAR_NONE);
        CVAR_DARK = new("zriot_dark", "Darkens the map (false: Disable)", false, ConVarFlags.FCVAR_NONE);
        CVAR_DARK_LEVEL = new("zriot_dark_level", "The darkness of the map,  a being the darkest,  z being extremely bright when zriot_dark is 1 (n: Default)", "n");
        CVAR_DARK_SKY = new("zriot_dark_sky", "sky_borealis01", "The sky the map will have when zriot_dark is 1");
        CVAR_ZMARKET_BUYZONE = new("zriot_zmarket_buyzone", "Must be in buyzone to access !zmarket, if Market is installed (false: Can be used anywhere)", false, ConVarFlags.FCVAR_NONE);
        CVAR_CASHFILL = new("zriot_cashfill", "Enable the mod to set the players cash to zriot_cashamount (false: Disabled)", true, ConVarFlags.FCVAR_NONE);
        CVAR_CASHAMOUNT = new("zriot_cashamount", "How much money players will have when they spawn when zriot_cashfill is 1", 12000, ConVarFlags.FCVAR_NONE, new RangeValidator<int>(0, 99999));
        CVAR_TRIGGERMAPVOTE = new("zriot_triggermapvote", "Trigger the mapvote before the map end for Zriot mode (false: Disable)", true, ConVarFlags.FCVAR_NONE);
        CVAR_MAPVOTEDAYLEFT = new("zriot_mapvotedayleft", "How many day left before map vote start", 2, ConVarFlags.FCVAR_NONE, new RangeValidator<int>(0, 99999));
        CVAR_INSTANTMAPCHANGE = new("zriot_instantmapchange", "Change map instantly after winning all days", true, ConVarFlags.FCVAR_NONE);
        CVAR_SPAWNPROTECT = new("zriot_spawnprotect", "Enable Spawn Protection for human or not (false: Disable)", true, ConVarFlags.FCVAR_NONE);
        CVAR_SPAWNPROTECTTIME = new("zriot_spawnprotecttime", "How long that client will get immune from every damage before back to normal again", 7, ConVarFlags.FCVAR_NONE, new RangeValidator<int>(0, 99999));
        CVAR_SPAWNPROTECTSPEED = new("zriot_spawnprotectspeed", "How much speed that client will get after spawn before back to normal", 400.0f, ConVarFlags.FCVAR_NONE, new RangeValidator<float>(0f, 114514f));
        CVAR_DYNAMIC_ENABLE = new("zriot_dynamic_enable", "Enable Dyanmic Zombie mode (Difficulty Dynamic) WARNING: This is still unstable. (false: Disable)", false, ConVarFlags.FCVAR_NONE);
        CVAR_DYNAMIC_PLAYERRATIO = new("zriot_dynamic_playerratio", "Every n player the zombie will get buffed", 10, ConVarFlags.FCVAR_NONE, new RangeValidator<int>(0, 1024));
        CVAR_ALLOWHUMAN_RESPAWN = new("zriot_allowhuman_respawn", "Allow Human To Respawn in that map or not. (false: Disable)", false, ConVarFlags.FCVAR_NONE);
        CVAR_DAYS_POSITION = new("zriot_days_position", "The position of days in the hostname, 1 = After the hostname, 2 = Before the hostname.", 1, ConVarFlags.FCVAR_NONE, new RangeValidator<int>(1, 2));

        //RegisterFakeConVars(typeof(ZRiotSettings));

        //HookConVarChange(CVAR_ENABLE, EnableHook);

        //HookConVarChange(CVAR_ALLOWHUMAN_RESPAWN, OnHumanRespawnChanged);

        //AutoExecConfig(true, "zombieriot", "sourcemod/zombieriot");
    }

    public void SettingsOnLoad()
    {
        CreateCvars();
        RegisterFakeConVars(typeof(ConVar));
    }

    public bool SettingsIntialize(string mapname)
    {
        var configFolder = Path.Combine(Server.GameDirectory, "csgo/cfg/zriot/");

        if (!Directory.Exists(configFolder))
        {
            Logger.LogError($"[ZRiot] Couldn't find directory {configFolder}");
            return false;
        }

        var configPath = Path.Combine(configFolder, "zombieriot.cfg");

        if (!File.Exists(configPath))
        {
            CreateAutoExecCFG(configPath);
            Logger.LogInformation($"[ZRiot] Creating {configPath}");
        }

        Server.ExecuteCommand("exec zriot/zombieriot.cfg");

        var mapConfig = Path.Combine(configFolder, mapname + ".cfg");

        if (File.Exists(mapConfig))
        {
            Logger.LogInformation($"[ZRiot] Found Map cfg file loading {mapConfig}");
        }

        return true;
    }

    public void CreateAutoExecCFG(string path)
    {
        StreamWriter execCfg = File.CreateText(path);

        execCfg.WriteLine("zriot_enable \"true\"");
        execCfg.WriteLine("zriot_ambience \"true\"");
        execCfg.WriteLine("zriot_ambience_file \"ambient/zr/zr_ambience.mp3\"");
        execCfg.WriteLine("zriot_ambience_length \"60.0\"");
        execCfg.WriteLine("zriot_ambience_volume \"0.6\"");
        execCfg.WriteLine("zriot_hostname_update \"true\"");
        execCfg.WriteLine("zriot_zvision_redisplay \"0.2\"");

        execCfg.WriteLine("zs_respawn_timer \"5.0\"");
        execCfg.WriteLine("zs_respawn_join_late \"1\"");
        execCfg.WriteLine("zs_respawn_team \"0\"");
        execCfg.WriteLine("zs_respawn_protect \"0\"");
        execCfg.WriteLine("zs_respawn_protect_time \"5.0\"");
        execCfg.WriteLine("zs_respawn_protect_speed \"600.0\"");

        execCfg.WriteLine("zs_classes_human_default \"human_default\"");
        execCfg.WriteLine("zs_classes_zombie_default \"zombie_default\"");
        execCfg.WriteLine("zs_classes_mother_default \"motherzombie\"");

        execCfg.WriteLine("zs_repeatkiller_threshold \"0.0\"");
        execCfg.WriteLine("zs_topdefender_enable \"1\"");
        execCfg.WriteLine("zs_timeout_winner \"3\"");

        execCfg.Close();
    }

    public void OnHumanRespawnChanged(Handle cvar, string oldvalue, string newvalue)
    {
        g_bAllowHumanRespawn = GetConVarBool(gCvars.CVAR_ALLOWHUMAN_RESPAWN);
    }




    public void HookCvars()
    {
        SetConVarBool(FindConVar("mp_autoteambalance"), false);
        SetConVarInt(FindConVar("mp_limitteams"), 0);

        HookConVarChange(FindConVar("mp_autoteambalance"), AutoTeamBalanceHook);
        HookConVarChange(FindConVar("mp_limitteams"), LimitTeamsHook);

        HookConVarChange(gCvars.CVAR_ZOMBIETEAM, ZombieTeamHook);

        HookConVarChange(FindConVar("mp_restartgame"), RestartGameHook);
    }

    public void UnhookCvars()
    {
        UnhookConVarChange(FindConVar("mp_autoteambalance"), AutoTeamBalanceHook);
        UnhookConVarChange(FindConVar("mp_limitteams"), LimitTeamsHook);

        UnhookConVarChange(gCvars.CVAR_ZOMBIETEAM, ZombieTeamHook);

        UnhookConVarChange(FindConVar("mp_restartgame"), RestartGameHook);
    }

    public void EnableHook(Handle convar, string oldValue, string newValue)
    {
        bool enable = Convert.ToBoolean(newValue);

        if (enable)
        {
            FindMapSky();

            UpdateHostname();

            HookEvents();
            HookCvars();

            ServerCommand("bot_kick");

            gDay = 0;

            CS_TerminateRound(3.0, CSRoundEnd_GameStart, true);
        }
        else
        {
            ZRiotEnd();
        }
    }

    public void AutoTeamBalanceHook(Handle convar, string oldValue, string newValue)
    {
        SetConVarBool(convar, false);
    }

    public void LimitTeamsHook(Handle convar, string oldValue, string newValue)
    {
        SetConVarInt(convar, 0);
    }

    public void ZombieTeamHook(Handle convar, string oldValue, string newValue)
    {
        if (string.Equals(newValue, "t", StringComparison.OrdinalIgnoreCase) || string.Equals(newValue, "ct", StringComparison.OrdinalIgnoreCase))
        {
            UpdateTeams();
        }
    }

    public void RestartGameHook(Handle convar, string oldValue, string newValue)
    {
        gDay = 0;

        ResetZombies(true);
    }
}
