using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    [SerializeField]
    private BotController botPrefab;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private SpawnEntry[] customerPool;

    [SerializeField]
    private float spawnInterval = 5f;

    [SerializeField]
    private int maxCustomers = 20;

    [SerializeField]
    private ProfileManager profileManager;

    [SerializeField]
    private List<BotController> activeBots =
        new();

    [SerializeField]
    private Queue<BotController> pooledBots =
        new();

    [SerializeField]
    private SpawnBotsSounds sounds;

    private Coroutine spawnRoutine;

    #region AlphaSystem
    public bool IsSpawning =>
        spawnRoutine != null;

    public void StartSpawn()
    {
        if (spawnRoutine != null)
            return;

        spawnRoutine =
            StartCoroutine(SpawnRoutine());
    }

    public void StopSpawn()
    {
        if (spawnRoutine == null)
            return;

        StopCoroutine(spawnRoutine);
        spawnRoutine = null;
    }

    #endregion

    public void SpawnBot()
    {
        CustomerTypeSO type =
            GetRandomCustomerType();

        CustomerProfileSO profile =
            profileManager.GetRandomProfile();

        BotController bot =
            GetBot();

        bot.transform.position =
            spawnPoint.position;

        bot.ActivateBot(
            type, 
            profile);

        activeBots.Add(bot);

        sounds.PlaySpawn();
    }

    private BotController GetBot()
    {
        if (pooledBots.Count > 0)
        {
            return pooledBots.Dequeue();
        }

        BotController bot =
            Instantiate(botPrefab);

        bot.OnBotFinished +=
            HandleBotFinished;

        return bot;
    }

    private void HandleBotFinished(
        BotController bot)
    {
        ReturnBot(bot);
    }

    private void ReturnBot(
        BotController bot)
    {
        activeBots.Remove(bot);

        bot.VisualController.HideEverything();

        bot.DeactivateBot();

        pooledBots.Enqueue(bot);

        sounds.PlayDespawn();

        if (activeBots.Count == 0 &&
            !GameDataBase.Instance
                .roundManager
                .IsRoundActive)
        {
            GameDataBase.Instance
                .roundManager
                .RoundCompleted();
        }
    }

    private CustomerTypeSO GetRandomCustomerType()
    {
        int totalWeight = 0;

        foreach (SpawnEntry entry in customerPool)
        {
            totalWeight += entry.weight;
        }

        int randomValue =
            Random.Range(
                0,
                totalWeight
            );

        int currentWeight = 0;

        foreach (SpawnEntry entry in customerPool)
        {
            currentWeight += entry.weight;

            if (randomValue < currentWeight)
            {
                return entry.customerType;
            }
        }

        return customerPool[0].customerType;
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            if (
                activeBots.Count <
                maxCustomers
            )
            {
                SpawnBot();
            }

            yield return
                new WaitForSeconds(
                    spawnInterval
                );
        }
    }
}