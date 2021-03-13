using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfBoss
{   NoKnifes,
    LastKnifes,
    Light,
    MovingLR,
    MovingUD,
    LastBoss
}
public enum KnifeRare
{
    Normal,Rare,Unique
}
public enum KnifePerk
{   None,
    ExtraScore,
    ExtraLife,
    KnifeDissolve,
    SlowingRotate,
    QuickCharge,
    AddApples,
    LifeAndScoreAndRotate,
    ScoreAndApples,
    TakeLifeAddApples,
    LifeScoreDiss,
    LifeChargeRotateStage
}
public enum KnifeCost
{   None,
    Score,
    KnifeHits,
    Stage,
    Ads,
    ScoreAndAdsAndStage
}
public enum PerkLevels
{  
    None,
   First ,
   Second,
   Third,
   Fourth,
   Fifth
}
