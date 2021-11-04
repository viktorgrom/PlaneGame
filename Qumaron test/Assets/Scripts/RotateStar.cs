using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateStar : MonoBehaviour
{   
    private void Update()
    {
        transform.Rotate(new Vector3(0f, 40f * Time.deltaTime, 0f));       
    }
}
