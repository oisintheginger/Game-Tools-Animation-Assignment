using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour {

    Rigidbody playerRB;
    Animator anim;

    [SerializeField]
    Vector3 JumpForce, forwardForce;
    [SerializeField]
    float strength, maxVert,attackRadius;

    public groundCheck gc;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
        
    }

    // Use this for initialization
    void Start () {
        playerRB = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        forwardForce = strength * transform.forward;
        anim.SetFloat("VertSpeed", playerRB.velocity.y);
        anim.SetBool("isGrounded", gc.Grounded);
        float m_turn = Input.GetAxis("Horizontal");
        anim.SetFloat("Turn", m_turn);

        float m_forward = Input.GetAxis("Vertical");
        anim.SetFloat("Forward", m_forward);
        
        //running
        if (Input.GetKey(KeyCode.W)&&gc.Grounded==false)
        {
            forwardForce = strength * transform.forward;
            playerRB.AddForce(forwardForce);
        }
        if (Input.GetKey(KeyCode.S) && gc.Grounded == false)
        {
            forwardForce *= -1;
            playerRB.AddForce(forwardForce);
        }
        //jumping
        if (Input.GetKeyDown(KeyCode.Space) && gc.Grounded == true)
        {
            StartCoroutine(jumpWait());
        }
        //attack and block
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");
        }
        if (Input.GetMouseButton(1))
        {
            anim.SetBool("isBlocking", true);
        }
        else if (!Input.GetMouseButton(1))
        {
            anim.SetBool("isBlocking", false);
        }
    }

    IEnumerator jumpWait()
    {
        anim.SetTrigger("JumpUp");
        yield return new WaitForSecondsRealtime(0.1f);
        playerRB.AddForce(JumpForce);
    }

   
}
