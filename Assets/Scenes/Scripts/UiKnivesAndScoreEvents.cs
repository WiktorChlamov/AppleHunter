using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using static Settings;
using static Localization;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Experimental.Rendering.Universal;

namespace Game
{
    public class UiKnivesAndScoreEvents : MonoBehaviour
    {
        public static UiKnivesAndScoreEvents uiKnivesAndScore;
        [HideInInspector]
        public UnityEvent appleHitEvent = new UnityEvent(),
            onFireEvent = new UnityEvent(),
            onWheelHitEvent = new UnityEvent(),
            OnKnifeHit = new UnityEvent();
        private int stageHits;
        private Coroutine timer;
        private static int extraScore;
        private static int heart = 0;
        private static float abilityCharge;
        private static float abilityMod;
        private int stageApplesScore;
        [SerializeField] private UIObjects uIObjects;
        [SerializeField] private GameObjects gameObjects;
        public Coroutine Timer { get => timer; set => timer = value; }
        public static int ExtraScore { get => extraScore; set => extraScore = value; }
        public static int Heart { get => heart; set => heart = value; }
        public static float StageHeartPoints { get; set; }
        public static float ExtraHealthPoints { get; set; }
        public static float AbilityCharge { get => abilityCharge; set => abilityCharge = value; }
        public static float AbilityMod { get => abilityMod; set => abilityMod = value; }
        public static void SetBase()
        {
            ExtraScore = 0;
            Heart = 0;
            ExtraHealthPoints = 0;
        }
        private UiKnivesAndScoreEvents()
        {
            uiKnivesAndScore = this;
        }

