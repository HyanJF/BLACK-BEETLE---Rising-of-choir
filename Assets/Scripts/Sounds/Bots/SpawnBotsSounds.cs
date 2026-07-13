using UnityEngine;

public class SpawnBotsSounds : MonoBehaviour
{
    [SerializeField]
    private AudioClip botSpawn;

    [SerializeField]
    private AudioClip botDespawn;

    public void PlaySpawn()
    {
        GameDataBase.Instance.soundController.PlaySoundSpawn(botSpawn);
    }

    public void PlayDespawn()
    {
        GameDataBase.Instance.soundController.PlaySoundSpawn(botDespawn);
    }
}