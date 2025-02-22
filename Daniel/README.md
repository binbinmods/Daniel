# Daniel, the Redeemer

A hero mod, introducing Daniel, a fiend who seeks redemption for all and is capable with Shadow, Holy, and Fire magic. 

They were designed by @oracleraven. See [this post](https://discordapp.com/channels/679706811108163701/1339879716919967757) for their original idea.

This currently does not include any events or quests related to Daniel except for those that apply too all heroes or all heroes of a given class.

A couple of notes:
## Notes:
- I understand that things are going to be janky at times, and there are definitely bugs that will be worked out
- **What to do if Daniel is not unlocked:** Due to some jankiness of the way the code works, Daniel is unlocked only for the profile that is open when you launch the game (and for new profiles). So if they aren't unlocked in the correct profile, switch to that profile, close the game and re-open it and they will be unlocked. I'll fix this in the future, but most people won't notice it. You can also just use the profile editor to fix it.
- There are **no character events** for Daniel at this time beyond the ones that are available to all characters of a given class (such as pet trainers or healers being able to remove cards at Rest areas).
- Daniel's selection location (in the Hero Selection screen) is intentionally in position 5 (the far right). I have not yet automated the process of placing characters, and this is to accommodate other heroes. If you wish to change this, you can access the `Redeemer.json` file and the `OrderInList` property with whatever you wish.

This mod relies on [Obeliskial Content](https://across-the-obelisk.thunderstore.io/package/meds/Obeliskial_Content/).

<details>
<summary>Traits</summary>

### Level 1
- Repentant:	When you suffer Fire or Shadow damage, heal all heroes for 20% of that amount and gain 1 Zeal. -This heal does not gain bonuses-


### Level 2

![Atonement](https://github.com/binbinmods/Daniel/blob/main/Assets/atonement.png?raw=true)

![Infernal Fervor](https://github.com/binbinmods/Daniel/blob/main/Assets/infernalfervor.png?raw=true)

### Level 3

- Healer Duality:	When you play a Healer card, reduce the cost of the highest cost Mage card in your hand by 1 until discarded. When you play a Mage card, reduce the cost of the highest cost Healer card in your hand by 1 until discarded. (3 times/turn)
- Purgatory:	When you play a "Fire Spell" card Purge 1, "Holy Spell" card gain 1 Bless, "Shadow Spell" card increase curse charges on all monsters by 10%. (6 times/turn)

### Level 4

![Immanence](https://github.com/binbinmods/Daniel/blob/main/Assets/immanence.png?raw=true)

![Judgement Hour](https://github.com/binbinmods/Daniel/blob/main/Assets/judgementhour.png?raw=true)

### Level 5

- Dark Heaven:	Dark +2. Dark on heroes don't explode, and increase all healing received by 1% per charge. Healer Duality can be activated 4 times per turn.
- Holy Hell:	At the end of your turn, grant 2 Bless and 2 Zeal to all heroes, and transform all Dark charges on heroes into Burn charges.

</details>


## Installation (manual)

1. Install [Obeliskial Essentials](https://across-the-obelisk.thunderstore.io/package/meds/Obeliskial_Essentials/) and [Obeliskial Content](https://across-the-obelisk.thunderstore.io/package/meds/Obeliskial_Content/).
2. Click _Manual Download_ at the top of the page.
3. In Steam, right-click Across the Obelisk and select _Manage_->_Browse local files_.
4. Extract the archive into the game folder. Your _Across the Obelisk_ folder should now contain a _BepInEx_ folder and a _doorstop\_libs_ folder.
5. Run the game. If everything runs correctly, you will see this mod in the list of registered mods on the main menu.
6. Press F5 to open/close the Config Manager and F1 to show/hide mod version information.
7. Note: I am not certain about these install instructions. In the worst case, just copy _TheWiseWolf.dll_ into the _BepInEx\plugins_ folder, and the _Daniel_ folder (the one with the subfolders containing the json files) into _BepInEx\config\Obeliskial\_importing_

## Installation (automatic)

1. Download and install [Thunderstore Mod Manager](https://www.overwolf.com/app/Thunderstore-Thunderstore_Mod_Manager) or [r2modman](https://across-the-obelisk.thunderstore.io/package/ebkr/r2modman/).
2. Click **Install with Mod Manager** button on top of the page.
3. Run the game via the mod manager.

## Support

This has been updated for Across the Obelisk version 1.5.0.1.

Hope you enjoy it and if have any issues, ping me in Discord or make a post in the **modding #support-and-requests** channel of the [official Across the Obelisk Discord](https://discord.gg/across-the-obelisk-679706811108163701).

## Donation

Please do not donate to me. If you wish to support me, I would prefer it if you just gave me feedback. 