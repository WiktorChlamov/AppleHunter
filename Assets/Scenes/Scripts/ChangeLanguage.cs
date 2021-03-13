using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
public class ChangeLanguage : MonoBehaviour
{
    public Localization.Lang lang;
    public void Change()
    {
        Localization.ChangeLang(lang);
        SettingsMenu.lang = (int)lang;
        UniqueKnifes uniqueKnife = SelectKnife.Selected.GetComponent<SelectKnife>().UniqueKnifes;
        KnifePerks.Perk(uniqueKnife.knifePerk, (int)uniqueKnife.perkLevel, out string discription);
        KnifeActive.knifeActive.Discription.SetText(uniqueKnife.KnifeName + "\n" + discription);
    }
    
}
