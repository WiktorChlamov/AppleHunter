using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Game {
    public class Ability : MonoBehaviour
    {
        public static Ability ability;
        private Ability()
        {
            ability = this;
        }
        private void PerkButtonChanges()
        {
            UiKnivesAndScoreEvents.AbilityCharge = 0;
            UIObjects.objectsUI.PerkButton.GetComponent<Image>().fillAmount = 0;
            Material material = UIObjects.objectsUI.PerkButton.GetComponent<Image>().material;
            material.SetFloat("_OutlineSick", 0);
        }
        public void TakeLife()
        {
            if(UiKnivesAndScoreEvents.StageHeartPoints < 1) { return; }
            UiKnivesAndScoreEvents.StageHeartPoints = 0;
            RandomKnifesAndApple.StageHeart.GetComponent<Image>().fillAmount = 0;
            AddApples(3);
            PerkButtonChanges();
        }
        public void ReduceSpeed(float number)
        {
            if (UiKnivesAndScoreEvents.AbilityCharge < 1)
            {
                return;
            }
            PerkButtonChanges();
            Rotate.RotatingSpeed /= number;
        }
        public void AddApples(int count)
        {
            if (UiKnivesAndScoreEvents.AbilityCharge < 1)
            {
                return;
            }
            int freeSpace = GameObjects.gameObjects.PointsOfRandom.Length - RandomKnifesAndApple.SelectedPoints.Count;
            if (count > freeSpace)
            {   
                if(GameObjects.gameObjects.KnifesInWood.Count ==0)
                {
                    count = freeSpace;
                }
                RemoveKnifes(count - freeSpace);
            }
            else
            {PerkButtonChanges();
            }
            RandomKnifesAndApple.AppleSpawn(count,true);
            
        }
        public void RemoveKnifes(int count)
        {
            if (UiKnivesAndScoreEvents.AbilityCharge <1)
            {
                return;
            }
            PerkButtonChanges();
            int maxRange = GameObjects.gameObjects.KnifesInWood.Count > count ? count : GameObjects.gameObjects.KnifesInWood.Count;
            if (maxRange == 0) { return; };
            int[] randoms = new int[maxRange];
            for (int i = 0; i < randoms.Length; i++)
            {
                int select = UnityEngine.Random.Range(1, maxRange + 1);
                while (Array.Exists(randoms, poi => select == poi))
                {
                    select = UnityEngine.Random.Range(1, maxRange + 1);
                }
                randoms[i] = select;
            }
            var random = randoms.OrderByDescending(t => t);
            foreach (int numb in random)
            {
                GameObjects.gameObjects.KnifesInWood[numb - 1].GetComponentInChildren<SpriteRenderer>().material = Materials.materials.DissolveDawn;
                Materials.IsDissolveDawn = true;
                RandomKnifesAndApple.SelectedPoints.Remove(GameObjects.gameObjects.KnifesInWood[numb - 1]);
                Destroy(GameObjects.gameObjects.KnifesInWood[numb - 1].gameObject, 1);
                GameObjects.gameObjects.KnifesInWood.RemoveAt(numb - 1);
            }

        }
    }
}
