using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlyUnit : MonoBehaviour
{
    [SerializeField] protected float _speed;
    protected float _maxPlaneSpeed = 2f;
    protected float _windForce = 0.1f;
    protected float _gasForce = 0.3f;

    
}
