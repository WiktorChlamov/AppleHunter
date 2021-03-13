using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BossName", menuName = "Boss")]
public class Boss : ScriptableObject
{
    public Transform boss;
    public int startingKnifes;
    public float wheelSpeed;
    public int ApplesCount;
    public TypeOfBoss typeOfBoss;
    public int stageKnifes;

}
