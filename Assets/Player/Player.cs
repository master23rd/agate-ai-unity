using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    private Rigidbody _rigidBody;
    
    [SerializeField]
    private float _speed;

    [SerializeField]
    private Transform _camera;

    [SerializeField]
    private float _powerupDuration;

    private Coroutine _powerupCoroutine;

    public Action OnPowerUpStart;
    public Action OnPowerUpStop;

    private bool _isPowerUpActive;

    [SerializeField]
    private Transform _respawnPoint;

    [SerializeField]
    private int _health;

    [SerializeField]
    private TMP_Text _healthText;

    private IEnumerator StartPowerUp()
    {
        _isPowerUpActive = true;

        if(OnPowerUpStart!=null)
        {
            OnPowerUpStart();
        }
        Debug.Log("Start power up");
        
        yield return new WaitForSeconds(_powerupDuration);
         _isPowerUpActive = false;
       
        if(OnPowerUpStop!=null)
        {
            OnPowerUpStop();
        }
        Debug.Log("Stop power up");
    }

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        UpdateUI();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Debug.Log("Horizontal:" +horizontal);
        Debug.Log("Veritcal:" + vertical);

        Vector3 horizontalDirection = horizontal * _camera.right;
        Vector3 verticalDirection = vertical * _camera.forward;

        verticalDirection.y = 0;
        horizontalDirection.y = 0;

        //Vector3 movementDirection = new Vector3(horizontal,0,vertical);
        Vector3 movementDirection = horizontalDirection + verticalDirection;
        _rigidBody.velocity = movementDirection * _speed * Time.fixedDeltaTime;
    }

    public void PickPowerUp()
    {
        Debug.Log("Pick Power Up");
        if(_powerupCoroutine != null)
        {
            StopCoroutine(_powerupCoroutine);
        }

        _powerupCoroutine = StartCoroutine(StartPowerUp());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(_isPowerUpActive)
        {
          if(collision.gameObject.CompareTag("Enemy"))
          {
               collision.gameObject.GetComponent<Enemy>().Dead();
          }
        }
    }

    private void UpdateUI()
    {
        _healthText.text = "Health:" + _health;
    }

    public void Dead()
    {
        _health -= 1;
        
        if(_health > 0)
        {
            transform.position = _respawnPoint.position;

        } else {
            _health = 0;
            Debug.Log("Lose");
            SceneManager.LoadScene("LoseScreen");
        }
        UpdateUI();
    }
}
