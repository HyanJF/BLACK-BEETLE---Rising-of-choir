using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField]
    private AudioSource soundPlayer;

    [SerializeField]
    private AudioSource spawnBots;

    [SerializeField]
    private AudioSource interactionSounds;

    [Header("Sources")]
    [SerializeField]
    private AudioSource musicSource;

    public void PlaySoundPlayer(AudioClip clip)
    {
        soundPlayer.PlayOneShot(clip);
    }

    public void PlaySoundSpawn(AudioClip clip)
    {
        spawnBots.PlayOneShot(clip);
    }

    public void PlaySoundInteracion(AudioClip clip)
    {
        interactionSounds.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null)
            return;

        if (musicSource.clip == clip &&
            musicSource.isPlaying)
            return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
        musicSource.clip = null;
    }
}