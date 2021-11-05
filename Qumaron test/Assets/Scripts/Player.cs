using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : FlyUnit
{
    [SerializeField] private Transform[] waypoints;        
    private Rigidbody _rb;

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

    void Start()
    {
        
        _speed = 0f;
        _rb = GetComponent<Rigidbody>();
        _starPosition = 0;
        _rb.useGravity = true;
        _gasEffectPlane.SetActive(false);       
        _gasAlert.SetActive(false);
        _gameOverBtn.SetActive(false);
        _buttonEffect.SetActive(false);
        _gasBtnBar.fillAmount = _gasImageFill;

        
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
        if(_move && _speed <= _maxPlaneSpeed && !_gameOver)
        {
            _speed += _gasForce * Time.deltaTime; 
            _gasImageFill -= 0.08f * Time.deltaTime;
            _gasBtnBar.fillAmount = _gasImageFill;
            _gasEffectPlane.SetActive(true);
            _buttonEffect.SetActive(true);



        }
        else if(!_move && _speed > 0)
        {
            _speed -= _windForce * Time.deltaTime;
            _gasEffectPlane.SetActive(false);
            _buttonEffect.SetActive(false);
        }

        FlyToStar();
        AlertSpeed();
        CheckGravitation();

        if (_speed <= 0 && _gravityEnable)
        {
            GameOver();
        }
            

    } 

    private void GameOver()
    {
        _gameOver = true;
        _gameOverBtn.SetActive(true);
    }
    //гравітація
    private void CheckGravitation()
    {
        if (_speed > _maxPlaneSpeed / 2 && _startFLy)
        {
            _startFLy = false;
            _rb.useGravity = false;
        }

        if (_speed < _maxPlaneSpeed / 2 && _gravityEnable && _rb.useGravity == false)
        {
            _rb.useGravity = true;
        }

        if(_speed > _maxPlaneSpeed / 2 && !_startFLy)
            _rb.useGravity = false;


        if (_speed >= _maxPlaneSpeed/2)
            _gravityEnable = true;
    }
    //загроза падінню
    private void AlertSpeed()
    {
        if (_speed < _maxPlaneSpeed / 2 && !_startFLy)
            _gasAlert.SetActive(true);
        else
            _gasAlert.SetActive(false);
    }
    

    //рух
    private void FlyToStar()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);

        Quaternion lookRotation = Quaternion.LookRotation((waypoints[_starPosition].position - transform.position).normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 2f * Time.deltaTime);
    }

    void IncreaseIndex()
    {
        _starPosition++;
        if (_starPosition >= waypoints.Length)
        {
            _starPosition = 0;
        }       
        
    }
}
