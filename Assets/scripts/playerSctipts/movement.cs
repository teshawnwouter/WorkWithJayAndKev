using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class movement : MonoBehaviour
{

    private bool iFramesCheck;
    private float iFrameTime;
    private float iFrameEnd = 3f;


    public GameObject HP;
    public int hitPoints;
    public int maxHitpoints = 3;

    public int charge;


    public Vector3 giveMeMoney;
    public Vector3 overtimeCheck;

    //dit is het code key die je moet intoesten voor het schieten
    public KeyCode lazerKey = KeyCode.Mouse1;

    //dit is het draai snelheid
    [SerializeField]public float torque;

    //dit is voor je snellheid van beweging
    [SerializeField]public float thrust;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        hitPoints = maxHitpoints;

        charge = 0; 
       rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
       
        RecoverTIme();
        
        giveMeMoney = rb.angularVelocity;
        overtimeCheck = rb.velocity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float turn = Input.GetAxisRaw("Horizontal");
        rb.AddTorque(transform.up * torque * turn);

        if (Input.GetAxisRaw("Vertical") > 0)
        {
            Debug.Log(thrust);
            rb.AddRelativeForce(Vector3.forward * thrust);
            
        }


    }

    public int GetHealthPoints()
    {
        return hitPoints;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(hitPoints == 0)
        {
            FindAnyObjectByType<gameManager>().TotalScore();
            SceneManager.LoadScene("EndScherm");
            Debug.Log("game Over");
        }
        else if(iFramesCheck == false)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            hitPoints--;
            iFramesCheck = true;
        }
    }

    private void RecoverTIme()
    {
        if(iFramesCheck == true){
            iFrameTime = iFrameTime + Time.deltaTime;
            if(iFrameTime > iFrameEnd)
            {
                gameObject.GetComponent<BoxCollider>().enabled = true; 
                iFrameTime = 0;
                iFramesCheck = false;
            }
        } 
    }
    private void OnTriggerEnter(Collider other)
    {
     PickUps pickUp = other.GetComponent<PickUps>();
        if(pickUp != null)
        {
            pickUp.Actiate();
        }
    }

}
