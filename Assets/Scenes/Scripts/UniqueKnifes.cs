using System;
using System.Collections;
using System.Collections.Generic;
using static Localization;
using UnityEngine;
[CreateAssetMenu(fileName = "KnifeName", menuName = "Knife")]
public class UniqueKnifes : ScriptableObject
{   [SerializeField]
    private KnifeNames knifeName;
    public Sprite knifeSprite;
    public KnifePerk knifePerk;
    public PerkLevels perkLevel;
    public int perkCost;
    public KnifeCost cost;
    
    [SerializeField] private string id = string.Empty;
    public KnifeRare knifeRare;
    public string Id => id;

    public string KnifeName { get => Translate((int)knifeName); }
    [ContextMenu("GenerateID")]
    private void GenerateID() => id = Guid.NewGuid().ToString();

}
