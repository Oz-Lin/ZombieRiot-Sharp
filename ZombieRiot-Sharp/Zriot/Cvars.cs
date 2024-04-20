using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CounterStrikeSharp.API.Modules.Cvars;
using CounterStrikeSharp.API.Modules.Cvars.Validators;
using Microsoft.Extensions.Logging;

namespace ZombieRiot_Sharp.Zriot;

public partial class Cvars
{

    public enum ZRiotSettings
    {
        public FakeConVar<bool> CVAR_ENABLE;
        public FakeConVar<bool> CVAR_AMBIENCE;
        public FakeConVar<string> CVAR_AMBIENCE_FILE;
        public FakeConVar<float> CVAR_AMBIENCE_LENGTH;
        public FakeConVar<float> CVAR_AMBIENCE_VOLUME;
        public FakeConVar<int> CVAR_HOSTNAME_UPDATE;
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
        public FakeConVar<int> CVAR_DYNAMIC_ENABLE;
        public FakeConVar<int> CVAR_DYNAMIC_PLAYERRATIO;
        public FakeConVar<bool> CVAR_ALLOWHUMAN_RESPAWN;
        public FakeConVar<int> CVAR_DAYS_POSITION;
    }

    ZRiotSettings g_cvars;


    void CreateCvars()
    {
        g_cvars.CVAR_ENABLE = new("zriot_enable", "Enable ZombieRiot gameplay (0: Disable)", true, ConVarFlags.FCVAR_NONE);
        g_cvars.CVAR_AMBIENCE = new("zriot_ambience", "1", "Enable creepy ambience to be played throughout the game (0: Disable)");
        g_cvars.CVAR_AMBIENCE_FILE = new("zriot_ambience_file", "ambient/zr/zr_ambience.mp3", "Path to ambient sound file that will be played throughout the game, when zriot_ambience is 1");
        g_cvars.CVAR_AMBIENCE_LENGTH = new("zriot_ambience_length", "60.0", "The length, in seconds, of the ambient sound file");
        g_cvars.CVAR_AMBIENCE_VOLUME = new("zriot_ambience_volume", "0.6", "Volume of ambient sounds when zriot_ambience is 1 (0.0: Unhearable,  1.0: Max volume)");
        g_cvars.CVAR_HOSTNAME_UPDATE = new("zriot_hostname_update", "1", "Updates the server's hostname to display the current day server is playing (0: Disable)");
        g_cvars.CVAR_ZVISION_REDISPLAY = new("zriot_zvision_redisplay", "0.2", "Frequency, in seconds, to display zvision on the zombies' screen (Never go below 0.1, 0.2 seems safe)");
        g_cvars.CVAR_ZVISION_ALLOW_DISABLE = new("zriot_zvision_allow_disable", "1", "Allow users to disable ZVision with their nightvision key (0: Disable)");
        g_cvars.CVAR_REGRESSION = new("zriot_regression", "1", "If the zombies win the round, the game will regress one day (0: Disable)");
        g_cvars.CVAR_NOBLOCK = new("zriot_noblock", "1", "Prevents zombies from getting stuck in each other (0: Disable)");
        g_cvars.CVAR_FREEZE = new("zriot_freeze", "10", "Time, in seconds, to freeze zombies at round start to allow humans to get set up (0: Disable)");
        g_cvars.CVAR_BOTQUOTA_SILENT = new("zriot_botquota_silent", "1", "Blocks join/leave text for bots (0: Disable)");
        g_cvars.CVAR_FIRST_RESPAWN = new("zriot_first_respawn", "10", "Amount of time to wait before spawning a player into the game for the first time (0: Disable)");
        g_cvars.CVAR_RESPAWN = new("zriot_respawn", "30", "Amount of time each human has to wait before they will respawn into the game (0: Disable)");
        g_cvars.CVAR_ZOMBIETEAM = new("zriot_zombieteam", "t", "Which team zombie's will be on (t: Terrorist ct: Counter-Terrorist)");
        g_cvars.CVAR_ZOMBIEMAX = new("zriot_zombiemax", "12", "The max amount of zombies spawned at one time");
        g_cvars.CVAR_HUD = new("zriot_hud", "1", "Enable persistent display of the HUD which displays day, zombies left, and humans left (0: Disable)");
        g_cvars.CVAR_TARGETING = new("zriot_targeting", "1", "Enables a system that tracks damage done to each zombie, and shows you each one's current health (0: Disable)");
        g_cvars.CVAR_ROUNDFADE = new("zriot_roundfade", "0", "Player's screens fade blue if humans win, red if zombies in, and black in any other case (0: Disable)");
        g_cvars.CVAR_OVERLAYS = new("zriot_overlays", "1", "Enable use of round end overlays to show the winner (0: Disable)");
        g_cvars.CVAR_OVERLAYS_ZOMBIE = new("zriot_overlays_zombie", "overlays/zr/zombies_win", "Path to overlay shown when Zombies win, when zriot_overlays is 1");
        g_cvars.CVAR_OVERLAYS_HUMAN = new("zriot_overlays_human", "overlays/zr/humans_win", "Path to overlay shown when Humans win, when zriot_overlays is 1");
        g_cvars.CVAR_RAGDOLL_REMOVE = new("zriot_ragdoll_remove", "20", "The time, in seconds, before the ragdoll of dead zombies will be deleted (0: Disable)");
        g_cvars.CVAR_NAPALM = new("zriot_napalm", "1", "Turns grenades into napalm grenades that light zombies on fire (0: Disable)");
        g_cvars.CVAR_NAPALM_TIME = new("zriot_napalm_time", "20", "How long the zombie burns when zr_napalm is 1");
        g_cvars.CVAR_DARK = new("zriot_dark", "0", "Darkens the map (0: Disable)");
        g_cvars.CVAR_DARK_LEVEL = new("zriot_dark_level", "a", "The darkness of the map,  a being the darkest,  z being extremely bright when zriot_dark is 1 (n: Default)");
        g_cvars.CVAR_DARK_SKY = new("zriot_dark_sky", "sky_borealis01", "The sky the map will have when zriot_dark is 1");
        g_cvars.CVAR_ZMARKET_BUYZONE = new("zriot_zmarket_buyzone", "0", "Must be in buyzone to access !zmarket, if Market is installed (0: Can be used anywhere)");
        g_cvars.CVAR_CASHFILL = new("zriot_cashfill", "1", "Enable the mod to set the players cash to zriot_cashamount (0: Disabled)");
        g_cvars.CVAR_CASHAMOUNT = new("zriot_cashamount", "12000", "How much money players will have when they spawn when zriot_cashfill is 1");
        g_cvars.CVAR_TRIGGERMAPVOTE = new("zriot_triggermapvote", "1.0", "Trigger the mapvote before the map end for Zriot mode (0: Disable)");
        g_cvars.CVAR_MAPVOTEDAYLEFT = new("zriot_mapvotedayleft", "2.0", "How many day left before map vote start");
        g_cvars.CVAR_INSTANTMAPCHANGE = new("zriot_instantmapchange", "1.0", "Change map instantly after winning all days");
        g_cvars.CVAR_SPAWNPROTECT = new("zriot_spawnprotect", "1.0", "Enable Spawn Protection for human or not (0: Disable)");
        g_cvars.CVAR_SPAWNPROTECTTIME = new("zriot_spawnprotecttime", "7.0", "How long that client will get immune from every damage before back to normal again");
        g_cvars.CVAR_SPAWNPROTECTSPEED = new("zriot_spawnprotectspeed", "400.0", "How much speed that client will get after spawn before back to normal");
        g_cvars.CVAR_DYNAMIC_ENABLE = new("zriot_dynamic_enable", "0.0", "Enable Dyanmic Zombie mode (Difficulty Dynamic) WARNING: This is still unstable.");
        g_cvars.CVAR_DYNAMIC_PLAYERRATIO = new("zriot_dynamic_playerratio", "10.0", "Every n player the zombie will get buffed");
        g_cvars.CVAR_ALLOWHUMAN_RESPAWN = new("zriot_allowhuman_respawn", "1.0", "Allow Human To Respawn in that map or not.");
        g_cvars.CVAR_DAYS_POSITION = new("zriot_days_position", "1", "The position of days in the hostname, 1 = After the hostname, 2 = Before the hostname.");

        HookConVarChange(g_cvars.CVAR_ENABLE, EnableHook);
    
        HookConVarChange(g_cvars.CVAR_ALLOWHUMAN_RESPAWN, OnHumanRespawnChanged);

        AutoExecConfig(true, "zombieriot", "sourcemod/zombieriot");
    }

