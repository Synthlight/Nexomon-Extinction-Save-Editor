Nexomon: Extinction Save Editor
---

Fairly bare-bones. I may add more features in the future, maybe not.

You can edit:
- Inventory, add/delete items, change quantities, etc.
- Player stuff incl. location, options, costume, sex, pet, map location.
- Party monsters.
- Storage monsters.
- Can change change monster skills & cores.
- Wallet (Coins, Diamonds, Tokens).

Instructions
---

It'll ask you which file to open on startup.<br>
Ctrl+S opens the save dialog.<br>
That's basically it for the interface.

The default save location is `C:\Program Files (x86)\Steam\userdata\<user id>\1196630\remote\data-<slot>.dat`<br>
Do **NOT** bother editing the one in AppData/LocalLow. The one there seems to just be a backup and gets overwritten so ignore it.

<b>Made for & tested on Steam saves. Other platforms are supported but rely on the community to test, submit saves, etc, as I only own the Steam version.</b><br>
Supported platforms:
- Steam
- Switch
- PS4

If you have a save that breaks things, open an issue.<br>
Should support saves up to version 23.

### Use at your own risk, keep save backups, etc. I shouldn't need to tell you this part.

I have the rest of the save file mapped out, but didn't bother adding stuff for achievement tracking, plot flags, etc.<br>
List of things not yet included that I may get around to that at some point:
```
BeatenTamers                    beatenTamers;
Mining                          mining;
Rematcher                       rematcher;
Achievements                    achievements;
PermanentlyDestroyedEntities    permanentlyKilledEntities;
Dictionary<int, bool>           switches;
Dictionary<int, List<string>>   permanentlyKilledFlags;
Dictionary<string, int>         variables;
HashSet<DatabaseMonsters.Entry> seenMonsters;
HashSet<DatabaseMonsters.Entry> ownedMonsters;
List<int>                       cadiumMapsWithZieglerMiasma;
```

If you want it, there's an 010 template for the start of the save file. (Goes till wallet, same as the save editor.)<br>
[Save-Template.bt](Save-Template.bt)

Crashes
---

If there's a crash, it'll get recorded in the Windows even viewer. `eventvwr.msc`<br>
Open an issue with the crash log.

If the exe doesn't appear to do anything when run, check for crash logs there.<br>
The other common cause of this is people not extracting the files from the zip before running it. The dlls **MUST** be in the same directory for the program to work.
Running the exe from the zip will not do this and it'll fail to run.

Prerequisites
---

.Net Core/Desktop 3.1 Runtime is required to run this:
- https://dotnet.microsoft.com/download/dotnet-core/3.1
- (x64) https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-desktop-3.1.7-windows-x64-installer
- (x86) https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-desktop-3.1.7-windows-x86-installer

(Direct links may be out-of-date. Look for the "Desktop Runtime" section in the right column.)