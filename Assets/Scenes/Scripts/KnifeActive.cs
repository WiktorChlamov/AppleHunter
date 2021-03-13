using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Game {
    public class KnifeActive : MonoBehaviour
    {
        public static KnifeActive knifeActive;
        [SerializeField] private TextMeshProUGUI discriptionFirst, discriptionSecond,adsCount;
        [SerializeField] private Transform shell, knifeSlot, stok, knifeStokPB, buyButton;
        [SerializeField] private List<UniqueKnifes> buyedKnifes, unBuyedKnifes;
        public TextMeshProUGUI Discription { get => discriptionFirst; set => discriptionFirst = value; }
        public static Sprite SpriteKnife { get; set; }
        public TextMeshProUGUI DiscriptionSecond { get => discriptionSecond; set => discriptionSecond = value; }
        public List<UniqueKnifes> UnBuyedKnifes { get => unBuyedKnifes; set => unBuyedKnifes = value; }
        public List<UniqueKnifes> BuyedKnifes { get => buyedKnifes; set => buyedKnifes = value; }
        public Transform BuyButton { get => buyButton; set => buyButton = value; }
        public TextMeshProUGUI AdsCount { get => adsCount; set => adsCount = value; }

        private void Start()
        {
            InstBuyed();
            InstUnBuyed();
        }
        private void InstBuyed()
        {
            for (int i = 0; i < buyedKnifes.Count; i++)
            {
                Transform slot = InstSlots(buyedKnifes[i], knifeSlot, shell);

                if (SelectKnife.Selected == null || buyedKnifes[i].Id == Data.SelectedKnife)
                {
                    SelectKnife.Selected = slot;
                }
            }
            UniqueKnifes uniqueKnife = SelectKnife.Selected.GetComponent<SelectKnife>().UniqueKnifes;
            SpriteKnife = uniqueKnife.knifeSprite;
            ChangeKnifes();
            SelectKnife.Selected.GetComponent<Image>().enabled = true;
            Discription.color = LocalizationManager.Color32(uniqueKnife.knifeRare);
            KnifePerks.Perk(uniqueKnife.knifePerk, (int)uniqueKnife.perkLevel,out string discription);
            Discription.SetText(uniqueKnife.KnifeName + "\n" + discription);
        }
        public void Destroy()
        {
            foreach(GameObject gameObject in shell.GetComponentsInChildren<GameObject>())
            {
                Destroy(gameObject);
            }
        }
        private void InstUnBuyed()
        {
            foreach (UniqueKnifes unique in unBuyedKnifes)
            {
                InstSlots(unique, knifeStokPB, stok);
            }
        }
        public void InstOneByed()
        {
            InstSlots(SelectKnife.ToBuySelected.GetComponent<SelectKnife>().UniqueKnifes, knifeSlot, shell);
        }
        private Transform InstSlots(UniqueKnifes knife, Transform knifePB, Transform parent)
        {
            Transform slot = Instantiate(knifePB, parent);
            SelectKnife select = slot.GetComponent<SelectKnife>();
            select.UniqueKnifes = knife;
            switch (knife.knifeRare)
            {
                case KnifeRare.Normal:
                    select.Sprite.material = Materials.materials.NormalKnife;
                    break;
                case KnifeRare.Rare:
                    select.Sprite.material = Materials.materials.RareKnife;
                    break;
                case KnifeRare.Unique:
                    select.Sprite.material = Materials.materials.UniqueKnifeMat;
                    break;
            }
            return slot;
        }
        private void Awake()
        {
            knifeActive = this;
        }
        public void ChangeKnifes()
        {
            Materials.materials.Dissolve.SetTexture("_MainTexture", SpriteKnife.texture);
            Materials.materials.DissolveDawn.SetTexture("_MainTexture", SpriteKnife.texture);
        }
    }
}
