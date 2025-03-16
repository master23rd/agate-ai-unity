using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private IEnumerator StartPowerUp()
    {
        if(OnPowerUpStart!=null)
        {
            OnPowerUpStart();
        }

        Debug.Log("Start power up");
        yield return new WaitForSeconds(_powerupDuration);

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
}
