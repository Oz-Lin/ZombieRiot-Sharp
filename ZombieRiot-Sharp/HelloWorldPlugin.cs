using CounterStrikeSharp.API.Core;
using System;

namespace ZombieRiot_Sharp;

public class HelloWorldPlugin : BasePlugin
{
    public override string ModuleName => "Hello World Plugin";

    public override string ModuleVersion => "0.0.1";

    public override void Load(bool hotReload)
    {
        Console.WriteLine("Hello World!");
    }
}