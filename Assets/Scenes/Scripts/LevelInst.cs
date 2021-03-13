using System.Collections;
using System.Collections.Generic;
using TMPro;
using static Localization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

namespace Game
{
    public class LevelInst : MonoBehaviour
    {
        public static LevelInst levelInst;
        private static int stage =1;

        public static int Stage { get => stage; set => stage = value; }

        private void Awake()
        {
            Camera.main.GetComponent<Volume>().enabled = Data.Bloom;
            Data.CurrentHits = 0;
            Data.CurrentStage = Stage;
            levelInst = this;
            Instantiate();
            UIObjects.objectsUI.HeartParent.GetComponent<TextMeshProUGUI>().SetText(Translate((int)Other.Lifes));
        }
        public void Instantiate()
        {
            GameObjects.gameObjects.GameField.gameObject.SetActive(true);
            Knife.KnifeHit = false;
            Rotate.BaseState();
         UIObjects.objectsUI.ScoreUI.GetComponent<TextMeshProUGUI>().SetText(Translate((int)Other.Score)+ Data.Score);
         UIObjects.objectsUI.HitCount.GetComponent<TextMeshProUGUI>().SetText(Translate((int)Other.Hitting) + Data.CurrentHits);
         UIObjects.objectsUI.StageUI.GetComponent<TextMeshProUGUI>().SetText(Translate((int)Other.Stage) + Data.CurrentStage);
            Levels.Stages(Data.CurrentStage); // установка колеса
                                              //Fire.fire.InstKnife();

        }
        public void Devastate()
        {
            StopCoroutines();
            if(Fire.fire.KnifesToFire != null)
            {
                foreach(GameObject knife in Fire.fire.KnifesToFire)
                {
                    Destroy(knife);
                }
                Fire.fire.KnifesToFire = null;
            }
            if (Fire.fire.KnifeToFire != null)
            {
                Destroy(Fire.fire.KnifeToFire);
                Fire.fire.KnifeToFire = null;
            }
            Destroy(GameObjects.gameObjects.Wheel.gameObject);
            DestroyGameObjects(GameObjects.gameObjects.CurrentApple);
            DestroyGameObjects(GameObjects.gameObjects.WoodPieces);
            DestroyGameObjects(GameObjects.gameObjects.Apples);
            DestroyGameObjects(GameObjects.gameObjects.KnifesInWood);
            DestroyGameObjects(RandomKnifesAndApple.AllUIKnifes);
            DestroyGameObjects(RandomKnifesAndApple.Heart);
            DestroyGameObjects(RandomKnifesAndApple.StageHeart.gameObject);
            Instantiate();
        }
        private void DestroyGameObjects(Transform[] array)
        {
            foreach (Transform piece in array)
            {
                if (piece != null) { Destroy(piece.gameObject); }
            }
        }
        private void DestroyGameObjects(List<Transform> array)
        {
            foreach (Transform piece in array)
            {
                if (piece != null) { Destroy(piece.gameObject); }
            }
            array.Clear();
        }
        private void DestroyGameObjects(GameObject target)
        {
            if(target != null) { Destroy(target); }
        }
            public void StopCoroutines()
        {
            WheelAndBosses.wheelAndBosses.Move = false;
            if (UiKnivesAndScoreEvents.uiKnivesAndScore.Timer != null)
            {
                StopCoroutine(UiKnivesAndScoreEvents.uiKnivesAndScore.Timer);
               UiKnivesAndScoreEvents.uiKnivesAndScore.Timer = null;
            }
        }
    }
}
