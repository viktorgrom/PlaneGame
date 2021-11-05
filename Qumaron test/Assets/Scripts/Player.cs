using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : FlyUnit
{
    [SerializeField] private Transform[] waypoints;    
    private float _alertSpeed = 3.5f;    
    private Rigidbody _rb;

    private int _starPosition;    
    [SerializeField] private Image _planePicture;
    private bool _onGas = false;    
    [SerializeField] private GameObject _gasEffectPlane;
    
    [SerializeField] private Image _gasBtnBar;
    private float _gasImageFill = 1f;
    private bool _gameOver = false;
    
    [SerializeField] private GameObject _gasAlert;
    [SerializeField] private GameObject _gameOverBtn;

    void Start()
    {
        _speed = 5f;
        _rb = GetComponent<Rigidbody>();
        _starPosition = 0;

        _gasEffectPlane.SetActive(false);       
        _gasAlert.SetActive(false);
        _gameOverBtn.SetActive(false);
        _gasBtnBar.fillAmount = _gasImageFill;

        transform.LookAt(waypoints[_starPosition]);
      
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

    
    void Update()
    {
        FlyToStar();

        if (_speed > 0f && !_onGas)
        {
            _speed -= _windForce * Time.deltaTime;
        }

        if(_speed < _alertSpeed)
            _gasAlert.SetActive(true);
        else
        {
            StartCoroutine(LookAt());
            _gasAlert.SetActive(false);
        }
            
        if (_speed < _maxPlaneSpeed / 2)
        {
            _rb.useGravity = true;            
        }     
    } 

    //газ
    public void GasSystem()
    {
        if(!_onGas && !_gameOver)
            StartCoroutine(GasOnCarutine());

        if(_gasImageFill > 0)
        {
            _gasImageFill -= 0.1f;
            _gasBtnBar.fillAmount = _gasImageFill;
        }
        else
        {
            _gameOver = true;
            _gameOverBtn.SetActive(true);
        }
        
    } 

    private IEnumerator GasOnCarutine()
    {
        float countDown = 1f;
        _onGas = true;
        _gasEffectPlane.SetActive(true);        
        _rb.useGravity = false;

        for (int i = 0; i < 1000; i++)
        {
            while(countDown >= 0)
            {
                if (_speed < _maxPlaneSpeed)
                {
                    _speed += _gasForce * Time.deltaTime;
                }

                countDown -= Time.deltaTime;
                yield return null;               
            }
            _onGas = false;
            _gasEffectPlane.SetActive(false);                 
        }           
    }
    
    //зміна напрямку
    private IEnumerator LookAt()
    {
        Quaternion lookRotation = Quaternion.LookRotation(waypoints[_starPosition].position - transform.position);

        float time = 0;

        while (time < 0.5)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
            time += Time.deltaTime * 1f;

            yield return null;
        }
    }
    //рух
    private void FlyToStar()
    {      
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);        
    }

    void IncreaseIndex()
    {
        _starPosition++;
        if (_starPosition >= waypoints.Length)
        {
            _starPosition = 0;
        }       
        StartCoroutine(LookAt());
    }
}
