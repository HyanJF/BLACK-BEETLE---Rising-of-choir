using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotThoughtVisual : MonoBehaviour
{
    private ThoghtType tType;

    private SpriteRenderer botSprite;
    private Color colorS;
    private Animator anim;
    private float timeAnim;
    private string thoughtText;


    private void Start()
    {
        botSprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        HideSprite();
    }

    public void Anim(ThoghtType thoght, float time)
    {
        timeAnim = time;
        switch (thoght)
        {
            case ThoghtType.Drink:
                thoughtText = "Bar";
                StartCoroutine(ThoughtAnim());
                break;
            case ThoghtType.Sit:
                thoughtText = "Table";
                StartCoroutine(ThoughtAnim());
                break;
            case ThoghtType.Bladder:
                thoughtText = "Bathroom";
                StartCoroutine(ThoughtAnim());
                break;
            case ThoghtType.Angry:
                thoughtText = "Angry";
                StartCoroutine(ThoughtAnim());
                break;
        }

    }

    IEnumerator ThoughtAnim()
    {
        ShowSprite();
        anim.Play(thoughtText);
        yield return new WaitForSeconds(timeAnim);
        HideSprite();
        anim.Play("Idle");

    }

    public void HideSprite()
    {
        colorS = botSprite.color;
        colorS.a = 0f;
        botSprite.color = colorS;
    }    

    public void ShowSprite()
    {
        colorS.a = 1f;
        botSprite.color = colorS;
    }
}
