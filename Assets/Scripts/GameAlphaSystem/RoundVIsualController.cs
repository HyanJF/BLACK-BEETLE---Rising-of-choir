using UnityEngine;

public class RoundVisualController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer startRoundVisual;

    [SerializeField]
    private MonoBehaviour interactionComponent;

    public void StartRoundVisuals()
    {
        startRoundVisual.enabled = false;

        interactionComponent.enabled = false;
    }

    public void ResetVisuals()
    {
        startRoundVisual.enabled = true;

        interactionComponent.enabled = true;
    }
}