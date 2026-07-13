using System.Collections;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField]
    private BotSpawner spawner;

    [SerializeField]
    private float firstRoundDuration = 120f;

    [SerializeField]
    private RoundVisualController visuals;

    [SerializeField]
    private RoundMusic music;

    [SerializeField]
    private GameObject lights;

    private bool roundEnding;

    public bool IsRoundActive
    {
        get;
        private set;
    }

    public void StartRound()
    {
        if (IsRoundActive)
            return;

        GameDataBase.Instance
            .roundTransitionUI
            .Play(
                "INICIANDO RONDA",
                "Prepárate para recibir clientes",
                BeginRound);
    }

    private IEnumerator RoundRoutine(float duration)
    {
        yield return new WaitForSeconds(duration);

        EndRound();
    }

    public void EndRound()
    {
        if (!IsRoundActive)
            return;

        IsRoundActive = false;

        spawner.StopSpawn();
    }

    public void RoundCompleted()
    {
        if (roundEnding)
            return;

        roundEnding = true;

        GameDataBase.Instance
            .roundTransitionUI
            .Play(
                "RONDA COMPLETADA",
                "El bar vuelve a estar vacío",
                FinishRound);
    }

    private void FinishRound()
    {
        roundEnding = false;

        music.StopMusic();

        lights.SetActive(false);

        visuals.ResetVisuals();
    }

    private void BeginRound()
    {
        IsRoundActive = true;

        lights.SetActive(true);

        music.PlayRandomMusic();

        spawner.StartSpawn();

        visuals.StartRoundVisuals();

        StartCoroutine(
            RoundRoutine(
                firstRoundDuration));
    }
}