using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Starter, Spread, Burst, Bomb, Rail}
public class Artillery : MonoBehaviour
{
    [SerializeField]
    private State _state = State.Starter;
    bool readyToFire = true;
    public float proSpeed = 10f;
    public float fireDelay = 3f;
    public float burstDelay = .3f;
    public int burstSpreadAngle;
    public float spreadAngle = 5f;
    public int explosionForce;
    public int explosionRadius;

    [Header("Prefab")]
    public GameObject starterObj, spreadObj, burstObj, bombObj;
    public GameObject lazer;
    public LineRenderer lineRenderer;

    [Header("References")]
    public Transform attackPoint;

    // Start is called before the first frame update
    void Start()
    {
        starterObj.GetComponent<bullets>().speed = proSpeed;
        spreadObj.GetComponent<bullets>().speed = proSpeed;
        burstObj.GetComponent<bullets>().speed = proSpeed;
        //bombObj.GetComponent<bullets>().speed = proSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && readyToFire == true)
        {
            UWS();
        }
    }
    void UWS()
    {
        switch (_state)
        {
            case State.Burst:
                StartCoroutine(Burst());
                break;

            case State.Spread:
                Projectile(spreadObj, -spreadAngle, false);
                Projectile(spreadObj, 0f, false);
                Projectile(spreadObj, spreadAngle, true);
                break;

            case State.Bomb:
                break;

            case State.Starter:
                Projectile(starterObj, 0f,true);
                break;

            case State.Rail:
                Lazer();
                break;

            default:
                break;

        }
    }
    private void Projectile(GameObject obj, float rotate,bool reset)
    {
        readyToFire = false;
        attackPoint.Rotate(0f,rotate,0f);
        GameObject projectile = Instantiate(obj, attackPoint.position, attackPoint.rotation);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        Vector3 forceDirection = attackPoint.transform.forward;
        attackPoint.Rotate(0f, -(rotate), 0f);
        if(reset)
            Invoke(nameof(ResetShot), fireDelay);
    }
    private IEnumerator Burst()
    {
        Projectile(burstObj, -(burstSpreadAngle), false);
        yield return new WaitForSeconds(burstDelay);
        Projectile(burstObj, 0f, false);
        yield return new WaitForSeconds(burstDelay);
        Projectile(burstObj, burstSpreadAngle, true);

    }
    private void ResetShot()
    {
        readyToFire = true;
    }
    private void Lazer()
    {
        readyToFire = false;
        Debug.Log("lazer");
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 100.0f);
        //laser from point a to b
        lineRenderer.SetPosition(0, attackPoint.position);
        lineRenderer.SetPosition(1, attackPoint.position + (transform.forward * 100f));
        StartCoroutine(ShootLaser());
        Debug.DrawRay(transform.position, transform.forward * 100);
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
                if (bussin != null)
                {
                    bussin.Dynamite();
                }
            }
        }
        Invoke(nameof(ResetShot), fireDelay);
    }
    IEnumerator ShootLaser()
    {
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(.3f);
        lineRenderer.enabled = false;
    }
}
