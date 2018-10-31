using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class demon : MonoBehaviour {

    public Transform player;
    
    [SerializeField]
    private float lookRadius, moveSpeed, attackSpellRadius, moveRadius;
    [SerializeField]
    Vector3 thisPos, playerPos;

    [SerializeField]
    private Transform projectileTransform;

    public GameObject projectileObj;

    Animator animD;
    Rigidbody demonRB;
    private void Start()
    {
        animD = GetComponent<Animator>();
        demonRB = GetComponent<Rigidbody>();
       // projectileTransform = animD.GetBoneTransform(HumanBodyBones.RightHand);
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, moveRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackSpellRadius);
    }
    // Update is called once per frame
    void Update () {
        thisPos = transform.position;
        
        playerPos = player.transform.position;
        
        


        float sec = moveSpeed * Time.deltaTime;

        float distXZ = Vector3.Distance(playerPos, thisPos);

        float distY = Mathf.Abs(player.position.y - transform.position.y);
        
        
        if (distXZ < moveRadius&& distXZ>attackSpellRadius && distY < 0.5f)
        {
            animD.SetBool("DemonRunning", true);
            transform.position = Vector3.MoveTowards(thisPos, player.transform.position,sec);
        }
        
        else if(distXZ>moveRadius|| distY > 0.2f)
        {
            if (distXZ < attackSpellRadius)
            {

                animD.SetTrigger("DemonAttack");
                animD.SetBool("DemonRunning", false);
            }
            else if(distXZ > attackSpellRadius)
            {
                animD.SetBool("isAttacking", false);
            }
            animD.SetBool("DemonRunning", false);
        }
        if (distXZ < lookRadius&& distY < 0.5f)
        {
            transform.LookAt(player);
        }

        
    }

    public void projectile()
    {
        
        Instantiate(projectileObj, projectileTransform.position, projectileTransform.rotation);
        
        
    }
}
