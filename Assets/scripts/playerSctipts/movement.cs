using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class movement : MonoBehaviour
{


    private int iFrames;
    private bool iFramesCheck;
    private float iFrameTime;
    private float iFrameEnd = 3f;


    public GameObject HP;
    public int hitPoints;
    public int maxHitpoints = 3;

    public int charge;
    //dit is het punt van waar je objecten komen
    public Transform attackPoint;

    //dit is het object wat je schiet
    public GameObject objectToThrow;
    public GameObject lazer;
    public LineRenderer lineRenderer;

    //dit zij de variabele voor het schieten
    public float throwCooldown;
    public float throwForce;
    public Vector3 giveMeMoney;
    public Vector3 overtimeCheck;
    bool readyToThrow;

    //dit is het code key die je moet intoesten voor het schieten
    public KeyCode throwKey = KeyCode.Mouse0;
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
        readyToThrow = true;
       rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
       
        RecoverTIme();

        //Debug.Log(Input.GetAxisRaw("Vertical"));
        if (Input.GetKeyDown(lazerKey) && charge >= 5)
        {
            Lazer();
            charge = charge- 5;
        }
        

        if (Input.GetKeyDown(throwKey) && readyToThrow == true) 
        {
            Throw();
        }
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
    private void Throw()
    {
        readyToThrow = false;
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, attackPoint.rotation);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        Vector3 forceDirection = attackPoint.transform.forward;

        Invoke(nameof(ResetThrow), throwCooldown);
    }
    private void ResetThrow()
    {
        readyToThrow = true;
    }
    
    private void Lazer()
    {
        Debug.Log("lazer");
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 100.0f);
        //laser from point a to b
        lineRenderer.SetPosition(0, attackPoint.position);
        lineRenderer.SetPosition(1, attackPoint.position + (transform.forward * 100f));
        StartCoroutine(ShootLaser());
        Debug.DrawRay(transform.position, transform.forward *100);
        for (int i = 0; i < hits.Length; i++)
        {

            RaycastHit hit = hits[i];
            Renderer rend = hit.transform.GetComponent<Renderer>();
            if (rend)
            {
                rend.material.shader = Shader.Find("Transparent/Diffuse");
                Color tempColor = rend.material.color;
                tempColor.a = 0.3f;
                rend.material.color = tempColor;
                astroid bussin = rend.transform.GetComponent<astroid>();
                if(bussin != null)
                {
                    bussin.Dynamite();
                }
            }
        }
    }
    IEnumerator ShootLaser()
    {
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(.3f);
        lineRenderer.enabled = false;
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
