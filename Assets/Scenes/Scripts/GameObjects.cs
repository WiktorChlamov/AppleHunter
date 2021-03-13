using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjects : MonoBehaviour
{
    public static GameObjects gameObjects;
    [SerializeField] private Transform knifeSpawnPoint,particle,wheelPB, glow,gameField;
    [SerializeField] private GameObject knifePB, applePB, currentApple;
    [SerializeField] private Transform[] bonusSpawnPoints, points, woodPieces;
    [SerializeField] private AudioClip knifeThrow,wheelHitting,appleHitting,knifeHitting;
    [SerializeField] private GameObject music;
    [SerializeField] private AudioClip[] musics;
    [SerializeField] private AudioSource sound;
    private List<Transform> apples;


    public Transform Wheel { get; set; }
    public List<Transform> KnifesInWood { get; set; } = new List<Transform>();
    public Transform[] PointsOfRandom { get => points; set => points = value; }
    public GameObject KnifePB { get => knifePB; set => knifePB = value; }
    public GameObject ApplePB { get => applePB; set => applePB = value; }
    public Transform KnifeSpawnPoint { get => knifeSpawnPoint; set => knifeSpawnPoint = value; }
    public Transform[] WoodPieces { get => woodPieces; set => woodPieces = value; }
    public Transform Particle { get => particle; set => particle = value; }
    public GameObject Music { get => music; set => music = value; }
    public AudioClip[] Musics { get => musics; set => musics = value; }
    public AudioClip KnifeThrow { get => knifeThrow; set => knifeThrow = value; }
    public AudioClip WheelHitting { get => wheelHitting; set => wheelHitting = value; }
    public AudioClip AppleHitting { get => appleHitting; set => appleHitting = value; }
    public AudioClip KnifeHitting{ get => knifeHitting; set => knifeHitting = value; }
    public AudioSource Sound { get => sound; set => sound = value; }
    public Transform WheelPB { get => wheelPB; set => wheelPB = value; }
    public Transform Glow { get => glow; set => glow = value; }
    public Transform GameField { get => gameField; set => gameField = value; }
    public GameObject CurrentApple { get => currentApple; set => currentApple = value; }
    public List<Transform> Apples { get => apples; set => apples = value; }
    public Transform[] BonusSpawnPoints { get => bonusSpawnPoints; set => bonusSpawnPoints = value; }

    private void Awake()
    {
        gameObjects = this;
    }
}
