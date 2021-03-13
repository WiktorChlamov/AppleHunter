using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using static Localization;
using UnityEngine.Rendering;

namespace Game
{
    public class Loading : MonoBehaviour
    {
        [SerializeField]
        private Transform recordAndScore, audioPB,materials;
        public static GameObject audioSource, materialsGO;
        [SerializeField] private List<UniqueKnifes> uniqueKnifes;
        private void Awake()
        {
            if (materialsGO == null)
            {
                materialsGO = materials.gameObject;
                DontDestroyOnLoad(materialsGO);
            }
            if (audioSource == null)
            {
                audioSource = Instantiate(audioPB.gameObject);
                DontDestroyOnLoad(audioSource);
            };
            gameObject.SetActive(true);
            Data.Load();
            AudioListener.pause = !Data.Sound;
            Camera.main.GetComponent<Volume>().enabled = Data.Bloom;
            SettingsMenu.Set(Data.Sound, Data.Lang_id);
            KnifeActive.knifeActive.AdsCount.text = Data.AdsCount.ToString();
            ChangeLang((Lang)Data.Lang_id);
            LoadKnifes();
            UpdateStats();
        }
        private void LoadKnifes()
        {
            foreach (UniqueKnifes unique in uniqueKnifes)
            {
                if (Data.ByedKnifes.Contains(unique.Id))
                {
                    KnifeActive.knifeActive.BuyedKnifes.Add(unique);
                }
                else
                {
                    KnifeActive.knifeActive.UnBuyedKnifes.Add(unique);
                }
            }
        }
        public void Bloom()
        {
            Data.Bloom = !Data.Bloom;
            Camera.main.GetComponent<Volume>().enabled = Data.Bloom;
        }
        public void Save()
        {   
            Data.Lang_id = (int)lang;
            Data.Sound = SettingsMenu.sound;
            Data.Save();
        }
        public void UpdateStats()
        {   
            TextMeshProUGUI textMeshProUGUI = recordAndScore.GetComponent<TextMeshProUGUI>();
            textMeshProUGUI.SetText(Translate((int)Record.StageRecord) +
            Data.StageRecord + "\n" + Translate((int)Record.HitRecord) +
            Data.HitRecord + "\n" + Translate((int)Other.Score) + Data.Score);
        }
        public void NextScene()
         {
            SceneManager.LoadScene("1");
         }
    }
    
}
