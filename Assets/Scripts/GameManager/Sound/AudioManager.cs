using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


/*CODE REFERENCE: https://www.youtube.com/watch?v=6OT43pvUyfY&ab_channel=Brackeys */
//This class handles the music in the game, wont be destoryed on scene switch.
public class AudioManager : MonoBehaviour
{

    public Sound[] sounds; //array of Sound object to add easily to inspector.

    public static AudioManager instance;

    [HideInInspector]
    public bool isMuted = false;


    //On awake we want to be sure there is only one audiomanager, so if there isnt one we make one and if there is one we delete this one.
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
       
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

        }
    }



    //Method gets a music name as parameter and plays it. Checks if name is null for peace of mind.
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            return;
        }
        s.source.Play();
    }


    //Method gets a music name as parameter and stops playing it. Checks if name is null for peace of mind.
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Stop();
    }


    //Code below is not referenced!
    //MuteMusic sets all volumes to 0 and changes isMuted variable.
    public void MuteMusic()
    {
        isMuted = !isMuted;
        for (int i = 0; i < sounds.Length; i++)
        {
           sounds[i].source.volume = 0f;
        }

    }


    //UnMuteMusic sets volume for specific value that is wanted for a specific music. Changes isMuted variable.
    public void UnMuteMusic()
    {
        isMuted = !isMuted;
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == "MainMenuMusic")
            {
                sounds[i].source.volume = 0.3f;
            }
            else if (sounds[i].name == "InfinityMusic")
            {
                sounds[i].source.volume = 0.1f;
            }
            else if (sounds[i].name == "StoryMusic")
            {
                sounds[i].source.volume = 0.1f;
            }
        }
    }
}
