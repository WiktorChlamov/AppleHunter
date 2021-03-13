using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace Game {
    public class SelectKnife : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Image sprite;
        [SerializeField] private UniqueKnifes uniqueKnifes;
        static Transform selected;
        static Transform toBuySelected;
        [SerializeField] bool shell;

        public UniqueKnifes UniqueKnifes { get => uniqueKnifes; set => uniqueKnifes = value; }
        public static Transform Selected { get => selected; set => selected = value; }
        public static Transform ToBuySelected { get => toBuySelected; set => toBuySelected = value; }
        public Image Sprite { get => sprite; set => sprite = value; }

        private void Start()
        {
            Sprite.sprite = UniqueKnifes.knifeSprite;
        }
        public static void SetText()
        {
            if (selected)
            {
                UniqueKnifes knife = selected.GetComponent<SelectKnife>().uniqueKnifes;
                KnifeActive.knifeActive.Discription.text = knife.KnifeName + "\n" + KnifePerks.Discription(knife.knifePerk, (int)knife.perkLevel); ;
            }
            if(toBuySelected)
            {
                UniqueKnifes knife = toBuySelected.GetComponent<SelectKnife>().uniqueKnifes;
                KnifeActive.knifeActive.DiscriptionSecond.text = knife.KnifeName + "\n" + KnifePerks.Discription(knife.knifePerk, (int)knife.perkLevel); ;

            }
        }
            public void OnPointerDown(PointerEventData eventData)
        {
            if (shell)
            {
                if (Selected != null) { Selected.GetComponent<Image>().enabled = false; }
                Selected = transform;
                Data.SelectedKnife = uniqueKnifes.Id;
                KnifeActive.SpriteKnife = UniqueKnifes.knifeSprite;
                KnifeActive.knifeActive.ChangeKnifes();
                GetComponent<Image>().enabled = true;
                KnifePerks.Perk(uniqueKnifes.knifePerk, (int)uniqueKnifes.perkLevel, out string discription);
                KnifeActive.knifeActive.Discription.color = LocalizationManager.Color32(uniqueKnifes.knifeRare);
                KnifeActive.knifeActive.Discription.text = uniqueKnifes.KnifeName + "\n" + discription;
            }
            else
            {
                if (toBuySelected != null) { toBuySelected.GetComponent<Image>().enabled = false; }
                toBuySelected = transform;
                KnifeActive.knifeActive.DiscriptionSecond.color = LocalizationManager.Color32(uniqueKnifes.knifeRare);
                string disc = $"{KnifePerks.Discription(uniqueKnifes.knifePerk, (int)uniqueKnifes.perkLevel)} \n {Localization.Translate((int)Localization.Other.Oppening)} {KnifePerks.CostDiscription(uniqueKnifes.cost, uniqueKnifes.perkCost)} ";
                KnifeActive.knifeActive.DiscriptionSecond.SetText(uniqueKnifes.KnifeName + "\n" + disc);
                GetComponent<Image>().enabled = true;
                if (KnifePerks.Cost(UniqueKnifes.cost, UniqueKnifes.perkCost))
                {
                    KnifeActive.knifeActive.BuyButton.GetComponent<Image>().material = Materials.materials.BuyButton;
                }
                else
                {
                    KnifeActive.knifeActive.BuyButton.GetComponent<Image>().material = null;
                }
            }
            //KnifeActive.knifeActive.ChangeKnifes();
            
        }
    }
}