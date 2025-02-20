using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Boss_HP_Bar : UI_StatBar
{
    // get the boss character
    [SerializeField] AIBossCharacterManager bossCharacter;

    public void EnableBossHPBar(AIBossCharacterManager boss)
    {
        bossCharacter = boss;

        SetMaxStat(bossCharacter.characterStatsManager.maxHealth);
        SetStat(bossCharacter.characterStatsManager.CurrentHealth);
        GetComponentInChildren<TextMeshProUGUI>().text = bossCharacter.characterName;
    }

    public void SetBossHP(float newValue)
    {
        SetStat(newValue);

        if(newValue <= 0)
        {
            RemoveHPBar(2.5f);
        }
    }

    public void RemoveHPBar(float time)
    {
        Destroy(gameObject, time);
    }
}
