using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogWallInteractable : MonoBehaviour
{
    [Header("Fog")]
    [SerializeField] GameObject[] fogGameObjects;

    [Header("I.D")]
    public int fogWallID;

    [Header("Active")]
    [SerializeField] bool _isActive = false;
    public bool IsActive{
        get{return _isActive;}
        set{
            OnIsActiveChanged(_isActive, value);
            _isActive = value;
            WorldObjectManager.instance.AddFogWallToList(this);
        }
    }

    private void OnIsActiveChanged(bool oldStatus, bool newStatus)
    {
        if(IsActive)
        {
            foreach(var fogObject in fogGameObjects)
            {
                fogObject.SetActive(true);
            }
        }
        else
        {
            foreach(var fogObject in fogGameObjects)
            {
                fogObject.SetActive(false);
            }
        }
    }
}
