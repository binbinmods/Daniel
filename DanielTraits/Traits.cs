using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using Obeliskial_Content;
using UnityEngine;
using static Daniel.CustomFunctions;
using static Daniel.Plugin;


namespace Daniel
{
    [HarmonyPatch]
    internal class Traits
    {
        // list of your trait IDs
        public static string heroName = "<heroName>";

        public static string subclassname = "redeemer";

        public static List<string> simpleTraitList = ["trait0", "trait1a", "trait1b", "trait2a", "trait2b", "trait3a", "trait3b", "trait4a", "trait4b"];

        public static List<string> myTraitList = (List<string>)simpleTraitList.Select(trait => subclassName + trait); // Needs testing

        
        static string trait0 = myTraitList[0];
        static string trait2a = myTraitList[3];
        static string trait2b = myTraitList[4];
        static string trait4a = myTraitList[7];
        static string trait4b = myTraitList[8];


        public static string debugBase = "Binbin - Testing " + heroName + " ";

        public static void DoCustomTrait(string _trait, ref Trait __instance)
        {
            // get info you may need
            Enums.EventActivation _theEvent = Traverse.Create(__instance).Field("theEvent").GetValue<Enums.EventActivation>();
            Character _character = Traverse.Create(__instance).Field("character").GetValue<Character>();
            Character _target = Traverse.Create(__instance).Field("target").GetValue<Character>();
            int _auxInt = Traverse.Create(__instance).Field("auxInt").GetValue<int>();
            string _auxString = Traverse.Create(__instance).Field("auxString").GetValue<string>();
            CardData _castedCard = Traverse.Create(__instance).Field("castedCard").GetValue<CardData>();
            Traverse.Create(__instance).Field("character").SetValue(_character);
            Traverse.Create(__instance).Field("target").SetValue(_target);
            Traverse.Create(__instance).Field("theEvent").SetValue(_theEvent);
            Traverse.Create(__instance).Field("auxInt").SetValue(_auxInt);
            Traverse.Create(__instance).Field("auxString").SetValue(_auxString);
            Traverse.Create(__instance).Field("castedCard").SetValue(_castedCard);
            TraitData traitData = Globals.Instance.GetTraitData(_trait);
            List<CardData> cardDataList = [];
            List<string> heroHand = MatchManager.Instance.GetHeroHand(_character.HeroIndex);
            Hero[] teamHero = MatchManager.Instance.GetTeamHero();
            NPC[] teamNpc = MatchManager.Instance.GetTeamNPC();
            
            LogDebug("Testing MyTraitList");
            LogDebug(string.Join(", ", myTraitList));
            if (!IsLivingHero(_character))
            {
                return;
            }
            if (_trait == trait0)
            { // When you suffer Fire or Shadow damage, heal all heroes for 20% of that amount and gain 1 Zeal. -This heal does not gain bonuses-
                string traitName = traitData.TraitName;
                string traitId = _trait;
                            
                
                
                
            }


            else if (_trait == trait2a)
            { // When you play a Healer card, reduce the cost of the highest cost Mage card in your hand by 1 until discarded. When you play a Mage card, reduce the cost of the highest cost Healer card in your hand by 1 until discarded. (3 times/turn)
                string traitName = traitData.TraitName;
                string traitId = _trait;
                int bonusActivations = _character.HaveTrait(trait4a) ? 1 : 0;
                LogDebug($"Handling Trait {traitId}: {traitName}");
                Duality(ref _character,ref _castedCard, Enums.CardClass.Mage, Enums.CardClass.Healer, traitId, bonusActivations:bonusActivations);

            }



            else if (_trait == trait2b)
            { // When you play a "Fire Spell" card Purge 1, "Holy Spell" card gain 1 Bless, "Shadow Spell" card increase curse charges on all monsters by 10%. (6 times/turn)
                string traitName = traitData.TraitName;
                string traitId = _trait;
                LogDebug($"Handling Trait {traitId}: {traitName}");

                if (CanIncrementTraitActivations(_trait))
                {
                    if (_castedCard.HasCardType(Enums.CardType.Fire_Spell))
                    {
                        LogDebug($"Handling Trait {traitId}: Purging");

                        Character randomNpc = GetRandomCharacter(teamNpc);
                        randomNpc.DispelAuras(1);

                        IncrementTraitActivations(_trait);
                        DisplayRemainingChargesForTrait(ref _character, traitData);
                        DisplayTraitScroll(ref _character, traitData);
                    }
                    if (_castedCard.HasCardType(Enums.CardType.Holy_Spell))
                    {
                        LogDebug($"Handling Trait {traitId}: Gaining Bless");
                        _character.SetAuraTrait(_character,"bless",1);

                        IncrementTraitActivations(_trait);
                        DisplayRemainingChargesForTrait(ref _character, traitData);
                        DisplayTraitScroll(ref _character, traitData);
                    }
                    if (_castedCard.HasCardType(Enums.CardType.Shadow_Spell))
                    {
                        LogDebug($"Handling Trait {traitId}: Increasing Curses");
                        foreach(NPC npc in teamNpc)
                        {
                            if(IsLivingNPC(npc))
                            {
                                ModifyAllAurasOrCursesByPercent(10,IsAuraOrCurse.Curse,npc,_character);
                            }
                        }
                        IncrementTraitActivations(_trait);
                        DisplayRemainingChargesForTrait(ref _character, traitData);
                        DisplayTraitScroll(ref _character, traitData);
                    }
                }

            }

            else if (_trait == trait4a)
            { // Dark +2. Dark on heroes don't explode, and increase all healing received by 1% per charge. Healer Duality can be activated 4 times per turn.
                string traitName = traitData.TraitName;
                string traitId = _trait;
                // LogDebug($"Handling Trait {traitId}: {traitName}");
                // Handled in GACM

            }

            else if (_trait == trait4b)
            { // At the end of your turn, grant 2 Bless and 2 Zeal to all heroes, and transform all Dark charges on heroes into Burn charges.
                string traitName = traitData.TraitName;
                string traitId = _trait;
                LogDebug($"Handling Trait {traitId}: {traitName}");
                foreach(Hero hero in teamHero)
                {
                    if(!IsLivingHero(hero))
                    {
                        continue;
                    }
                    hero.SetAuraTrait(_character,"bless",2);
                    hero.SetAuraTrait(_character,"zeal",2);
                    int nDark = hero.GetAuraCharges("dark");
                    hero.HealAuraCurse(GetAuraCurseData("dark"));
                    hero.SetAuraTrait(_character,"burn",nDark);
                }                
            }

        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Trait), "DoTrait")]
        public static bool DoTrait(Enums.EventActivation _theEvent, string _trait, Character _character, Character _target, int _auxInt, string _auxString, CardData _castedCard, ref Trait __instance)
        {
            if ((UnityEngine.Object)MatchManager.Instance == (UnityEngine.Object)null)
                return false;
            Traverse.Create(__instance).Field("character").SetValue(_character);
            Traverse.Create(__instance).Field("target").SetValue(_target);
            Traverse.Create(__instance).Field("theEvent").SetValue(_theEvent);
            Traverse.Create(__instance).Field("auxInt").SetValue(_auxInt);
            Traverse.Create(__instance).Field("auxString").SetValue(_auxString);
            Traverse.Create(__instance).Field("castedCard").SetValue(_castedCard);
            if (Content.medsCustomTraitsSource.Contains(_trait) && myTraitList.Contains(_trait))
            {
                DoCustomTrait(_trait, ref __instance);
                return false;
            }
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(CharacterItem), nameof(CharacterItem.ScrollCombatTextDamageNew))]
        public static void ScrollCombatTextDamageNewPostfix(ref CharacterItem __instance, CastResolutionForCombatText _cast)
        {
            LogDebug("ScrollCombatTextDamageNewPostfix");
            string traitId = trait0;
            string traitName = "Repentant";
            if (MatchManager.Instance==null)
            {
                return;
            }
            
            LogDebug($"Handling Trait {traitId}: {traitName}, pre traverse");

            Hero[] teamHero = MatchManager.Instance.GetTeamHero();
            Hero _hero = Traverse.Create(__instance).Field("_hero").GetValue<Hero>();
            Enums.DamageType damageType = _cast.damageType;
            Enums.DamageType damageType2 = _cast.damageType2;

            if(damageType == Enums.DamageType.Fire || damageType == Enums.DamageType.Shadow)
            {
                int _auxInt = _cast.damage;
                LogDebug($"Handling Trait {traitId}: {traitName}: damage1");
                int healAmount = Mathf.RoundToInt(_auxInt * 0.20f);
                foreach(Hero hero in teamHero)
                {
                    if(!IsLivingHero(hero))
                    {
                        continue;
                    }
                    TraitHealHero(ref _hero, hero, healAmount,traitName);
                    hero.SetAuraTrait(_hero,"zeal",1);

                }
            }
            if(damageType2 == Enums.DamageType.Fire || damageType2 == Enums.DamageType.Shadow)
            {
                int _auxInt = _cast.damage2;
                LogDebug($"Handling Trait {traitId}: {traitName}, damage2");
                int healAmount = Mathf.RoundToInt(_auxInt * 0.20f);
                foreach(Hero hero in teamHero)
                {
                    if(!IsLivingHero(hero))
                    {
                        continue;
                    }
                    TraitHealHero(ref _hero, hero, healAmount,traitName);
                    hero.SetAuraTrait(_hero,"zeal",1);

                }
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(AtOManager), "GlobalAuraCurseModificationByTraitsAndItems")]
        public static void GlobalAuraCurseModificationByTraitsAndItemsPostfix(ref AtOManager __instance, ref AuraCurseData __result, string _type, string _acId, Character _characterCaster, Character _characterTarget)
        {
            LogInfo($"GACM {subclassName}");

            Character characterOfInterest = _type == "set" ? _characterTarget : _characterCaster;
            string traitOfInterest;
            // trait 4a: Dark on heroes don't explode, and increase all healing received by 1% per charge. Healer Duality can be activated 4 times per turn.
            switch (_acId)
            {
                case "dark":
                    traitOfInterest = trait4a;
                    if (IfCharacterHas(characterOfInterest, CharacterHas.Trait, traitOfInterest, AppliesTo.Heroes))
                    {
                        __result.ExplodeAtStacks = 0;    
                        __result.DamageTypeWhenConsumed = Enums.DamageType.None;
                        __result.DamageWhenConsumedPerCharge = 0;
                        __result.HealReceivedPercentPerStack = 1;
                    }
                    break;
            }

        }
    }
}
