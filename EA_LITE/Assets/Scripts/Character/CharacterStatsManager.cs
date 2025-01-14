using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsManager : MonoBehaviour
{
    public void CalculateStaminaBasedOnEnduranceLevel(int endurance)
    {
        float stamina = 0;

        // CREATE AN EQUATION FOR HOW YOU WANT YOUR STAMINA TO BE CALCULATED
        stamina = endurance * 10;
    }
}
