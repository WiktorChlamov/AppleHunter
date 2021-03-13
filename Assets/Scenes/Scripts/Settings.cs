using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static Settings settings;
    [SerializeField] private static int knifesCount;
    [SerializeField] private float fireRate;
    [SerializeField] private float knifeSpeed;
    [SerializeField] private float wheelingTime;
    [SerializeField] private float wheelStoppingTime;
    public static int Knifes { get => knifesCount; set => knifesCount = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }
    public float KnifeSpeed { get => knifeSpeed; set => knifeSpeed = value; }
    public float WheelingTime { get => wheelingTime; set => wheelingTime = value; }
    public float WheelStoppingTime { get => wheelStoppingTime; set => wheelStoppingTime = value; }

    private void Awake()
    {
        settings = this;
    }
}
