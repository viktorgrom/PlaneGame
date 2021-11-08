using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlyUnit : MonoBehaviour
{
    [SerializeField] protected float _speed;
    [SerializeField] protected float _speedUp;
    protected float _maxForvardPlaneSpeed = 2000f;
    protected float _maxUpdPlaneSpeed = 1000f;   
    protected float _windForce = 500f;
    protected float _gravityForce = 200f;
    protected float _accelerateForfardForce = 750f;
    protected float _accelerateUpForce = 200f;

    
}
