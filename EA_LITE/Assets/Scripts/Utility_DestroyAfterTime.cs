using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility_DestroyAfterTime : MonoBehaviour
{
    // String to hold timer
    [SerializeField] float timeUntilDestroyed = 5;

    private void Awake()
    {
        // destory object after timer
        Destroy(gameObject, timeUntilDestroyed);
    }
}
