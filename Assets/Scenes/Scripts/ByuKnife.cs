using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Game {
    public class ByuKnife : MonoBehaviour, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {   if (SelectKnife.ToBuySelected != null)
                
            {   SelectKnife selectKnife = SelectKnife.ToBuySelected.GetComponent<SelectKnife>();
                if (KnifePerks.Cost(selectKnife.UniqueKnifes.cost, selectKnife.UniqueKnifes.perkCost))
                {   if(selectKnife.UniqueKnifes.cost == KnifeCost.Score)
                    {
                        Data.Score -= selectKnife.UniqueKnifes.perkCost;
                    }
                    Data.ByedKnifes.Add(selectKnife.UniqueKnifes.Id);
                    KnifeActive.knifeActive.InstOneByed();
                    Destroy(SelectKnife.ToBuySelected.gameObject);
                    SelectKnife.ToBuySelected = null;
                    Data.Save();
                    KnifeActive.knifeActive.DiscriptionSecond.SetText("");
                }
            }
        }
    }
}
