using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinCollector : PickUps
{
    public int coin = 100;

    public override void Actiate()
    {
        base.Actiate();

        if(coin <= 0)
        {
           GameManager.newScore += coin;
        }
       
    }
}
