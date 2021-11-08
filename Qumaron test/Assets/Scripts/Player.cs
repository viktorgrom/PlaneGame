using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : FlyUnit
{
    [SerializeField] private Transform[] waypoints;        
    private Rigidbody _rb;
   
    public Transform cenOfMassTrasform;
   // public Vector3 cenOfMassTrasformV;
    //public Vector3 cenOfMassTrasformBack;

    private int _starPosition;    
    [SerializeField] private Image _planePicture;  
    [SerializeField] private GameObject _gasEffectPlane;
    
    [SerializeField] private Image _gasBtnBar;
    private float _gasImageFill = 1f;
    private bool _gameOver = false;
    
    [SerializeField] private GameObject _gasAlert;
    [SerializeField] private GameObject _gameOverBtn;
    [SerializeField] private GameObject _buttonEffect;

    private bool _move = false;
    private bool _gravityEnable = false;
    private bool _startFLy = true;
   
    private Animator _anim;


    public Vector3 LookTargetPosition;
    public float distPlaneToTarget;

    public float timePassStarPlace = 0.0f;

    
    private void Awake()
    {
        GetComponent<Rigidbody>().centerOfMass = Vector3.Scale(cenOfMassTrasform.localPosition, transform.localScale);
    }

    void Start()
    {
        _anim = GetComponent<Animator>();        
        _anim.gameObject.GetComponent<Animator>().enabled = false;
        _speed = 0f;
        _rb = GetComponent<Rigidbody>();
        _starPosition = 0;
        _rb.useGravity = true;
        _gasEffectPlane.SetActive(false);       
        _gasAlert.SetActive(false);
        _gameOverBtn.SetActive(false);
        _buttonEffect.SetActive(false);
        _gasBtnBar.fillAmount = _gasImageFill;

        //LookTargetPosition = waypoints[_starPosition].position;

    }

    //зібрав зірку
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RotateStar>())
        {
            _planePicture.rectTransform.localPosition = new Vector3(_planePicture.rectTransform.localPosition.x + 40, 0, 0);           
           
            IncreaseIndex();
        }
    }

    public void Accelerate(bool _move)
    {
        this._move = _move;
    }

      
    void Update()
    {
        if (_move && _speedUp <= _maxUpdPlaneSpeed && !_gameOver)
            _speedUp += _accelerateUpForce * Time.deltaTime;

        if (_move && _speed <= _maxForvardPlaneSpeed && !_gameOver)
        {
            _speed += _accelerateForfardForce * Time.deltaTime;
            
            _gasImageFill -= 0.08f * Time.deltaTime;
            _gasBtnBar.fillAmount = _gasImageFill;
            _gasEffectPlane.SetActive(true);
            _buttonEffect.SetActive(true);
           
           // GetComponent<Rigidbody>().centerOfMass = cenOfMassTrasform;

        }
        else if(!_move && _speed > 0)
        {
            _speed -= _windForce * Time.deltaTime;
            _speedUp -= _gravityForce * Time.deltaTime;
            _gasEffectPlane.SetActive(false);
            _buttonEffect.SetActive(false);


           // GetComponent<Rigidbody>().centerOfMass = cenOfMassTrasformBack;
        }

       
        FlyToStar();
        AlertSpeed();
        //CheckGravitation();

        if (_speed <= 0 && _gravityEnable)
        {
            GameOver();
        }

        //зміна віртуальної позиції зірки
       /* distPlaneToTarget = Vector3.Distance(waypoints[_starPosition].position, transform.position);

        if(distPlaneToTarget > 8f)
        {
            timePassStarPlace += Time.deltaTime;

            if (timePassStarPlace >= 2f)
            {                               

                float x;
                float y;
                float z;
                x = Random.Range(-0.1f, 0.1f);
                y = Random.Range(-0.1f, 0.1f);
                z = Random.Range(-0.1f, 0.1f);

                LookTargetPosition = new Vector3(waypoints[_starPosition].position.x + x,
                    waypoints[_starPosition].position.y + y, waypoints[_starPosition].position.z + z);
               
                timePassStarPlace = 0f;
            }                            
        }*/
        

    }

    private void GameOver()
    {
        _gameOver = true;
        _gameOverBtn.SetActive(true);
    }
    //гравітація
    /*private void CheckGravitation()
    {
        if (_speed > _maxPlaneSpeed / 2 && _startFLy)
        {
            _startFLy = false;
            _rb.useGravity = false;
            anim.gameObject.GetComponent<Animator>().enabled = true;
        }

        if (_speed < _maxPlaneSpeed / 2 && _gravityEnable && _rb.useGravity == false)
        {
            _rb.useGravity = true;
        }

        if(_speed > _maxPlaneSpeed / 2 && !_startFLy)
            _rb.useGravity = false;


        if (_speed >= _maxPlaneSpeed/2)
            _gravityEnable = true;
    }*/
    //загроза падінню
    private void AlertSpeed()
    {
        if (_speed < _maxForvardPlaneSpeed / 2 && !_startFLy)
            _gasAlert.SetActive(true);
        else
            _gasAlert.SetActive(false);
    }
   

    //рух
    private void FlyToStar()
    {
        
        Quaternion lookRotation = Quaternion.LookRotation((waypoints[_starPosition].position - transform.position).normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 2f * Time.deltaTime);      
    }

    private void FixedUpdate()
    {
        _rb.AddForce(transform.forward * _speed * Time.deltaTime);

        if (_rb.position.y < (waypoints[_starPosition].position.y) && _speed > 0)
        {
            _rb.AddForce(transform.up * _speedUp * Time.deltaTime);
        }
       
        // _rb.AddForce(transform.right * (_speed / 8) * Time.deltaTime);              

    }

    void IncreaseIndex()
    {
        _starPosition++;
        if (_starPosition >= waypoints.Length)
        {
            _starPosition = 0;
        }       
        
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(GetComponent<Rigidbody>().worldCenterOfMass, 0.1f);
    }
}
