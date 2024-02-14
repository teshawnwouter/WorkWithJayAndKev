using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class scoreBoard : MonoBehaviour
{
    public TextMeshProUGUI showScore;
    public float finalScore;
    public float scoreTime = 5f;
    public float timer = 0;

    private void Awake()
    {
       
        finalScore = PlayerPrefs.GetInt("score", 0);
    }

    private void Update()
    {
        timer += Time.deltaTime / scoreTime;
        showScore.text = Mathf.Lerp(0, finalScore, timer).ToString("0000");
        
    }


}
