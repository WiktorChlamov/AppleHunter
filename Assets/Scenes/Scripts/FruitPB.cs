using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game
{
    public class FruitPB : MonoBehaviour
    {
        [SerializeField] private Fruit fruit;
        [SerializeField] private Transform[] pieces;
        public Fruit Fruit { get => fruit; set => fruit = value; }
        public Transform[] Pieces { get => pieces; set => pieces = value; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.TryGetComponent(out Knife knife))
            {
                transform.SetParent(collision.transform);
            }
            else
            {
                if(knife.InWood && !knife.BaseKnife)
                {
                    Materials.IsDissolveDawn = true;
                    collision.GetComponentInChildren<SpriteRenderer>().material = Materials.materials.DissolveDawn;
                    Destroy(collision.gameObject, 1);
                    GameObjects.gameObjects.KnifesInWood.Remove(collision.transform);
                }
            }
        }
    }
}
