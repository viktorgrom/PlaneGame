using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangAtPosition : MonoBehaviour
{
    public Rigidbody TargetRigitbody;
    public float ForceValue = 10f;

    
   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            TargetRigitbody.AddForceAtPosition(-transform.up * ForceValue, transform.position);
        }
        else
        {
            return;
        }
    }
}
