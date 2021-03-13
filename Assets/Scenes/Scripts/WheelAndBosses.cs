using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

namespace Game
{
    public class WheelAndBosses : MonoBehaviour
    {
        [SerializeField] Transform movingLeft, movingRight,movingUp,movingDawn;
        Transform moveTo;
        public static WheelAndBosses wheelAndBosses;
        public Boss[] bosses;
        private bool move = false;
        private Transform wheel;
        public bool Move { get => move; set => move = value; }

        private WheelAndBosses ()
        {
            wheelAndBosses = this;
        }
        private void Update()
        {
            if(move)
            {
                wheel.position = Vector3.MoveTowards(wheel.position, moveTo.position, 0.01f);
                if (Vector3.Distance(wheel.position, moveTo.position) < 0.001f)
                {
                    if (moveTo == movingLeft || moveTo == movingRight)
                    { moveTo = moveTo == movingLeft ? movingRight : movingLeft; }
                    else
                    {
                        moveTo = moveTo == movingUp ? movingDawn : movingUp;
                    }
                }

            }

        }

        public Transform Wheel()
        {   
            return wheel = Instantiate(GameObjects.gameObjects.WheelPB, transform);
        }
        
        public Transform SetBoss(int bossNumber)
        {
            UIObjects uIObjects = UIObjects.objectsUI;
            uIObjects.BossAnoun.gameObject.SetActive(true);
            Image bg = uIObjects.Background.GetComponent<Image>();
            bg.sprite = uIObjects.BossFon;
            Boss selecteddBoss = bosses[bossNumber];
            Rotate.RotatingSpeed = selecteddBoss.wheelSpeed;
            wheel = Instantiate(selecteddBoss.boss, transform);
            Settings.Knifes = selecteddBoss.startingKnifes;
            void CreateKnifesAndApple(int stageKnifes = 0)
            {
            int apples = Mathf.Clamp(selecteddBoss.ApplesCount, 0, GameObjects.gameObjects.PointsOfRandom.Length- stageKnifes);
            RandomKnifesAndApple.Spawning(stageKnifes, stageKnifes+1, apples);
            Fire.fire.InstKnife();
            }
            switch (selecteddBoss.typeOfBoss)
            {
                case TypeOfBoss.NoKnifes:
                    CreateKnifesAndApple();
                    break;
                case TypeOfBoss.LastKnifes:
                    CreateKnifesAndApple(GameObjects.gameObjects.PointsOfRandom.Length - Settings.Knifes);
                    //StartCoroutine(Moving(GameObjects.gameObjects.Wheel));
                    break;
                case TypeOfBoss.Light:
                    CreateKnifesAndApple(selecteddBoss.stageKnifes);
                    Rotate.TimeBetweenWhilling = Settings.settings.WheelingTime * 500;
                  /*  Lights.lights.Main.intensity = 0f;
                    //Lights.lights.Up.enabled = true;
                    Lights.lights.Knife.enabled = true;
                    Lights.lights.Objects.enabled = false;
                    Lights.lights.PerkButtonLight.enabled = true;
                    //Lights.lights.Dawn.enabled = true;*/
                    break;
                case TypeOfBoss.MovingLR:
                    CreateKnifesAndApple(selecteddBoss.stageKnifes);
                    moveTo = movingLeft;
                    Move = true;
                    break;
                case TypeOfBoss.MovingUD:
                    CreateKnifesAndApple(selecteddBoss.stageKnifes);
                    moveTo = movingUp;
                    Move = true;
                    break;
                case TypeOfBoss.LastBoss:
                    break;
                default:
                    break;
            }
            return wheel;
        }
    }
}