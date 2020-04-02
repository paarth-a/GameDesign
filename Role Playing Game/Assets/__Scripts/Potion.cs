using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public PotionType potionType;
    public static float value = 5;

    public enum PotionType
    {
        HEALTH,
        ENERGY
    }
}