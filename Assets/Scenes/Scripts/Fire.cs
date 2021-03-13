using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Settings;
using static GameObjects;
using UnityEngine.EventSystems;
namespace Game
{
    public class Fire : MonoBehaviour
    {
        private float timer;
        public static Fire fire;
        private GameObject knifeToFire;

        private GameObject[] knifesToFire;
        public GameObject KnifeToFire { get => knifeToFire; set => knifeToFire = value; }
        public float Timer { get => timer; set => timer = value; }
        public GameObject[] KnifesToFire { get => knifesToFire; set => knifesToFire = value; }

        private Fire()
        {
            fire = this;
        }
        public void BonusLevel()
        {
            Materials.AmmountDissolve = 0;
            Materials.IsDissolve = false;
            knifesToFire = new GameObject[4];
            float rot = 180;
            for (int i = 0; i < 4; i++)
            {
                knifesToFire[i] = Instantiate(gameObjects.KnifePB, gameObjects.BonusSpawnPoints[i]);
                knifesToFire[i].GetComponentInChildren<SpriteRenderer>().sprite = KnifeActive.SpriteKnife;
                KnifesToFire[i].GetComponent<Rigidbody2D>().rotation = rot;
                Materials.IsDissolve = true;
                Knife KnifeOne = knifesToFire[i].GetComponent<Knife>();
                KnifeOne.Fly = false;
                Direction(i, out int x, out int y);
                KnifeOne.X = x;
                KnifeOne.Y = y;
                rot -= 90;
            }
        
        }
        private void Direction(int what, out int x, out int y)
        {
            x = 0;
            y = 0;
            switch (what)
            {
                case 0:
                    y = 1;
                    return;
                case 1:
                    x = 1;
                    return;
                case 2:
                    y = -1;
                    return;
                case 3:
                    x = -1;
                    return;
                default:
                    break;
            }
        }
            public void InstKnife()
        {
            Materials.AmmountDissolve = 0;
            Materials.IsDissolve = false;
            KnifeToFire = Instantiate(gameObjects.KnifePB, gameObjects.KnifeSpawnPoint);
            if (KnifeActive.SpriteKnife != null)
            { KnifeToFire.GetComponentInChildren<SpriteRenderer>().sprite = KnifeActive.SpriteKnife; }
            KnifeToFire.GetComponent<Rigidbody2D>().rotation = 180;
            Materials.IsDissolve = true;
            KnifeToFire.GetComponent<Knife>().Fly = false;

        }
        public void FireOn()
        {

            Materials.IsDissolve = false;
            if (Knifes > 0 && Timer >= settings.FireRate)
            {   if(knifesToFire != null)
                {
                    foreach(GameObject game in knifesToFire)
                    {
                        game.GetComponentInChildren<SpriteRenderer>().material = Materials.materials.Normal;
                        game.GetComponent<Knife>().Fly = true;
                        game.GetComponent<PolygonCollider2D>().enabled = true;
                        UiKnivesAndScoreEvents.uiKnivesAndScore.onFireEvent.Invoke();
                        Knifes--;
                    }
                    if (Knifes > 0)
                    {
                        BonusLevel();
                    }
                    timer = 0;
                    return;

                }
                KnifeToFire.GetComponentInChildren<SpriteRenderer>().material = Materials.materials.Normal;
                KnifeToFire.GetComponent<Knife>().Fly = true;
                KnifeToFire.GetComponent<PolygonCollider2D>().enabled = true;
                UiKnivesAndScoreEvents.uiKnivesAndScore.onFireEvent.Invoke();
                Knifes--;
                if (Knifes > 0)
                {
                   InstKnife();
                }
                timer = 0;
            }
        }
        public void FixedUpdate()
        {
                Timer += 0.01f;
        }
        
    }
}