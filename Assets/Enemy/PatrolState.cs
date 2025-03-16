using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    //define moving and destination
    private bool _isMoving;
    private Vector3 _destination;

    public void EnterState(Enemy enemy)
    {
        //default player? moving is false or idle
        _isMoving = false;
        // Debug.Log("start patrol");
    }

    public void UpdateState(Enemy enemy)
    {
        // Debug.Log("Patrolling");

        //if enemy distance and player is closer switch to chaseState
        if(Vector3.Distance(enemy.transform.position, enemy.Player.transform.position) < enemy.ChaseDistance)
        {
            enemy.SwitchState(enemy.ChaseState);
        }

        //if enemy is idle
        if(!_isMoving)
        {
            //randomize waypoint positions when 
            _isMoving = true;
            int index = UnityEngine.Random.Range(0, enemy.Waypoints.Count);

            //set destination location
            _destination = enemy.Waypoints[index].position;
            enemy.NavMeshAgent.destination = _destination;
        } else {
            //if distance close stop moving
            if(Vector3.Distance(_destination, enemy.transform.position) <=0.1)
            {
                _isMoving = false;
            }
        }
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log("Stop Patrol");
    }
}

