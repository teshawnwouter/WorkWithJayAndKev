using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class WallBang : MonoBehaviour
{
    [SerializeField]
    Vector3 xMin, xMax, yMin, yMax;


    public Camera cam ;

    public GameObject moneySpread;
    // Start is called before the first frame update
    void Start()
    {
        xMin = cam.ViewportToWorldPoint(new Vector3(0, 0, 20));
        xMax = cam.ViewportToWorldPoint(new Vector3(1, 0, 20));
        yMin = cam.ViewportToWorldPoint(new Vector3(0, 0, 20));
        yMax = cam.ViewportToWorldPoint(new Vector3(0, 1, 20));
    }

    // Update is called once per frame
    void Update()
    {
        if(moneySpread.transform.position.x < xMin.x)
        {
            moneySpread.transform.position = new Vector3(xMax.x, moneySpread.transform.position.y, moneySpread.transform.position.z);
        }
        if (moneySpread.transform.position.x > xMax.x)
        {
            moneySpread.transform.position = new Vector3(xMin.x, moneySpread.transform.position.y, moneySpread.transform.position.z);
        }
        if (moneySpread.transform.position.z < yMin.z)
        {
            moneySpread.transform.position = new Vector3(moneySpread.transform.position.x, moneySpread.transform.position.y, yMax.z);
        }
        if (moneySpread.transform.position.z > yMax.z)
        {
            moneySpread.transform.position = new Vector3(moneySpread.transform.position.x, moneySpread.transform.position.y, yMin.z);
        }
    }
}
