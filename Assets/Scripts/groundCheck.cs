using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundCheck : MonoBehaviour {

    public bool Grounded;
   
    private void OnTriggerStay(Collider collision)
    {

        Grounded = true;
        
    }

    private void OnTriggerExit(Collider collision)
    {
        Grounded = false;
        
    }
}
