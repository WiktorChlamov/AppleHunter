using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public class Rotate : MonoBehaviour
{
    private static float timeBetweenWhilling;
    private static float stoppingTime;
    private static float rotating = 0f;
    private static float rotatingSpeed;
    private sbyte direction = 1;  
    private float fps = 50;

    [SerializeField] private GameObjects gameObjects;
    public static float TimeBetweenWhilling { get => timeBetweenWhilling; set => timeBetweenWhilling = value; }
    public static float RotatingSpeed { get => rotatingSpeed; set => rotatingSpeed = value; }
    public static float Rotating { get => rotating; set => rotating = value; }
    public static float StoppingTime { get => stoppingTime; set => stoppingTime = value; }
    public static void BaseState()
    {
        StoppingTime = Settings.settings.WheelStoppingTime * 50;
        TimeBetweenWhilling = Settings.settings.WheelingTime * 50;
        Rotating = 0;
    }
    private void Start()
    {
        BaseState();
    }
    
        void FixedUpdate() 
    {   if (gameObjects.Wheel == null) { TimeBetweenWhilling = Settings.settings.WheelingTime * fps; return; };
        Quaternion angle = Quaternion.Euler(0, 0, Rotating);
        gameObjects.Wheel.transform.rotation =
            Quaternion.Slerp(gameObjects.Wheel.transform.rotation, angle, 0.05f);
        if(StoppingTime != Settings.settings.WheelStoppingTime * fps)
        {   
            StoppingTime++;
            return;
        }
        if(Rotating > 360 || Rotating < -360)
        {
            Rotating = 0;
        }
        Rotating+=RotatingSpeed * direction;
        if (TimeBetweenWhilling >= 0)
        {
            TimeBetweenWhilling--;
        }
        else
        { 
            TimeBetweenWhilling = Settings.settings.WheelingTime* fps;
            StoppingTime = 0;
            Random rd = new Random();
            sbyte[] dir = new sbyte[2] { -1, 1 };
            direction = dir[rd.Next(0,2)];
        }
    }
}
