using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoBehaviour
{
    private List<Pickable> _pickableList = new List<Pickable>();

    [SerializeField]
    private Player _player;

    // Start is called before the first frame update`
    void Start()
    {
        InitPickableList();
    }

    private void InitPickableList()
    {
        Pickable[] pickableObjects = GameObject.FindObjectsOfType<Pickable>();
        for(int i = 0; i < pickableObjects.Length; i++)
        {
            _pickableList.Add(pickableObjects[i]);
            pickableObjects[i].OnPicked += OnPickablePicked;
        }

        Debug.Log("Pickable List "+ _pickableList.Count);
    }

    private void OnPickablePicked(Pickable pickable)
    {
        _pickableList.Remove(pickable);
        Destroy(pickable.gameObject);
        Debug.Log("Pickable List "+ _pickableList.Count);

        if(_pickableList.Count <= 0)
        {
            Debug.Log("Win");
        }

        //notes* PickableType is private, but tutorial sugest to include it - so I change it to public 
        if(pickable._pickableType == PickableType.PowerUp)
        {
            _player?.PickPowerUp();
        }
    }
}
