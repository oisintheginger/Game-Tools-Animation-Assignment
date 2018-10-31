using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellBall : MonoBehaviour {

    Rigidbody rb;
    Vector3 projectForce;
    [SerializeField]
    float projectileSpeed;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update () {
        projectForce = transform.forward * projectileSpeed ;
        rb.AddForce(projectForce);
        Destroy(gameObject, 3.0f);
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag =="Player")
        {
            Destroy(gameObject);
        }
    }
}
