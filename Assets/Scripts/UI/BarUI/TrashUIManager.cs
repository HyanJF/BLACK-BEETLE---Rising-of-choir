using UnityEngine;

public class TrashUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject root;

    private void Awake()
    {
        Hide();
    }

    public void Show()
    {
        root.SetActive(true);
    }

    public void Hide()
    {
        root.SetActive(false);
    }
}