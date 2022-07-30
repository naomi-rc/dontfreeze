using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;
    private bool canAttack = true;

    void Awake()
    {
        canAttack = true;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (var sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    private void Start()
    {
        foreach(var s in sounds)
        {
            if(s.loop)
                Play(s.name);
        }        
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found");
            return;
        }
        if (name.Contains("Attack"))
        {
            if (canAttack)
            {
                s.source.Play();
            }
        }
        else
        {
            s.source.Play();
        }

        if (name == "Death")
        {
            canAttack = false;
        }

    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found");
            return;
        }
        s.source.Stop();
    }

    public void PlayMovingSound(){
        
        Location location = FindObjectOfType<Location>();

        if (location != null && location.gameObject.tag == "SafeHouseLocationExit")
        {
            Play("MovingSafeHouse");
        }
        else
        {
            Play("Moving");  
        }
    }
}