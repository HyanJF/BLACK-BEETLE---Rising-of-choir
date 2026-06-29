using System.Collections;
using UnityEngine;

public class BotThoughtVisual : MonoBehaviour
{
    public SpriteRenderer botSprite;
    public Animator anim;

    private float timeAnim;
    private string thoughtText;


    private void Awake()
    {
        HideSprite();
    }

    public void Anim(
    ThoghtType thought,
    float time)
    {
        timeAnim = time;

        thoughtText = thought switch
        {
            ThoghtType.Drink => "Bar",
            ThoghtType.Sit => "Table",
            ThoghtType.Bladder => "Bathroom",
            ThoghtType.Angry => "Angry",
            ThoghtType.Happy => "Happy",
            _ => "Idle"
        };

        StopAllCoroutines();

        StartCoroutine(
            ThoughtAnim()
        );
    }

    IEnumerator ThoughtAnim()
    {
        ShowSprite();

        if (anim.gameObject.activeInHierarchy)
            anim.Play(thoughtText);

        yield return new WaitForSeconds(timeAnim);

        HideSprite();

        if (anim.gameObject.activeInHierarchy)
            anim.Play("Idle");
    }

    public void HideSprite()
    {
        Color color = botSprite.color;
        color.a = 0f;
        botSprite.color = color;
    }

    public void ShowSprite()
    {
        Color color = botSprite.color;
        color.a = 1f;
        botSprite.color = color;
    }

    public void DisableThought()
    {
        StopAllCoroutines();
        HideSprite();

        if (anim != null &&
            anim.gameObject.activeInHierarchy)
        {
            anim.Play("Idle");
        }
    }
}
