using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMusicManager : MonoBehaviour
{
    [Header("Looping Music Options")]
    [SerializeField] protected AudioClip[] musicOptions;
    public int choiceIndex;

    private AudioSource musicPlayer;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        musicPlayer = GetComponent<AudioSource>();
    }
    void Start()
    {
        PlayMusic(choiceIndex);
        
        
    }

    public void PlayMusic(int musIndex)
    {
        if (choiceIndex < musicOptions.Length && choiceIndex > -1)
        {
            AudioClip option = musicOptions[musIndex];
            musicPlayer.clip = option;
            musicPlayer.Play(0);
        }
        
    }
}
