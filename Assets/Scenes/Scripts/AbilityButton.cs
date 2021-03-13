using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    public AbilityButton abilityButton;
    public static UnityAction action;
    private void Awake()
    {   
        if(action == null)
        {
            gameObject.SetActive(false);
            return;
        }
        Material material = UIObjects.objectsUI.PerkButton.GetComponent<Image>().material;
        if (Game.UiKnivesAndScoreEvents.AbilityCharge == 1)
        {
            material.SetFloat("_OutlineSick", 1);
        }
        else
        {
            material.SetFloat("_OutlineSick", 0);
            UIObjects.objectsUI.PerkButton.GetComponent<Image>().fillAmount = 0;
        }

        GetComponent<Button>().onClick.AddListener(action);
    }
    
}
