using UnityEngine;

public class BotNeeds : MonoBehaviour
{
    private BotController bot;

    public float thirst;
    public float comfort;
    public float bladder;

    [Header("Maximum")]
    public float maxThirst = 100f;
    public float maxComfort = 100f;
    public float maxBladder = 100f;

    [Header("Increase Per Second")]
    public float thirstRate = 2f;
    public float comfortRate = 1f;
    public float bladderRate = 0.5f;

    [Header("Patience")]
    public float maxPatience = 100f;
    public float currentPatience = 100f;

    public float patienceRecoveryRate = 2f;
    public float patienceLossRate = 10f;

    [Header("Percentages")]
    public float ThirstPercent =>
        maxThirst <= 0f ? 0f : thirst / maxThirst;

    public float ComfortPercent =>
        maxComfort <= 0f ? 0f : comfort / maxComfort;

    public float BladderPercent =>
        maxBladder <= 0f ? 0f : bladder / maxBladder;

    private void Awake()
    {
        bot = GetComponent<BotController>();
    }

    private void Update()
    {
        thirst +=
            Time.deltaTime * thirstRate;

        comfort +=
            Time.deltaTime * comfortRate;

        bladder +=
            Time.deltaTime * bladderRate;

        thirst =
            Mathf.Clamp(
                thirst,
                0,
                maxThirst);

        comfort =
            Mathf.Clamp(
                comfort,
                0,
                maxComfort);

        bladder =
            Mathf.Clamp(
                bladder,
                0,
                maxBladder);
    }
}