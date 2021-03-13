using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Lights : MonoBehaviour
{
    public static Lights lights;
    [SerializeField] private Light2D main, up, dawn,knife, objects, perkButtonLight;
    public Light2D Main { get => main; set => main = value; }
    public Light2D Up { get => up; set => up = value; }
    public Light2D Dawn { get => dawn; set => dawn = value; }
    public Light2D Knife { get => knife; set => knife = value; }
    public Light2D Objects { get => objects; set => objects = value; }
    public Light2D PerkButtonLight { get => perkButtonLight; set => perkButtonLight = value; }

    private Lights ()
    {
        lights = this;
    }
}
