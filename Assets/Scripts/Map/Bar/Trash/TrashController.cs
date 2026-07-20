using UnityEngine;

public class TrashController : MonoBehaviour
{
    [SerializeField]
    private TrashUIManager trashUI;

    [SerializeField]
    private TrashVisualController trashVisualController;

    public bool PlayerInside
    {
        get;
        private set;
    }

    public void PlayerEntered()
    {
        PlayerInside = true;

        trashUI.Show();

        GameDataBase.Instance.actionUI.Show(
            "Tirar\nBebida",
            true);

        trashVisualController.SetFocused(PlayerInside);
    }

    public void PlayerExited()
    {
        PlayerInside = false;

        trashUI.Hide();

        GameDataBase.Instance.actionUI.Hide();

        trashVisualController.SetFocused(PlayerInside);
    }
}