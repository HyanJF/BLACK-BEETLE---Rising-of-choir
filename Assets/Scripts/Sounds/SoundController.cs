using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField]
    private AudioSource soundPlayer;

    [SerializeField]
    private AudioSource spawnBots;

    [SerializeField]
    private AudioSource interactionSounds;

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
}