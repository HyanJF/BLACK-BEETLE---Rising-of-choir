using System.Collections.Generic;
using UnityEngine;

public class RoundMusic : MonoBehaviour
{
    [SerializeField]
    private MusicTrack[] tracks;

    private readonly List<MusicTrack> availableTracks =
        new();

    private readonly List<MusicTrack> usedTracks =
        new();

    private void Awake()
    {
        RefillPool();
    }

    public void PlayRandomMusic()
    {
        if (availableTracks.Count == 0)
            RefillPool();

        int index =
            Random.Range(0, availableTracks.Count);

        MusicTrack track =
            availableTracks[index];

        availableTracks.RemoveAt(index);

        usedTracks.Add(track);

        GameDataBase.Instance.soundController.PlayMusic(track.clip);

        GameDataBase.Instance.musicUI.Show(track.displayName);
    }

    public void StopMusic()
    {
        GameDataBase.Instance.soundController.StopMusic();
    }

    private void RefillPool()
    {
        availableTracks.Clear();

        availableTracks.AddRange(usedTracks);

        usedTracks.Clear();

        foreach (MusicTrack track in tracks)
        {
            if (!availableTracks.Contains(track))
                availableTracks.Add(track);
        }
    }
}