namespace ZombieRiot_Sharp.Zriot;

public partial class Command
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
    
    }

    [RequiresPermissions(@"css/generic")]
    private void Command_UnRestrict(CCSPlayerController client, CommandInfo info)
    {

    }

    [RequiresPermissions(@"css/generic")]
    private void Command_SetDay(CCSPlayerController client, CommandInfo info)
    {

    }

    [RequiresPermissions(@"css/generic")]
    private void Command_Zombie(CCSPlayerController client, CommandInfo info)
    {

    }

    [RequiresPermissions(@"css/generic")]
    private void Command_Human(CCSPlayerController client, CommandInfo info)
    {

    }

    [RequiresPermissions(@"css/config")]
    private void Command_SetCount(CCSPlayerController client, CommandInfo info)
    {

    }

    [RequiresPermissions(@"css/config")]
    private void Command_SetHealthBoost(CCSPlayerController client, CommandInfo info)
    {

    }

}