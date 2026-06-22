using UnityEngine;

public class BotNeeds : MonoBehaviour
{
    private BotController bot;

    public float thirst;

    public float comfort;

    public float bladder;

    [Header("Patience")]
    public float maxPatience = 100f;
    public float currentPatience = 100f;

    public float patienceRecoveryRate = 2f;
    public float patienceLossRate = 10f;

    private void Awake()
    {
        bot = GetComponent<BotController>();
    }

    private void Update()
    {
        thirst +=
            Time.deltaTime * 2f;

        comfort +=
            Time.deltaTime * 1f;

        bladder +=
            Time.deltaTime * 0.5f;

        thirst =
            Mathf.Clamp(
                thirst,
                0,
                100
            );

        comfort =
            Mathf.Clamp(
                comfort,
                0,
                100
            );

        bladder =
            Mathf.Clamp(
                bladder,
                0,
                100
            );
    }
}