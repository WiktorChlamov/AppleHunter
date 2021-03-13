using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FruitName", menuName = "Fruit")]
public class Fruit : ScriptableObject
{   
    [SerializeField]
    private int chance;
    public int Chance { get => chance; set => chance = value; }
}
