using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Materials : MonoBehaviour
{   

    public static Materials materials;
    [SerializeField] private Material normal, dissolve,dissolveDawn, wheelMat, shield, moveLine, buyButton;
    [SerializeField] private Material[] appleMat;
    [SerializeField] private Material normalKnife, rareKnife, uniqueKnifeMat;
    private static bool isDissolve = false,isDissolveDawn = false,wheelGlow = false, appleDiss = false;
    private static float glow = 5;
    private static float ammountDissolve, appleDissTime;
    private static float ammountDissolveDawn = 1f;
    [SerializeField] private UniqueKnifes uniqueKnife;
    public Material Normal { get => normal; set => normal = value; }
    public Material Dissolve { get => dissolve; set => dissolve = value; }
    public static bool IsDissolve { get => isDissolve; set => isDissolve = value; }
    public static float AmmountDissolve { get => ammountDissolve; set => ammountDissolve = value; }
    public Material DissolveDawn { get => dissolveDawn; set => dissolveDawn = value; }
    public static bool IsDissolveDawn { get => isDissolveDawn; set => isDissolveDawn = value; }
    public UniqueKnifes UniqueKnife { get => uniqueKnife; set => uniqueKnife = value; }
    public static float AmmountDissolveDawn { get => ammountDissolveDawn; set => ammountDissolveDawn = value; }
    public Material WheelMat { get => wheelMat; set => wheelMat = value; }
    public static bool WheelGlow { get => wheelGlow; set => wheelGlow = value; }
    public static float Glow { get => glow; set => glow = value; }
    public Material Shield { get => shield; set => shield = value; }
    public Material[] AppleMat { get => appleMat; set => appleMat = value; }
    public static bool AppleDiss { get => appleDiss; set => appleDiss = value; }
    public Material NormalKnife { get => normalKnife; set => normalKnife = value; }
    public Material RareKnife { get => rareKnife; set => rareKnife = value; }
    public Material UniqueKnifeMat { get => uniqueKnifeMat; set => uniqueKnifeMat = value; }
    public Material MoveLine { get => moveLine; set => moveLine = value; }
    public Material BuyButton { get => buyButton; set => buyButton = value; }

    private Materials()
    {
        materials = this;
    }
    private void FixedUpdate()
    {   if (WheelGlow)
        {   
            Glow = Mathf.Clamp(Glow - Time.deltaTime*10,0,5);
            wheelMat.SetFloat("_OutlineSick", Glow);
            if (Glow <= 0)
            {
                WheelGlow = false;
                Glow = 5;
            }
        }
    if (AppleDiss)
        {
            appleDissTime = Mathf.Clamp01(appleDissTime + Time.deltaTime);
            foreach(Material material in appleMat)
            {
                material.SetFloat("_DissolveAmmount", appleDissTime);
            }
            if (appleDissTime == 1)
            {
                appleDiss = false;
                appleDissTime = 0;
                foreach (Transform gameObject in GameObjects.gameObjects.Apples)
                {
                    SpriteRenderer[] sprites = gameObject.GetComponentsInChildren<SpriteRenderer>();
                    if (sprites.Length > 0)
                    {
                        sprites[0].material = normal;
                        sprites[1].material = normal;
                    }
                }
                foreach (Material material in appleMat)
                {
                    material.SetFloat("_DissolveAmmount", 0);
                }
            }
        }

        if (IsDissolve)
        {
            AmmountDissolve = Mathf.Clamp01(AmmountDissolve + Time.deltaTime);
            dissolve.SetFloat("_DissolveAmmount", AmmountDissolve);
        }
        if (isDissolveDawn)
        {
            AmmountDissolveDawn = Mathf.Clamp01(AmmountDissolveDawn - Time.deltaTime);
            dissolveDawn.SetFloat("_DissolveAmmount", AmmountDissolveDawn);
            if (AmmountDissolveDawn ==0)
            {
                AmmountDissolveDawn = 1;
                isDissolveDawn = false;
            }
        }
    }


}

