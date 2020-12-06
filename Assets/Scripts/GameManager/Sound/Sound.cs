using UnityEngine;

/*CODE REFERENCE: https://www.youtube.com/watch?v=6OT43pvUyfY&ab_channel=Brackeys */
//This class is used to make the audio clips more easily managed. Adding/deleting/changing values.
[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(0f,1f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
