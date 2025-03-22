using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    public void EnterState(Enemy enemy)
    {
        Debug.Log("start Chase");
        // enemy.Animator.SetTrigger("ChaseState");
        enemy.Animator.SetTrigger("ChaseState");
    }

    public void UpdateState(Enemy enemy)
    {
        Debug.Log("Chase");
        //if player not null
        if(enemy.Player !=null)
        {
            enemy.NavMeshAgent.destination = enemy.Player.transform.position;
        }
        //if current distance higher than chase value, then switch to patrol
        if(Vector3.Distance(enemy.transform.position, enemy.Player.transform.position)> enemy.ChaseDistance)
        {
            enemy.SwitchState(enemy.PatrolState);
        }
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log("Stop Chase");
    }
}
