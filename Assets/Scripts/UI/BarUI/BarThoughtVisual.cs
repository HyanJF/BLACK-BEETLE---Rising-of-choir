using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class BarThoughtVisual : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Animator animator;

    private void Awake()
    {
        Hide();
    }

    public void Show(
    ThoughtType thought,
    DrinkType drink)
    {
        spriteRenderer.enabled = true;

        string state = thought switch
        {
            ThoughtType.Greeting => "Greeting",

            ThoughtType.Ordering =>
                drink == DrinkType.None
                    ? null
                    : drink.ToString(),

            ThoughtType.EmptyHands => "EmptyHands",

            ThoughtType.Waiting => "Waiting",

            ThoughtType.Angry => "Angry",

            ThoughtType.Leaving => "Leaving",

            ThoughtType.Served => "Served",

            _ => null
        };

        if (string.IsNullOrEmpty(state))
        {
            Hide();
            return;
        }

        animator.Play(state);
    }

    public void Hide()
    {
        spriteRenderer.enabled = false;
    }
}