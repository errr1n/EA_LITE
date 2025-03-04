using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallInteractable : MonoBehaviour
{
    [Header("Wall")]
    [SerializeField] GameObject[] wallGameObjects;
    public AudioClip impactSound;


    [Header("I.D")]
    public int wallID;

    [Header("Active")]
    [SerializeField] bool _isActive = false;
    public bool IsActive{
        get{return _isActive;}
        set{
            OnIsActiveChanged(_isActive, value);
            _isActive = value;
        }
    }
    private AudioSource audioSource;
    private bool soundPlayed = false;

    private void Update()
    {
        CheckIfActive();
    }

    private void OnEnable()
    {
        WorldObjectManager.instance.AddWallToList(this);

    }

    public void OnIsActiveChanged(bool oldStatus, bool newStatus)
    {
        // CheckIfActive();
        // Debug.Log("AN ACTIVE CAHNGE");
    }

    private void CheckIfActive()
    {
        if(IsActive)
        {
            foreach(var wallObject in wallGameObjects)
            {
                wallObject.SetActive(true);
                if(soundPlayed == false){
                    PlayImpactSound(); 
                    soundPlayed = true; 
                }
            }
        }
        else
        {
            foreach(var wallObject in wallGameObjects)
            {
                wallObject.SetActive(false);
            }
        }
    }

    public void PlayImpactSound()
    {
        audioSource = GetComponent<AudioSource>();
        if(audioSource != null){
            audioSource.clip = impactSound;
            audioSource.Play(0);

        }
    
    }
}
