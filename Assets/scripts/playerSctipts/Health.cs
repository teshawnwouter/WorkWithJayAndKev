using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] GameObject [] health;
    [SerializeField] private movement Movement;

    // Update is called once per frame
    void Update()
    {
        int lives = Movement.GetHealthPoints();

        for (int i = 0; i < health.Length; i++)
        {
            if(i < lives){
                health[i].SetActive(true);
            }
            else
            {
                health[i].SetActive(false);
            }
        }
    }
}
