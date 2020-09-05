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

It'll ask you which file to open on startup.<br>
Ctrl+S saves.
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