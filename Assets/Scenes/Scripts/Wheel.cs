using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] Transform glow;
    [SerializeField] Transform[] pieces,points;
    private void Awake()
    {
        GameObjects.gameObjects.PointsOfRandom = points;
        GameObjects.gameObjects.WoodPieces = pieces;
        GameObjects.gameObjects.Wheel = transform;
        GameObjects.gameObjects.Glow = glow;
    }
}
