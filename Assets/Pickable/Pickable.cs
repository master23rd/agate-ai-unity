using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField]
    public PickableType _pickableType;
    public Action<Pickable> OnPicked;


    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            // Debug.Log("trigger");
            // Debug.Log("Pickup " + _pickableType);
            // Destroy(gameObject);   

            if(OnPicked != null)
            {
                OnPicked(this);
            } 
        }

    }
    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
