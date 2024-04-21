using CounterStrikeSharp.API.Core;
using System;

namespace ZombieRiot_Sharp;



[MinimumApiVersion(200)]
public partial class ZombieRiotSharp : BasePlugin
{
    public override string ModuleName => "Zombie Riot Sharp";

    public override string ModuleAuthor => "Oz-Lin";

    public override string ModuleVersion => "0.0.1";

    public override string ModuleDescription => "PVE Human vs Zombie Bot Survival Mode for CS2";

    public override void Load(bool hotReload)
    {
        Console.WriteLine("Hello World!");
    }
}