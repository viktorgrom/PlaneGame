using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFly : MonoBehaviour
{
    public float speed;
    public float upPlaneForce;
    private Rigidbody _rb;

    public Transform cenOfMassTrasform;

    [SerializeField] private Transform[] waypoints;
    private int _starPosition;

    [SerializeField] private Image _planePicture;

    private void Awake()
    {
        GetComponent<Rigidbody>().centerOfMass = Vector3.Scale(cenOfMassTrasform.localPosition, transform.localScale);
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _starPosition = 0;
    }

    
    void Update()
    {
        Quaternion lookRotation = Quaternion.LookRotation((waypoints[_starPosition].position - transform.position).normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 2f * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        _rb.AddForce(transform.forward * speed  * Time.deltaTime);

        if(_rb.position.y < (waypoints[_starPosition].position.y))
        {
            _rb.AddForce(transform.up * upPlaneForce * Time.smoothDeltaTime);
        }

        //_rb.AddForce(transform.right * (upPlaneForce/5) * Time.deltaTime);

    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(GetComponent<Rigidbody>().worldCenterOfMass, 0.1f);
    }

    void IncreaseIndex()
    {
        _starPosition++;
        if (_starPosition >= waypoints.Length)
        {
            _starPosition = 0;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RotateStar>())
        {
            //_planePicture.rectTransform.localPosition = new Vector3(_planePicture.rectTransform.localPosition.x + 40, 0, 0);

            IncreaseIndex();
        }
    }
}
