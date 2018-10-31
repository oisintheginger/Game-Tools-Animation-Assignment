using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour {

    Rigidbody playerRB;
    Animator anim;


    public int playerHealth;

    [SerializeField]
    Vector3 JumpForce, forwardForce;
    [SerializeField]
    float strength, maxVert,attackRadius;

    // raycast transform
    [SerializeField]
    Transform raycastHeight;

    //checking for the ground check
    public groundCheck gc;

    //drawing a circle for the range
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

        //raycast stuff
        int layerMask = 1 << 9;

        
        RaycastHit hitter;
        if (Physics.Raycast(raycastHeight.position, transform.TransformDirection(Vector3.forward), out hitter, attackRadius, layerMask))
        {
            Debug.DrawRay(raycastHeight.position, transform.TransformDirection(Vector3.forward) * hitter.distance, Color.yellow);
            if(Input.GetMouseButtonDown(0)&&hitter.collider.gameObject.tag=="Enemy")
            {
                Debug.Log("Did Hit");
                hitter.collider.GetComponent<demon>().DemonHealth -=1;
                if (hitter.collider.GetComponent<demon>().DemonHealth == 0)
                {
                    hitter.collider.gameObject.GetComponent<Animator>().SetBool("DemonDead", true);
                }
            }
            
        }
        else
        {
            Debug.DrawRay(raycastHeight.position, transform.TransformDirection(Vector3.forward) * attackRadius, Color.white);
            Debug.Log("Did not Hit");
        }



        //running
        if (Input.GetKey(KeyCode.W)&&gc.Grounded==false&& anim.GetBool("PlayerDead") == false)
        {
            forwardForce = strength * transform.forward;
            playerRB.AddForce(forwardForce);
        }
        if (Input.GetKey(KeyCode.S) && gc.Grounded == false&& anim.GetBool("PlayerDead") == false)
        {
            forwardForce *= -1;
            playerRB.AddForce(forwardForce);
        }
        //jumping
        if (Input.GetKeyDown(KeyCode.Space) && gc.Grounded == true&& anim.GetBool("PlayerDead") == false)
        {
            StartCoroutine(jumpWait());
        }
        //attack and block
        if (Input.GetMouseButtonDown(0)&& anim.GetBool("PlayerDead") == false)
        {
            anim.SetTrigger("Attack");
        }
        if (Input.GetMouseButton(1)&& anim.GetBool("PlayerDead") == false)
        {
            anim.SetBool("isBlocking", true);
        }
        else if (!Input.GetMouseButton(1)&& anim.GetBool("PlayerDead") == false)
        {
            anim.SetBool("isBlocking", false);
        }
    }

    IEnumerator jumpWait()
    {
        anim.SetTrigger("JumpUp");
        yield return new WaitForSecondsRealtime(0.1f); //waiting for the animation to be at the correct point before the jumpforce is added
        playerRB.AddForce(JumpForce);
    }
}
