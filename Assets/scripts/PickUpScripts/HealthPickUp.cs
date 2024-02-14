using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : PickUps
{
    public int HealthGane = 1;
    public override void Actiate()
    {
        base.Actiate();
        

        if (Movement.hitPoints < Movement.maxHitpoints)
        {
            Movement.hitPoints += HealthGane;
        }
        else
        {
            Debug.Log("test");
        }
    }

  

    
}
