using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wall : MonoBehaviour
{
    public static int count;
    public static bool Shooting;
    public GameObject ballSpawner;
    private BallSpawner bs;
    public static bool firstHit;
    public static Vector3 NextPosition;
    public GameObject Text;
    private TextMeshPro tmp;
    public static int currentPoints;
    public GameObject FirstBall;
    private bool setText;

    private void Start()
    {
        count = 0;
        Shooting = false;
        firstHit = false;
        currentPoints = 0;

        tmp = Text.GetComponent<TextMeshPro>();
        NextPosition = ballSpawner.transform.position;
        bs = ballSpawner.GetComponent<BallSpawner>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ball")
        {
            
            
            if (firstHit == false)
            {
                setText = true;
                NextPosition = new Vector3(collision.transform.position.x, gameObject.transform.position.y + 0.2f, 0);

                Text.transform.parent.position = NextPosition;
                if (TriggerScript.onRight == true)
                {
                    Text.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.116f, 0.295f);
                }
                else
                {
                    Text.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.116f, -0.295f);
                }
                FirstBall.transform.gameObject.SetActive(true);
                Destroy(collision.gameObject);
                firstHit = true;
            }
            
            else
            {
                collision.transform.GetComponent<CircleCollider2D>().enabled = false;
                collision.transform.GetComponent<Rigidbody2D>().bodyType=RigidbodyType2D.Static;
                collision.transform.GetComponent<BallMoveto>().Move = true;
            }
            //count += 1;
            
            /*if (count == 1)
            {
                if (firstHit == false)
                {
                    

                    NextPosition = new Vector3(collision.transform.position.x, gameObject.transform.position.y + 0.2f, 0);
                    
                    Text.transform.parent.position = NextPosition;
                    if (TriggerScript.onRight == true)
                    {
                        Text.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.116f, 0.295f);
                    }
                    else
                    {
                        Text.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.116f, -0.295f);
                    }
                    ballSpawner.GetComponent<BallSpawner>().LastBall = collision.transform.gameObject;
                    ballSpawner.GetComponent<BallSpawner>().LastBall.layer = 2;
                    collision.transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    collision.transform.position = NextPosition;
                    firstHit = true;
                }
            } else if (count == BallSpawner.BallCount)
            {
                ballSpawner.GetComponent<BallSpawner>().SpeedupOff();

                Destroy(collision.gameObject);
                //Shooting = false;
                count = 0;
                currentPoints = 0;
                ballSpawner.transform.position = NextPosition;
                ballSpawner.GetComponent<BallSpawner>().moving = true;
                
                
                
            }
            else {
                collision.transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                collision.transform.GetComponent<BallMoveto>().Move = true;
                //Destroy(collision.gameObject);
            }*/
            
            
        }
    }
    private void Update()
    {
        if (firstHit == true)
        {
            if (ballSpawner.transform.childCount == 0)
            {
                if (GetDown.Move==false)
                {
                    bs.SpeedupOff();
                    currentPoints = 0;
                    ballSpawner.transform.position = NextPosition;
                    GetDown.Move = true;
                    setText = false;
                    count = 0;
                    firstHit = false;
                }
            }
        }
        if (setText == true)
        {
            if (count < BallSpawner.BallCount)
            {
                tmp.text = count + 1 + "x";
            }

        }
        else
        {
            if (Shooting == false)
            {
                tmp.text = BallSpawner.BallCount + "x";
                bs.Reset.SetActive(false);
                bs.slider.transform.parent.gameObject.SetActive(true);
            }
        }
    }

}
