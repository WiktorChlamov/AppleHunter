using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIObjects : MonoBehaviour
{
    [SerializeField] Canvas canvas;
   [SerializeField] Transform winMenu, background, bossAnoun,continueButton, stageHealth,heartPF,heartParent, knifesUI, scoreUI,
        knifesPrefubUI, hitCount,endMenu,gameUI,stageUI,endMenuStage, endMenuHits, perkButton;
    public static UIObjects objectsUI;
    [SerializeField] private Sprite bossFon, normalFon; 
    public Transform KnifesUI { get => knifesUI; set => knifesUI = value; }
    public Transform ScoreUI { get => scoreUI; set => scoreUI = value; }
    public Transform KnifesPrefubUI { get => knifesPrefubUI; set => knifesPrefubUI = value; }
    public Transform HitCount { get => hitCount; set => hitCount = value; }
    public Transform EndMenu { get => endMenu; set => endMenu = value; }
    public Transform GameUI { get => gameUI; set => gameUI = value; }
    public Transform StageUI { get => stageUI; set => stageUI = value; }
    public Transform EndMenuStage { get => endMenuStage; set => endMenuStage = value; }
    public Transform EndMenuHits { get => endMenuHits; set => endMenuHits = value; }
    public Canvas Canvas { get => canvas; set => canvas = value; }
    public Transform PerkButton { get => perkButton; set => perkButton = value; }
    public Transform HeartPF { get => heartPF; set => heartPF = value; }
    public Transform HeartParent { get => heartParent; set => heartParent = value; }
    public Transform StageHealth { get => stageHealth; set => stageHealth = value; }
    public Transform ContinueButton { get => continueButton; set => continueButton = value; }
    public Transform BossAnoun { get => bossAnoun; set => bossAnoun = value; }
    public Sprite BossFon { get => bossFon; set => bossFon = value; }
    public Sprite NormalFon { get => normalFon; set => normalFon = value; }
    public Transform Background { get => background; set => background = value; }
    public Transform WinMenu { get => winMenu; set => winMenu = value; }

    private UIObjects()
    {
        objectsUI = this;
    }

}
