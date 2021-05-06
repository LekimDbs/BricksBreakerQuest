using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientChange : MonoBehaviour
{
    public Sprite GradientWhite;
    private Sprite MainSprite;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        MainSprite = sr.sprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ball")
        {
            sr.sprite = GradientWhite;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ball")
        {
            sr.sprite = MainSprite;
        }
    }
}
