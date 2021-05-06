using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static int Points;
    public Slider Slider;
    public float TargetScore;
    public Image Star1;
    public Image Star2;
    public Image Star3;
    private float changeSpeed;
    public TextMeshProUGUI PointsText;

    private void Start()
    {
        Points = 0;
    }


    void Update()
    {
        changeSpeed = Points - Slider.value;
        Slider.value = Mathf.MoveTowards(Slider.value, Points / TargetScore * 100, changeSpeed * Time.deltaTime);
        PointsText.text = Points + "";
        

        if (Mathf.Round(Slider.value) >= 100)
        {
            Star3.color = new Color(1, 1, 1);
        } else if (Mathf.Round(Slider.value) >= 70)
        {
            Star2.color = new Color(1, 1, 1);
        } else if (Mathf.Round(Slider.value) > 0)
        {
            Star1.color = new Color(1, 1, 1);
        }
    }
}
