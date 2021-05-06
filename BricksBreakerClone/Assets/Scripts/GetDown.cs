using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDown : MonoBehaviour
{
    public static bool Move;
    private float speed = 8f;
    private float step;
    public Vector2 newPos;


    private void Start()
    {
        Move = false;
    }
    private void FixedUpdate()
    {
        if (Move == true)
        {
            step = speed * Time.deltaTime;

            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, newPos, step);
            if (Vector2.Distance(gameObject.transform.position, newPos) < 0.0001f)
            {

                Move = false;
                Wall.Shooting = false;

            }
        }
    }
}
