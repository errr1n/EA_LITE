using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectsManager : CharacterEffectsManager
{
    [Header("Debug Delete Later")]
    [SerializeField] InstantCharacterEffect effectToTest;
    [SerializeField] bool processEffect = false;

    private void Update()
    {
        if(processEffect)
        {
            processEffect = false;
            // INSTANTIATING A COPY OF THIS, INSTEAD OF JUST USING IT AS IT IS (ORIGINAL IS NOT EFFECTED)
            InstantCharacterEffect effect = Instantiate(effectToTest);
            ProcessInstantEffect(effect);
        }
    }
}
