using UnityEngine;


class AudioManager : Singleton<AudioManager>
{
    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayAudio(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    public void StopAudio()
    {
        source.Stop();
    }
}