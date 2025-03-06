# Stat Manager Mod

A mod that allows you to customize player statistics and upgrades in-game through configuration values.

## Features

- Modify multiple player stats through easy configuration
- Configure upgrade multipliers for:
  - Health
  - Sprint Speed
  - Map Player Count
  - Energy (Stamina)
  - Extra Jump
  - Grab Range
  - Grab Strength
  - Grab Throw Strength
  - Tumble Launch

## Installation

1. Download and install [BepInEx](https://thunderstore.io/c/repo/p/BepInEx/BepInExPack/)
2. Place the `StatManager.dll` file in the `BepInEx/plugins` directory
3. Launch the game once to generate the configuration file
4. Edit the configuration file to customize your stats

## Configuration

After the first run, a configuration file will be generated at `BepInEx/config/Bocon.StatManager.cfg`. You can edit this file to change the mod settings.

### Configuration Options

All values represent the number of times each upgrade should be applied. Default values are shown below:

```ini
[Stats]
## Amount of Health upgrades.
# Setting type: Int32
# Default value: 10
Health = 10

## Amount of Sprint Speed upgrades.
# Setting type: Int32
# Default value: 5
Sprint Speed = 5

## Amount of Player Map Count upgrades.
# Setting type: Int32
# Default value: 1
Player Map Count = 1

## Amount of Energy upgrades.
# Setting type: Int32
# Default value: 10
Energy = 10

## Amount of Extra Jump upgrades.
# Setting type: Int32
# Default value: 1
Extra Jump = 1

## Amount of Grab Range upgrades.
# Setting type: Int32
# Default value: 5
Grab Range = 5

## Amount of Grab Strength upgrades.
# Setting type: Int32
# Default value: 5
Grab Strength = 5

## Amount of Grab Throw upgrades.
# Setting type: Int32
# Default value: 5
Grab Throw = 5

## Amount of Tumble Launch upgrades.
# Setting type: Int32
# Default value: 5
Tumble Launch = 5
```

## Preview

![Stat Manager Preview](https://cdn.bocon.wtf/u/AL5F7rmZ.png)

See it in action:
![Gameplay Preview](https://cdn.bocon.wtf/u/yJ0pE4DW.gif)

## Dependencies

- BepInEx 5.4.21 or later