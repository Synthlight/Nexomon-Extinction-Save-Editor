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

#### Made for & tested on Steam saves. (Build version from 2020-08-29.) Other platforms are unsupported.

### Use at your own risk, keep save backups, etc. I shouldn't need to tell you this part.

I have the rest of the save file mapped out, but didn't bother adding stuff for achievemtn tracking, plot flags, etc.<br>
List of things not yet included that I may get aroud to that at some point:
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

Prerequisites
---

.Net Core/Desktop 3.1 Runtime is required to run this:
- https://dotnet.microsoft.com/download/dotnet-core/3.1
- (x64) https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-desktop-3.1.7-windows-x64-installer
- (x86) https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-desktop-3.1.7-windows-x86-installer

(Direct links may be out-of-date. Look for the "Desktop Runtime" section in the right column.)