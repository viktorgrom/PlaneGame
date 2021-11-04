using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlyUnit : MonoBehaviour
{
    [SerializeField] protected float _speed;
    protected float _maxPlaneSpeed = 4.5f;
    protected float _windForce = 0.32f;
    protected float _gasForce = 1.5f;

    
}
