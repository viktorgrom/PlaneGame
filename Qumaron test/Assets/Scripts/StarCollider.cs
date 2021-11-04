using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCollider : MonoBehaviour
{   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>())
            gameObject.SetActive(false);
        
    }
}
