using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunChargePickUp : PickUps
{
   
    public int LazerCharge = 5;
    public override void Actiate()
    {

        base.Actiate();
        Movement.charge += LazerCharge;
    }
}
