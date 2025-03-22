using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
     //make list waypoint
     [SerializeField]
     public List<Transform> Waypoints = new List<Transform>();

     //navmestAgent variable
     [HideInInspector]
     public NavMeshAgent NavMeshAgent;

     [SerializeField]
     public float ChaseDistance;
     [SerializeField]
     public Player Player;

     //state initiate
     private BaseState _currentState;
     public PatrolState PatrolState = new PatrolState();
     public ChaseState ChaseState = new ChaseState();
     public RetreatState RetreatState = new RetreatState();

    //public object Animator { get; internal set; }
    [HideInInspector]
    public Animator Animator;
    
    private void Awake() 
     {
          //make patrol default state
           Animator = GetComponent<Animator>();
          _currentState = PatrolState;
          _currentState.EnterState(this); 

          //activate navMeshAgent
          NavMeshAgent = GetComponent<NavMeshAgent>();
    
     }

    private void Start()
    {
        if(Player!=null)
        {
          Player.OnPowerUpStart += StartRetreating;
          Player.OnPowerUpStop += StopRetreating;
        }
    }

    private void Update()
     {
          if(_currentState!=null)
          {
               _currentState.UpdateState(this);
          }
     }

     public void SwitchState(BaseState state)
     {
          //finish current state
          _currentState.ExitState(this);

          //replace current state with active state (latest)
          _currentState = state;
          _currentState.EnterState(this);
     }

     private void StartRetreating()
     {
          SwitchState(RetreatState);
     }

     private void StopRetreating()
     {
          SwitchState(PatrolState);
     }

     public void Dead()
     {
          Destroy(gameObject);
     }

    private void OnCollisionEnter(Collision collision)
    {
        if(_currentState != RetreatState)
        {
          if(collision.gameObject.CompareTag("Player"))
          {
               collision.gameObject.GetComponent<Player>().Dead();
          }
        }
    }
}