    public void OnHumanRespawnChanged(Handle cvar, const char[] oldvalue, const char[] newvalue)
    {
        g_bAllowHumanRespawn = GetConVarBool(gCvars.CVAR_ALLOWHUMAN_RESPAWN)
    }


    void HookCvars()
    {
        SetConVarBool(FindConVar("mp_autoteambalance"), false);
        SetConVarInt(FindConVar("mp_limitteams"), 0);

        HookConVarChange(FindConVar("mp_autoteambalance"), AutoTeamBalanceHook);
        HookConVarChange(FindConVar("mp_limitteams"), LimitTeamsHook);

        HookConVarChange(gCvars.CVAR_ZOMBIETEAM, ZombieTeamHook);

        HookConVarChange(FindConVar("mp_restartgame"), RestartGameHook);
    }

    void UnhookCvars()
    {
        UnhookConVarChange(FindConVar("mp_autoteambalance"), AutoTeamBalanceHook);
        UnhookConVarChange(FindConVar("mp_limitteams"), LimitTeamsHook);

        UnhookConVarChange(gCvars.CVAR_ZOMBIETEAM, ZombieTeamHook);

        UnhookConVarChange(FindConVar("mp_restartgame"), RestartGameHook);
    }

    public void EnableHook(Handle convar, const char[] oldValue, const char[] newValue)
    {
        bool enable = view_as<bool>(StringToInt(newValue));

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

    public void AutoTeamBalanceHook(Handle convar, const char[] oldValue, const char[] newValue)
    {
        SetConVarBool(convar, false);
    }

    public void LimitTeamsHook(Handle convar, const char[] oldValue, const char[] newValue)
    {
        SetConVarInt(convar, 0);
    }

    public void ZombieTeamHook(Handle convar, const char[] oldValue, const char[] newValue)
    {
        if (StrEqual(newValue, "t", false) || StrEqual(newValue, "ct", false))
        {
            UpdateTeams();
        }
    }

    public void RestartGameHook(Handle convar, const char[] oldValue, const char[] newValue)
    {
        gDay = 0;

        ResetZombies(true);
    }

}
