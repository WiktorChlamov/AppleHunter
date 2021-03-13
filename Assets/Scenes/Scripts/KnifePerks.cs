using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Localization;

namespace Game {
    public static class KnifePerks
    {
        public static void Perk(KnifePerk knifePerk, int level, out string discription)
        {
            LevelInst.Stage = 1;
            discription = Discription(knifePerk, level);
            UiKnivesAndScoreEvents.SetBase();
            AbilityButton.action = null;
            switch (knifePerk)
            {
                case KnifePerk.None:
                    break;
                case KnifePerk.ExtraScore:
                    UiKnivesAndScoreEvents.ExtraScore = 2 * level;
                    break;
                case KnifePerk.ExtraLife:
                    UiKnivesAndScoreEvents.Heart = 1 * level;
                    break;
                case KnifePerk.KnifeDissolve:
                    AbilitySettings(1, 0.05f * level);
                    AbilityButton.action = delegate () { Ability.ability.RemoveKnifes(level); };
                    break;
                case KnifePerk.SlowingRotate:
                    AbilitySettings(1, 0.08f * level);
                    AbilityButton.action = delegate () { Ability.ability.ReduceSpeed(2); };
                    break;
                case KnifePerk.QuickCharge:
                    UiKnivesAndScoreEvents.ExtraHealthPoints = 0.01f * level;
                    break;
                case KnifePerk.AddApples:
                    AbilitySettings(0, 0.02f * level);
                    AbilityButton.action = delegate () { Ability.ability.AddApples(level); };
                    break;
                case KnifePerk.LifeAndScoreAndRotate:
                    UiKnivesAndScoreEvents.ExtraScore = 2 * level;
                    UiKnivesAndScoreEvents.Heart = 1;
                    AbilitySettings(1, 0.04f * level);
                    AbilityButton.action = delegate () { Ability.ability.ReduceSpeed(2); };
                    break;
                case KnifePerk.ScoreAndApples:
                    UiKnivesAndScoreEvents.ExtraScore = 1 * level;
                    AbilitySettings(0, 0.015f * level);
                    AbilityButton.action = delegate () { Ability.ability.AddApples(level); };
                    break;
                case KnifePerk.TakeLifeAddApples:
                    UiKnivesAndScoreEvents.ExtraScore = 4;
                    UiKnivesAndScoreEvents.ExtraHealthPoints = 0.02f;
                    AbilitySettings(0, 1);
                    AbilityButton.action = delegate () { Ability.ability.TakeLife(); };
                    break;
                case KnifePerk.LifeScoreDiss:
                    UiKnivesAndScoreEvents.Heart = 1 * level;
                    UiKnivesAndScoreEvents.ExtraScore = 2 * level;
                    AbilitySettings(1, 0.05f * level);
                    AbilityButton.action = delegate () { Ability.ability.RemoveKnifes(level); };
                    break;
                case KnifePerk.LifeChargeRotateStage:
                    UiKnivesAndScoreEvents.Heart = 1 * level;
                    UiKnivesAndScoreEvents.ExtraHealthPoints = 0.01f * level;
                    AbilitySettings(1, 0.02f * level);
                    AbilityButton.action = delegate () { Ability.ability.ReduceSpeed(1.5f); };
                    LevelInst.Stage = 51;
                    break;
            }
        }
        public static string Discription(KnifePerk knifePerk, int level)
        {
            switch (knifePerk)
            {
                case KnifePerk.None:
                    return "";
                case KnifePerk.ExtraScore:
                    return $"{Translate((int)PerkLoc.Points)}{2 * level}";
                case KnifePerk.ExtraLife:
                    return $"{Translate((int)PerkLoc.Life)}{level}";
                case KnifePerk.KnifeDissolve:
                    return $"{Translate((int)PerkLoc.KnifesRemoving)}{level}";
                case KnifePerk.SlowingRotate:
                    return $"{Translate((int)PerkLoc.SlowingRotate)}" +
                        $"\n{level} {Translate((int)Other.Level)} ";
                case KnifePerk.QuickCharge:
                    return $"{Translate((int)PerkLoc.MoreLifeCharging)}" +
                        $"\n{level} {Translate((int)Other.Level)} ";
                case KnifePerk.AddApples:
                    return $"{Translate((int)PerkLoc.AppleSpawning)} {level}";
                case KnifePerk.LifeAndScoreAndRotate:
                    return $"{Translate((int)PerkLoc.Points)}{2 * level}\n{Translate((int)PerkLoc.Life)}1 \n" +
                        $"{ Translate((int)PerkLoc.SlowingRotate)}";
                case KnifePerk.ScoreAndApples:
                    return $"{Translate((int)PerkLoc.Points)}{level} \n {Translate((int)PerkLoc.AppleSpawning)} {level}";
                case KnifePerk.TakeLifeAddApples:
                    return $"{Translate((int)PerkLoc.TakeLife)}{Translate((int)PerkLoc.AppleSpawning)} 3 \n" +
                        $"{Translate((int)PerkLoc.MoreLifeCharging)}" +
                        $"2 {Translate((int)Other.Level)} \n" +
                        $"{Translate((int)PerkLoc.Points)}4";
                case KnifePerk.LifeScoreDiss:
                    return $"{Translate((int)PerkLoc.Life)}{level} \n" +
                        $"{Translate((int)PerkLoc.Points)}{2 * level}\n" +
                        $"{Translate((int)PerkLoc.KnifesRemoving)}{level}";
                case KnifePerk.LifeChargeRotateStage:
                    return $"{Translate((int)PerkLoc.Life)}{level} \n" +
                        $"{Translate((int)PerkLoc.MoreLifeCharging)}\n" +
                        $"{ Translate((int)PerkLoc.SlowingRotate)}\n" +
                        $"{ Translate((int)PerkLoc.SetStage)}";
                default:
                    return "";
            }

        }
        public static string CostDiscription(KnifeCost knifeCost, int perkCost)
        {
            switch (knifeCost)
            {
                case KnifeCost.None:
                    return "";
                case KnifeCost.Score:
                    return $"{perkCost} {Translate((int)PerkCost.Score)}";
                case KnifeCost.KnifeHits:
                    return $"{perkCost} {Translate((int)PerkCost.Hitting)}";
                case KnifeCost.Stage:
                    return $"{Translate((int)PerkCost.Level)}{perkCost}";
                case KnifeCost.Ads:
                    return $"{Translate((int)PerkCost.Ads)} {perkCost}";
                case KnifeCost.ScoreAndAdsAndStage:
                    return $"{perkCost * 300} {Translate((int)PerkCost.Score)}, " +
                        $"{Translate((int)PerkCost.Ads)} {perkCost}, " +
                        $"{Translate((int)PerkCost.Level)}{perkCost*5}";
                default:
                    return "???";
            }
        }

        public static bool Cost(KnifeCost knifeCost, int perkCost)
        {
            switch (knifeCost)
            {
                case KnifeCost.Score:
                    if (perkCost <= Data.Score)
                    {
                        return true;
                    }
                    return false;
                case KnifeCost.KnifeHits:
                    return Data.HitRecord >= perkCost;
                case KnifeCost.Stage:
                    return Data.StageRecord >= perkCost;
                case KnifeCost.None:
                    return true;
                case KnifeCost.Ads:
                    return Data.AdsCount >= perkCost;
                case KnifeCost.ScoreAndAdsAndStage:
                    if (perkCost*30 <= Data.Score && Data.AdsCount >= perkCost && Data.StageRecord >= perkCost*5)
                    {
                        return true;
                    }
                    return false;
                default:
                    return false;
            }

        }
        private static void AbilitySettings(float charge,float mod)
        {
            UiKnivesAndScoreEvents.AbilityCharge = charge;
            UiKnivesAndScoreEvents.AbilityMod = mod;
        }

    }
}