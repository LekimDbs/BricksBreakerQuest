using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningTrigger : MonoBehaviour
{
    public GameObject WarningBg;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Target")
        {
            if(WarningBg.activeSelf==false)
            WarningBg.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Target")
        {
            if(WarningBg.activeSelf==true)
            WarningBg.SetActive(false);
        }
    }


    
}
