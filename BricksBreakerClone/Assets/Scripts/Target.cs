using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Target : MonoBehaviour
{
    public Color[] Colors;
    public int Life;
    private TextMeshPro txt;

    private void Start()
    {
        txt = gameObject.transform.GetChild(0).GetComponent<TextMeshPro>();
        txt.text = Life + "";
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ball")
        {
            if (Life > 1)
            {
                Life--;
                txt.text = Life + "";


                gameObject.transform.GetComponent<SpriteRenderer>().color = Colors[Random.Range(0, Colors.Length)];
            }
            else
            {
                transform.parent.GetComponent<PlayPs>().start = true;
                Destroy(this.gameObject);
            }

        }
    }
    private void OnDestroy()
    {
        
        Wall.currentPoints += 10;
        UIManager.Points += Wall.currentPoints;
    }
    
}
