using UnityEngine;

public class GameDataBase : MonoBehaviour
{
    public static GameDataBase instance;

    public DrinkDatabase drinkData;

    private void Awake()
    {
        instance = this;
    }
}
