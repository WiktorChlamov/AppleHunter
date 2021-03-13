using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;
using static GameObjects;
using static Settings;
namespace Game
{
    public class RandomKnifesAndApple : MonoBehaviour
    {
        private static Dictionary<Transform, Transform> selectedPoints; // object --> pointOfSpawn
        public static Transform[] AllUIKnifes { get; set; }
        public static Transform[] Heart { get; set; }
        public static Transform StageHeart { get => stageHeart; set => stageHeart = value; }
        public static Dictionary<Transform , Transform> SelectedPoints { get => selectedPoints; set => selectedPoints = value; }

        private static Transform stageHeart;
        public static void Spawning(int minKnife,int maxKnife,int apples)
        {
            SelectedPoints = new Dictionary<Transform, Transform>();
            int numStartKnifes = UnityEngine.Random.Range(minKnife, maxKnife);
            AppleSpawn(apples);
            CreateKnifes(numStartKnifes);
            UiKnifesSpawn();
            Hearts();
        }
        private static void Hearts()
        {   
                StageHeart = Instantiate(UIObjects.objectsUI.StageHealth, UIObjects.objectsUI.HeartParent);
                float alpha = Mathf.Clamp01(UiKnivesAndScoreEvents.StageHeartPoints);
                if( alpha != 1)
                 {      Color color = new Color { a = 0.4f };
                      StageHeart.GetComponent<Image>().color = color;
                  }
                StageHeart.GetComponent<Image>().fillAmount = alpha;
                Heart = new Transform[UiKnivesAndScoreEvents.Heart];
                for (int i = 0; i < Heart.Length; i++)
                {
                    Heart[i] = Instantiate(UIObjects.objectsUI.HeartPF, UIObjects.objectsUI.HeartParent);
                }
        }

        private static void UiKnifesSpawn()
        {
            AllUIKnifes = new Transform[Knifes];
            for (int i = 0; i < AllUIKnifes.Length; i++)
            {
                AllUIKnifes[i] = Instantiate(UIObjects.objectsUI.KnifesPrefubUI, UIObjects.objectsUI.KnifesUI);
            }
        }
        public static void AppleSpawn(int apples, bool ability = false)
        {
            foreach (Material material in Materials.materials.AppleMat)
            {
                material.SetFloat("_DissolveAmmount", 0);
            }
            gameObjects.Apples = new List<Transform>();
            if (apples == 1 && !ability)
            {
                if (SpawnApple(gameObjects.ApplePB.GetComponent<FruitPB>().Fruit.Chance))
                {
                    Transform point = gameObjects.PointsOfRandom[UnityEngine.Random.Range(0, gameObjects.PointsOfRandom.Length)];
                    Transform apple = InstantiateAndRotate(point, gameObjects.ApplePB);
                    gameObjects.Apples.Add(apple);
                    SelectedPoints.Add(apple, point);
                }
            }
            else
            {
                foreach(Transform transform in FillingPoints(apples))
                {
                    Transform apple = InstantiateAndRotate(transform, gameObjects.ApplePB);
                    if(ability)
                    {
                        SpriteRenderer[] sprites = apple.GetComponentsInChildren<SpriteRenderer>();
                        sprites[0].material=Materials.materials.AppleMat[0];
                        sprites[1].material = Materials.materials.AppleMat[1];
                        Materials.AppleDiss = true;
                    }
                    gameObjects.Apples.Add(apple);
                    SelectedPoints.Add(apple, transform);
                }
            }
        }
        private static Transform[] FillingPoints(int count)
        {   Transform[] points = new Transform[count];
            for (int i = 0; i < count; i++)
            {
                Transform point = gameObjects.PointsOfRandom[UnityEngine.Random.Range(0, gameObjects.PointsOfRandom.Length)];
                while (selectedPoints.ContainsValue(point) | Array.Exists(points, p => p == point))
                {
                    point = gameObjects.PointsOfRandom[UnityEngine.Random.Range(0, gameObjects.PointsOfRandom.Length)];
                }
                points[i]=point;
            }
            return points;
        }
        private static void CreateKnifes(int numStartKnifes)
        {
            foreach (Transform transform in FillingPoints(numStartKnifes))
            {
                Debug.Log(transform);
                Transform knife = InstantiateAndRotate(transform, gameObjects.KnifePB);
                knife.GetComponent<PolygonCollider2D>().enabled = true;
                selectedPoints.Add(knife, transform);
                gameObjects.KnifesInWood.Add(knife);
            }
        }
        private static Transform InstantiateAndRotate(Transform rotateToThisObject, GameObject gameObjectToRotate)
        {
            CircleCollider2D wheelCollider = gameObjects.Wheel.GetComponent<CircleCollider2D>();
            GameObject objectToRotate = Instantiate(gameObjectToRotate, wheelCollider.ClosestPoint(rotateToThisObject.position), Quaternion.identity);
            if (KnifeActive.SpriteKnife != null && objectToRotate.GetComponent<Knife>())
            {
                objectToRotate.GetComponent<Knife>().InWood = true;
                SpriteRenderer knifeSR = objectToRotate.GetComponentInChildren<SpriteRenderer>();
                knifeSR.sprite = KnifeActive.SpriteKnife;
                knifeSR.material = Materials.materials.Normal;
            }
            Vector2 direction = wheelCollider.ClosestPoint(rotateToThisObject.position) - (Vector2)wheelCollider.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            objectToRotate.GetComponent<Rigidbody2D>().rotation = angle;
            return objectToRotate.transform;
        }
        private static bool SpawnApple(int chance) => chance > UnityEngine.Random.Range(0, 100);
    }
}
