using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  PickUps : MonoBehaviour
{
    protected movement Movement;
    protected gameManager GameManager;
    public virtual void Actiate()
    {
           Destroy(gameObject);

    }
    private void Start()
    {
        Movement = FindObjectOfType<movement>();
    }

}
