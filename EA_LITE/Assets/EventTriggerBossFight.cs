using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggerBossFight : MonoBehaviour
{
    [SerializeField] int bossID;
    [SerializeField] GameObject triggerBox;


    private void OnTriggerEnter(Collider other)
    {
        AIBossCharacterManager boss = WorldAIManager.instance.GetBossCharacterByID(bossID);

        if(boss != null)
        {
            boss.WakeBoss();
        }

        GameObject.Destroy(triggerBox);
    }
}
