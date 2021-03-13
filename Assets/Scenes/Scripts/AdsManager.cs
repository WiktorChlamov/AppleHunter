using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    static bool? shop = null;
    string placement = "Rewarded_Android";
    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize("4034227", false);
    }
    public void ShowContinueAdd()
    {
        shop = false;
        Advertisement.Show(placement);
    }
    public void ShowShopAdd()
    {
        shop = true;
        Advertisement.Show(placement);
    }
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        { if (shop == true)
            {
                Game.Data.AdsCount++;
                Game.KnifeActive.knifeActive.AdsCount.text = Game.Data.AdsCount.ToString();
                Game.Data.Save();
                shop = null;
                return;
            }
            else if (shop == false)
            {
                Game.UiKnivesAndScoreEvents.uiKnivesAndScore.RestartLevel();
                shop = null;
            }
        }
    }
    public void OnUnityAdsDidError(string message)
    {
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    public void OnUnityAdsReady(string placementId)
    {
    }
}
