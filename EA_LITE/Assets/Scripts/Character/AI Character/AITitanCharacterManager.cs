using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITitanCharacterManager : AIBossCharacterManager
{
    public AIBossSoundFXManager bossSoundFXManager;

    protected override void Awake()
    {
        base.Awake();

        bossSoundFXManager = GetComponent<AIBossSoundFXManager>();
    }
}
