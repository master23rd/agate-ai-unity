using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickableManager : MonoBehaviour
{
    private List<Pickable> _pickableList = new List<Pickable>();

    [SerializeField]
    private Player _player;

    [SerializeField]
    //call ScoreManager class (initiate scoreManager)
    private ScoreManager _scoreManager;

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

        //set maxScore based on pickable List
        Debug.Log("Pickable List "+ _pickableList.Count);
        _scoreManager.SetMaxScore(_pickableList.Count);
    }

    private void OnPickablePicked(Pickable pickable)
    {
        _pickableList.Remove(pickable);
        if(_scoreManager !=null)
        {
            _scoreManager.AddScore(1);
        }
        Destroy(pickable.gameObject);
        Debug.Log("Pickable List "+ _pickableList.Count);

        if(_pickableList.Count <= 0)
        {
            Debug.Log("Win");
            SceneManager.LoadScene("WinScreen");
        }

        //notes* PickableType is private, but tutorial sugest to include it - so I change it to public 
        if(pickable._pickableType == PickableType.PowerUp)
        {
            _player?.PickPowerUp();
        }

    }
}
