using UnityEngine;

public class GameDataBase : MonoBehaviour
{
    public static GameDataBase Instance;

    public DrinkDatabase drinkData;

    [Header("UI Systems")]
    public BarStockUIManager stockUI;
    public BarCustomerUI customerUI;
    public ActionUI actionUI;
    public DialogueUI dialogueUI;
    public DialogueDatabase dialogueData;
    public DialogueTimingDatabase dialogueTiming;
    public SoundController soundController;
    private void Awake()
    {
        Instance = this;
    }
}
