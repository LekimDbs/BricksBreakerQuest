using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayPs : MonoBehaviour
{
    public bool start;
    public TextMeshPro PStxt;
    private ParticleSystem ps;

    private void Start()
    {
        ps = gameObject.GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (start == true)
        {
            StartCoroutine(DestroyPs());
            start = false;
        }
    }
    IEnumerator DestroyPs()
    {
        PStxt.gameObject.SetActive(true);
        PStxt.text = Wall.currentPoints + "";
        ps.Play();
        yield return new WaitForSeconds(0.8f);
        Destroy(ps.gameObject);
        Destroy(PStxt.gameObject);
    }
}
