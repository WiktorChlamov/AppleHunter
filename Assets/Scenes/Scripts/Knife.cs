using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Settings;
namespace Game
{
    public class Knife : MonoBehaviour
    {
        private static bool knifeHit = false;
        private bool inWood, baseKnife = true;
        private int x, y =1;
        public static bool KnifeHit { get => knifeHit; set => knifeHit = value; }
        public bool Fly { get; set; } = false;
        public bool Rotate { get; set; } = false;
        public bool InWood { get => inWood; set => inWood = value; }
        public bool BaseKnife { get => baseKnife; set => baseKnife = value; }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }

        void FixedUpdate()
        {
            if (Fly)
            {
                transform.position += new Vector3(X * settings.KnifeSpeed, Y*settings.KnifeSpeed);
            }
            else 
            if (Rotate)
            {
                transform.Rotate(0, 0, 10);
            }

        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            
            if (collision.gameObject.TryGetComponent(out Knife _) && KnifeHit == false)
            {
                Debug.Log("Knife");
                KnifeHit = true;
              if (!InWood)
                {
                    Fly = false; Rotate = true;
                    Vector2 vector2 = gameObject.transform.position - collision.transform.position;
                    Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
                    rb.AddForce(vector2 * 150);
                    rb.gravityScale = 1;
                    UiKnivesAndScoreEvents.uiKnivesAndScore.OnKnifeHit.Invoke();
                    transform.SetParent(null);
                    Destroy(gameObject, 1);
                }
                return;
            }
            
            else if (collision.gameObject.TryGetComponent(out Wheel _))
            {
                InWood = true;
                transform.SetParent(collision.transform);
                Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
                rigidbody2D.bodyType = RigidbodyType2D.Static;
                if (Fly)
                {
                    baseKnife = false;
                    GameObjects.gameObjects.KnifesInWood.Add(transform);
                    UiKnivesAndScoreEvents.uiKnivesAndScore.onWheelHitEvent.Invoke();
                }
                Fly = false;

            }
            
        }
            private void OnTriggerEnter2D(Collider2D collision)
        {   
            if (!InWood & collision.gameObject.TryGetComponent(out FruitPB _))
            {
                RandomKnifesAndApple.SelectedPoints.Remove(collision.transform);
                GameObjects.gameObjects.CurrentApple = collision.gameObject;
                collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;
                UiKnivesAndScoreEvents.uiKnivesAndScore.appleHitEvent.Invoke();
                Destroy(collision.gameObject, 5f);
                GameObjects.gameObjects.CurrentApple = null;
                return;
            }
            if (!InWood & collision.gameObject.TryGetComponent(out MeshFilter _))
            {
                Debug.Log("UFO");
                UiKnivesAndScoreEvents.uiKnivesAndScore.OnKnifeHit.Invoke();
                return;
            }
        }
    }
}
