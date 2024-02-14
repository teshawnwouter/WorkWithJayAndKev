using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class gameManager : MonoBehaviour
{

    public int Large;
    public int medeum;
    public int Small;

    public TextMeshProUGUI score;
    public int newScore = 0;
    public List<GameObject> asstroids = new List<GameObject>();

    public int sceneAstroid;

    private int spawn;
    void Start()
    {
        Large = 50;
        medeum = 30;
        Small = 10;


        score.text = "score:" + newScore.ToString();

        spawn = UnityEngine.Random.Range(5, 7);
        sceneAstroid = 0;
        
        for (int i = 0; i < spawn ; i++)
        {
          StartNewRound();
        }
        
    }
    void Update()
    {


        if (sceneAstroid == 0)
        {
            for (int i = 0; i < spawn; i++)
            {
                StartNewRound();
            }
        }

    }

    void StartNewRound()
    {
        int randomIndex = UnityEngine.Random.Range(0, asstroids.Count);
        Vector3 RandomSpawnPosition = new Vector3(UnityEngine.Random.Range(-15, 15), 0, UnityEngine.Random.Range(-15, 15));

        Instantiate(asstroids[randomIndex], RandomSpawnPosition, Quaternion.identity);
        sceneAstroid++;
    }

    //word geroepen waneer astroiede wordt gedestoryed
    public void LostAstroids()
    {
        newScore+= Small;
        score.text = "score:" + newScore.ToString();
        sceneAstroid--;
    }
    public void SplitAstroids(int current)
    {
        if(current == 1)
        {
            newScore += medeum;
        }
        else
        {
            newScore += Large;
        }

       
        score.text ="score: " + newScore.ToString();
        sceneAstroid++;
    }

    public void LosePoints(int current)
    {
        if(current == 1)
        {
            newScore -= medeum;
        }
        else
        {
            newScore -= Large;
        }
    }
    public void LostToSmall()
    {
        newScore-= Small;
        score.text = "score:" + newScore.ToString();
        sceneAstroid--;
    }

    public void TotalScore()
    {
        PlayerPrefs.SetInt("score", newScore);
        PlayerPrefs.Save();
    }

}
