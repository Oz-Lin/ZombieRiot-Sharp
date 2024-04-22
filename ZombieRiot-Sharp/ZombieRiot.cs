using System.Reflection.Metadata;

namespace ZombieRiot_Sharp;


public partial class ZombieRiotSharp
{

    #define Target_Bombed							0		// Target Successfully Bombed!
    #define VIP_Escaped								1		// The VIP has escaped!
    #define VIP_Assassinated						2		// VIP has been assassinated!
    #define Terrorists_Escaped						3		// The terrorists have escaped!
    #define CTs_PreventEscape						4		// The CT's have prevented most of the terrorists from escaping!
    #define Escaping_Terrorists_Neutralized			5		// Escaping terrorists have all been neutralized!
    #define Bomb_Defused							6		// The bomb has been defused!
    #define CTs_Win									7		// Counter-Terrorists Win!
    #define Terrorists_Win							8		// Terrorists Win!
    #define Round_Draw								9		// Round Draw!
    #define All_Hostages_Rescued					10		// All Hostages have been rescued!
    #define Target_Saved							11		// Target has been saved!
    #define Hostages_Not_Rescued					12		// Hostages have not been rescued!
    #define Terrorists_Not_Escaped					13		// Terrorists have not escaped!
    #define VIP_Not_Escaped							14		// VIP has not escaped!
    #define Game_Commencing							15		// Game Commencing!

    #define DXLEVEL_MIN 90

    #define DEFAULT_FOV 90
    #define DEFAULT_GRAVITY 1.0
    #define MAXZOMBIES 25
    #define MAXDAYS 25


    bool market;

    char gMapConfig[PLATFORM_MAX_PATH];

    int gDay;
    int dCount;

    int gZombieTeam;
    int gHumanTeam;

    int gZombiesKilled;

    int dxLevel[MAXPLAYERS + 1];

    bool bZombie[MAXPLAYERS + 1];
    Handle trieDeaths = INVALID_HANDLE;

    int gRespawnTime[MAXPLAYERS + 1];
    Handle tRespawn[MAXPLAYERS + 1];
    Handle tZVision[MAXPLAYERS + 1];

    bool bZVision[MAXPLAYERS + 1];

    Handle tHUD = INVALID_HANDLE;
    Handle tFreeze = INVALID_HANDLE;

    QueryCookie mat_dxlevel;

    int g_iDayleft;
    int g_iDayMapvote;

    bool g_bTriggerMapVote;
    bool g_bAlreadyVoted;

    int g_activeratio = 1;

    int g_iCountdown[MAXPLAYERS + 1];
    bool g_bClientProtected[MAXPLAYERS + 1] = { false, ...};
    Handle g_fClientProtectTimer[MAXPLAYERS + 1] = { INVALID_HANDLE, ...};
    bool g_bRoundStart = true;

    // Client Pref

    Handle g_hHumanClassCookie = INVALID_HANDLE;
    int g_iSelectedClass[MAXPLAYERS + 1];
    int gZombieID[MAXPLAYERS + 1];

    Handle g_hZombieVolume = INVALID_HANDLE;
    Handle g_hAmbienceVolume = INVALID_HANDLE;
    float g_fZombieVolume[MAXPLAYERS + 1];
    float g_fAmbienceVolume[MAXPLAYERS + 1];



    bool g_bAllowHumanRespawn;

    enum struct ZRiot_DayData
    {
        char data_display[32];
        char data_zombieoverride[32 * MAXZOMBIES];
        char data_storyline[192];
        int data_count;
        float data_count_ratio;
        int data_healthboost;
        float data_hp_ratio;
        bool data_respawn;
        char data_deaths_before_zombie;
        float data_fademin;
        float data_fademax;
        int data_maxzm;
        float data_maxzm_ratio;
    }



    ZRiot_DayData arrayDays[MAXDAYS];

    void FindClientDXLevel(int client)
    {
        if (IsFakeClient(client))
        {
            return;
        }

        mat_dxlevel = QueryClientConVar(client, "mat_dxlevel", DXLevelClientQuery);
    }

    public void DXLevelClientQuery(QueryCookie cookie, int client, ConVarQueryResult result, char[] cvarName, char[] cvarValue)
    {
        if (cookie != mat_dxlevel)
        {
            return;
        }

        dxLevel[client] = 0;

        if (result != ConVarQuery_Okay)
        {
            return;
        }

        dxLevel[client] = StringToInt(cvarValue);
    }

    void DisplayClientOverlay(int client, char[] overlay)
    {
        if (!dxLevel[client])
        {
            FindClientDXLevel(client);

            return;
        }

        if (dxLevel[client] >= DXLEVEL_MIN)
        {
            ClientCommand(client, "r_screenoverlay \"%s\"", overlay);
        }
        else
        {
            ZRiot_PrintCenterText(client, "DX90 not supported", dxLevel[client], DXLEVEL_MIN);
        }
    }

    void GotoNextMap()
    {
        Handle timelimit = FindConVar("mp_timelimit");

        if (timelimit == INVALID_HANDLE)
        {
            return;
        }

        int flags = GetConVarFlags(timelimit) & FCVAR_NOTIFY;
        SetConVarFlags(timelimit, flags);

        SetConVarInt(timelimit, 1);
    }
}

