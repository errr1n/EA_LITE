using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Boss_HP_Bar : UI_StatBar
{
    [SerializeField] AIBossCharacterManager bossCharacter;

    public void EnableBossHPBar(AIBossCharacterManager boss)
    {
        Debug.Log("enable hp bar");
        bossCharacter = boss;

        // bossCharacter.characterStatsManager.CurrentHealth += OnBossHPChanged;
        // PlayerUIManager.instance.playerUIHudManager.SetNewHealthValue(boss.characterStatsManager.CurrentHealth);
        // OnBossHPChanged(bossCharacter.characterStatsManager.CurrentHealth,)

        SetMaxStat(bossCharacter.characterStatsManager.maxHealth);
        SetStat(bossCharacter.characterStatsManager.CurrentHealth);
        GetComponentInChildren<TextMeshProUGUI>().text = bossCharacter.characterName;
    }

    private void OnDestroy()
    {
        //
    }

    private void OnBossHPChanged(int oldValue, int newValue)
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
