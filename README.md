# ZombieRiot-Sharp
## PVE Human vs Zombie Bot Survival Mode for CS2

PVE Zombie Survival mode that you need to eliminate all zombies in each day to get to the next level.

Plugin based on the forum post (CS:GO Version): https://forums.alliedmods.net/showthread.php?t=335402
Plugin update based on original: https://forums.alliedmods.net/showthread.php?p=647040  

Rebase from GitHub (CS:GO Version): https://github.com/Oz-Lin/ZombieSharp

### Requirements
- [Metamode:Source](https://www.sourcemm.net/downloads.php/?branch=master) Dev build (2.x).
- [CounterStrikeSharp](https://github.com/roflmuffin/CounterStrikeSharp) at least API 200+
- [CS2Fixes](https://github.com/Source2ZE/CS2Fixes) plugin for movement unlocker (knockback), weapon commands, infinite reserve ammo settings (optional), enforcing navigation mesh for bots.
- [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json/releases) 
- [Dual Mounting](https://github.com/Source2ZE/MultiAddonManager) for custom contents (especially zombie player models).

### Other recommended plugins
- [Custom Default Ammo](https://github.com/1Mack/CS2-CustomDefaultAmmo) overriding default ammo configs.
- [HNS Freeze](https://github.com/lhunter3/HNS-Freeze) turn a decoy into freeze grenade.

### Recommended Server-side Convars
```
bot_quota_mode "normal" 
bot_difficulty 3 //avoid camping zombies
mp_disconnect_kills_players 1 //avoid potential server crashes
```
 
To be migrated from CS:GO SourceMod 1.11 to CS2 CounterStrikeSharp:
- ~~Completely new syntax (Supported SM 1.11+)~~ C# .NET 8.0 standard
- ~~Replaced the old function with Sourcemod API~~ CounterStrikeSharp API 200+
- ~~Add CS:GO support~~ Migrate to CS2
- Specific max Zombie spawn in that day
- Spawn Protection
- Dynamic Difficulty (Experimental)
- Human Class
- ~~Volume Control~~ Have to use `snd_musicvolume <0.0-1.0>` in console for CS2
- API for Day Data and Zombie Count (NEW)
- API for Zombie Healthboost (NEW)

In progress