        private void Awake()
        {
            appleHitEvent.AddListener(AppleHitting);
            onFireEvent.AddListener(Shot);
            onWheelHitEvent.AddListener(WheelHit);
            OnKnifeHit.AddListener(KnifeHit);
            
        }
        private void Start()
        {
            if (abilityMod == 1)
            {
                uIObjects.PerkButton.GetComponent<Image>().fillAmount = 0;
                Material material = uIObjects.PerkButton.GetComponent<Image>().material;
                material.SetFloat("_OutlineSick", 0);
            }
        }
        private void Shot()
        {
            PlaySound(GameObjects.gameObjects.KnifeThrow);
            if (Knifes > 0)
            {
                Color color = new Color { a = 0.4f };
                RandomKnifesAndApple.AllUIKnifes[RandomKnifesAndApple.AllUIKnifes.Length - Knifes].GetComponent<Image>().color = color;
            }
        }
        private void AppleHitting()
        {
            Charging(true);
            gameObjects.CurrentApple.GetComponent<Light2D>().enabled = false;
            PlaySound(gameObjects.AppleHitting);
            // Vibration.VibratePop();
            stageApplesScore += 6 + extraScore;
            int wholeScore = stageApplesScore + Data.Score;
            uIObjects.ScoreUI.GetComponent<TextMeshProUGUI>().SetText(Translate((int)Other.Score) + wholeScore);
            foreach (Transform piece in gameObjects.CurrentApple.GetComponent<FruitPB>().Pieces)
            {
                Vector2 vector2 = piece.position - gameObjects.KnifeSpawnPoint.position;
                piece.SetParent(null);
                if (!piece.gameObject.TryGetComponent(out Rigidbody2D rb))
                {
                   rb = piece.gameObject.AddComponent<Rigidbody2D>();
                }
                rb.AddForce(vector2 * 50);
                Destroy(piece.gameObject, 2f);
            }
        }
        private void WheelHit()
        {
            Charging();
            Materials.WheelGlow = true;
            Materials.Glow = 5;
            //Vibration.Vibrate(200);
            PlaySound(gameObjects.WheelHitting);
            Data.CurrentHits++;
            stageHits++;
            uIObjects.HitCount.GetComponent<TextMeshProUGUI>().SetText(Translate((int)Other.Hitting) + Data.CurrentHits);
            if (stageHits == RandomKnifesAndApple.AllUIKnifes.Length)
            {
                Data.Save();
                stageHits = 0;

                if (Data.CurrentStage == 80)
                {
                    Data.Score += stageApplesScore;
                    stageApplesScore = 0;
                    Data.Save();
                    Win();
                    return;
                }
                NextLevel();
            }
            GameObjects.gameObjects.Particle.GetComponent<ParticleSystem>().Play();
        }
        private void KnifeHit()
        {
            stageApplesScore = 0;
            PlaySound(gameObjects.KnifeHitting);
            Data.Save();
            if (StageHeartPoints == 1)
            {
                StageHeartPoints = 0;
                if (abilityMod == 1)
                {
                    Image image = uIObjects.PerkButton.GetComponent<Image>();
                    image.material.SetFloat("_OutlineSick", 0);
                    image.fillAmount = 0;
                }
                Timer = StartCoroutine(Delay(() => RestartLevel(), 1));
                return;
            }
            if(Heart > 0)
            {
                Heart--;
                Timer = StartCoroutine(Delay(() => RestartLevel(), 1));
                return;
            }
            // Vibration.Vibrate(200);
            Timer = StartCoroutine(Delay(() => TheEnd(),1));
        }
        public void ContinueAfterWin()
        {
            NextLevel();
        }
        private void NextLevel()
        {   
            
            Data.Score += stageApplesScore;
            stageApplesScore = 0;
            Data.Save();
            Data.CurrentStage++;
            Timer = StartCoroutine(Delay(() => LevelInst.levelInst.Devastate(), 1));
            gameObjects.Glow.gameObject.SetActive(false);
            CrackWoodAndKnifes();
        }
        public void CrackWoodAndKnifes()
        {
            foreach (Transform piece in gameObjects.WoodPieces)
            {
                piece.SetParent(null);
                Rigidbody rb = piece.GetComponent<Rigidbody>();
                rb.AddExplosionForce(500, gameObjects.KnifeSpawnPoint.position, 200);
                rb.useGravity = true;
            }
            foreach (Transform piece in GameObjects.gameObjects.KnifesInWood)
            {
                piece.GetComponent<PolygonCollider2D>().enabled = false;
                piece.SetParent(null);
                Vector2 vector2 = piece.position - gameObjects.KnifeSpawnPoint.position;
                Rigidbody2D rb = piece.GetComponent<Rigidbody2D>();
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.AddForce(vector2 * 90);
                rb.gravityScale = 1;
                piece.GetComponentInChildren<SpriteRenderer>().material = Materials.materials.DissolveDawn;
                Materials.IsDissolveDawn = true;
            }
            foreach (Transform piece in gameObjects.Apples)
            {
                if (piece != null)
                {
                    piece.GetComponent<Light2D>().enabled = false;
                    piece.SetParent(null);
                    piece.GetComponent<Rigidbody2D>().gravityScale = 1;
                    piece.GetComponent<CircleCollider2D>().enabled = false;
                }
            }
        }
        public void Win()
        {
            uIObjects.WinMenu.gameObject.SetActive(true);
            gameObjects.GameField.gameObject.SetActive(false);
        }
        public IEnumerator Delay(Action action, float timer)
        {
            yield return new WaitForSeconds(timer);
            action();
        }
        private void TheEnd()
        {
            gameObjects.GameField.gameObject.SetActive(false);
            uIObjects.EndMenu.gameObject.SetActive(true);
            uIObjects.EndMenuStage.GetComponent<TextMeshProUGUI>().
          SetText(Translate((int)Other.Stage) + Data.CurrentStage + "\n" + Translate((int)Record.StageRecord) + Data.StageRecord);
            uIObjects.EndMenuHits.GetComponent<TextMeshProUGUI>().
           SetText(Translate((int)Other.Hitting) + Data.CurrentHits + "\n" + Translate((int)Record.HitRecord) + Data.HitRecord);
        }
        public void MainMenu()
        {
            StageHeartPoints = 0;
            SceneManager.LoadScene("Menu");
            Materials.AmmountDissolve = 0;
            Materials.IsDissolve = false;
            Materials.AmmountDissolveDawn = 1f;
        }
        public void Charging(bool apple = false)
        {   
            if(apple)
            {
                abilityCharge += abilityMod *2;
                StageHeartPoints = StageHeartPoints + 0.04f + ExtraHealthPoints * 2;
            }
            else
            {
                abilityCharge += abilityMod;
                StageHeartPoints = StageHeartPoints + 0.02f + ExtraHealthPoints;
            }
            Image perkButton = uIObjects.PerkButton.GetComponent<Image>();
            Image stageHeart = RandomKnifesAndApple.StageHeart.GetComponent<Image>();
            abilityCharge = Mathf.Clamp01(abilityCharge);
            StageHeartPoints = Mathf.Clamp01(StageHeartPoints);
            perkButton.fillAmount = abilityCharge;
            stageHeart.fillAmount = StageHeartPoints;
            if (StageHeartPoints == 1)
            {
                Color color = new Color (255,255,255,1);
                stageHeart.color = color;
            }
            if(abilityMod ==1 )
            {
                abilityCharge = StageHeartPoints;
                perkButton.fillAmount = StageHeartPoints;
                if (StageHeartPoints == 1)
                {
                    Material material = perkButton.material;
                    material.SetFloat("_OutlineSick", 1);
                    return;
                }

            }
            if(abilityCharge == 1)
            {
                Material material = perkButton.material;
                material.SetFloat("_OutlineSick", 1);
            }
        }
        public void RestartLevel()
        {
            Data.CurrentHits -= stageHits;
            stageHits = 0;
            LevelInst.levelInst.Devastate();
            uIObjects.EndMenu.gameObject.SetActive(false);
        }
        private void PlaySound(AudioClip audioClip)
        {
            gameObjects.Sound.clip = audioClip;
            gameObjects.Sound.Play();
        }
    }
}
