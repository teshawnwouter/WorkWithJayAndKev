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

    [Header("References")]
    public Transform attackPoint;

    // Start is called before the first frame update
    void Start()
    {
        starterObj.GetComponent<bullets>().speed = proSpeed;
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
                Projectile(starterObj, -spreadAngle, false);
                Projectile(starterObj, 0f, false);
                Projectile(starterObj, spreadAngle, true);
                break;

            case State.Bomb:
                break;

            case State.Starter:
                Projectile(starterObj, 0f,true);
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
        Projectile(starterObj, -(burstSpreadAngle), false);
        yield return new WaitForSeconds(burstDelay);
        Projectile(starterObj, 0f, false);
        yield return new WaitForSeconds(burstDelay);
        Projectile(starterObj, burstSpreadAngle, true);

    }
    private void ResetShot()
    {
        readyToFire = true;
    }
}
