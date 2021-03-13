using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public static class Levels
    {
        private static Transform SetNormalStage(int knifes, float speed, int minKnifes =1,int maxKnifes =4, int apples =1, bool bonus = false,float fireSpeed = 0.05f,float knifeSpeed =1)
        {
            UIObjects uIObjects = UIObjects.objectsUI;
            Image bg = uIObjects.Background.GetComponent<Image>();
            bg.sprite = uIObjects.NormalFon;
            Settings.settings.KnifeSpeed = knifeSpeed;
            Settings.settings.FireRate = fireSpeed;
            UIObjects.objectsUI.BossAnoun.gameObject.SetActive(false);
            Rotate.TimeBetweenWhilling = Settings.settings.WheelingTime * 50;
            Settings.Knifes = knifes;
            Rotate.RotatingSpeed = speed;
            Transform wheel = WheelAndBosses.wheelAndBosses.Wheel();
            RandomKnifesAndApple.Spawning(minKnifes, maxKnifes, apples);
            if (bonus) { Fire.fire.BonusLevel(); }
            else { Fire.fire.InstKnife(); }
            return wheel;
        }
        private static void SetNormalLight()
        {
           /* Lights.lights.Objects.enabled = true; 
            Lights.lights.Main.intensity = 0.8f;
            Lights.lights.Up.enabled = false;
            Lights.lights.Knife.enabled = false;
            Lights.lights.PerkButtonLight.enabled = false;*/
        }
        public static Transform Stages(int stage)
        {
            
            SetNormalLight();
            switch (stage)
            {
                 case int _ when stage < 3:
                    return SetNormalStage(3, 1f,0,0);
                 case int _ when stage < 5:
                    return SetNormalStage(4, 1f,0, 0);
                case int _ when stage == 5:
                    return SetNormalStage(4, 1f,0,0,2,true) ;
                case int _ when stage < 8:
                    return SetNormalStage(5, 1.1f, 0, 0);
                case int _ when stage < 10:
                    return SetNormalStage(3, 1.1f,1,2);
                case int _ when stage == 10:
                    return WheelAndBosses.wheelAndBosses.SetBoss(0);
                case int _ when stage < 13:
                    return SetNormalStage(4, 1.2f,1,3);
                case int _ when stage < 15:
                    return SetNormalStage(5, 1.2f,1,3);
                case int _ when stage == 15:
                    return SetNormalStage(8, 1.2f, 0, 0, 3, true);
                case int _ when stage < 18:
                    return SetNormalStage(7, 1.2f,1,4);
                case int _ when stage < 20:
                    return SetNormalStage(8, 1.2f,1,4);
                case int _ when stage == 20:
                    return WheelAndBosses.wheelAndBosses.SetBoss(1);
                case int _ when stage < 23:
                    return SetNormalStage(4, 1.4f, 2, 4);
                case int _ when stage < 25:
                    return SetNormalStage(5, 1.4f, 2, 5);
                case int _ when stage == 25:
                    return SetNormalStage(8, 1.4f, 0, 0, 4, true);
                case int _ when stage < 28:
                    return SetNormalStage(6, 1.4f, 2, 5);
                case int _ when stage < 30:
                    return SetNormalStage(7, 1.4f, 2, 5);
                case int _ when stage == 30:
                    return WheelAndBosses.wheelAndBosses.SetBoss(2);
                case int _ when stage < 33:
                    return SetNormalStage(8, 1.5f, 0, 0);
                case int _ when stage < 35:
                    return SetNormalStage(10, 1.5f, 0, 0);
                case int _ when stage == 35:
                    return SetNormalStage(8, 1.5f, 0, 0, 6, true);
                case int _ when stage < 38:
                    return SetNormalStage(3, 1.5f, 8, 10);
                case int _ when stage < 40:
                    return SetNormalStage(2, 1.5f, 10, 14);
                case int _ when stage == 40:
                    return WheelAndBosses.wheelAndBosses.SetBoss(3);
                case int _ when stage < 43:
                    return SetNormalStage(10, 2f, 0, 0,2,fireSpeed: 0.01f, knifeSpeed: 1.5f);
                case int _ when stage < 45:
                    return SetNormalStage(14, 2f, 0, 0);
                case int _ when stage == 45:
                    return SetNormalStage(12, 2f, 0, 0, 8, true, fireSpeed: 0.03f,knifeSpeed: 1.5f);
                case int _ when stage < 48:
                    return SetNormalStage(5, 2f, 1, 10, fireSpeed: 0.03f,knifeSpeed: 1.5f);
                case int _ when stage < 50:
                    return SetNormalStage(6, 2f, 2, 11, fireSpeed: 0.03f,knifeSpeed: 1.5f);
                case int _ when stage == 50:
                    return WheelAndBosses.wheelAndBosses.SetBoss(4);
                case int _ when stage < 53:
                    return SetNormalStage(4, 2.2f, 3, 5, fireSpeed: 0.03f);
                case int _ when stage < 55:
                    return SetNormalStage(6, 2.2f, 3, 5, fireSpeed: 0.03f, knifeSpeed: 1.5f);
                case int _ when stage == 55:
                    return SetNormalStage(8, 2.2f, 0, 0, 0, false, fireSpeed: 0.03f, knifeSpeed: 1.5f);
                case int _ when stage < 58:
                    return SetNormalStage(3, 2.2f, 8, 12, fireSpeed: 0.03f, knifeSpeed: 1.5f);
                case int _ when stage < 60:
                    return SetNormalStage(5, 2.2f, 6, 10, fireSpeed: 0.03f, knifeSpeed: 1.5f);
                case int _ when stage == 60:
                    return WheelAndBosses.wheelAndBosses.SetBoss(5);
                case int _ when stage < 63:
                    return SetNormalStage(2, 2.5f, 10, 14, fireSpeed: 0.03f, knifeSpeed: 1.5f);
                case int _ when stage < 65:
                    return SetNormalStage(3, 2.5f, 9, 13, fireSpeed: 0.03f, knifeSpeed: 1.5f);
                case int _ when stage == 65:
                    return SetNormalStage(12, 2.5f, 0, 0, 12, true);
                case int _ when stage < 68:
                    return SetNormalStage(4, 2f, 10, 11, fireSpeed: 0.03f, knifeSpeed: 1.5f);
                case int _ when stage < 70:
                    return SetNormalStage(5, 2f, 10, 12, fireSpeed: 0.03f, knifeSpeed: 1.5f);
                case int _ when stage == 70:
                    return WheelAndBosses.wheelAndBosses.SetBoss(6);
                case int _ when stage < 73:
                    return SetNormalStage(12, 2.7f, 0, 0, fireSpeed: 0.03f, knifeSpeed: 1.5f);
                case int _ when stage < 75:
                    return SetNormalStage(14, 2.7f, 0, 0, fireSpeed: 0.03f, knifeSpeed: 1.5f);
                case int _ when stage == 75:
                    return SetNormalStage(16, 1f, 0, 0, 20, true);
                case int _ when stage < 78:
                    return SetNormalStage(18, 1f, 0, 0, fireSpeed: 0.03f, knifeSpeed: 1.5f);
                case int _ when stage < 80:
                    return SetNormalStage(20, 1f, 0, 0, fireSpeed: 0.03f, knifeSpeed: 1.5f);
                case int _ when stage == 80:
                    return WheelAndBosses.wheelAndBosses.SetBoss(7);
                default:
                    return SetNormalStage(20, 1.5f);
            }
        }
    }
}
