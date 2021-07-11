using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAlertState : IState
{
    private Enemy owner;
    public string stateName => "alert";

    public EnemyAlertState(Enemy owner) {
        this.owner = owner;
    }

    public void Enter()
    {
        Debug.Log("alert!");
        owner.EquipWeapon();
        return;
    }

    public void Execute() 
    {
        Debug.Log("executing alert");
        owner.alertUpdateTimer = owner.alertUpdateTime;
        NavMeshHit navHit;
        NavMesh.SamplePosition(owner.player.transform.position, out navHit, 100, -1);
        owner.agent.SetDestination(navHit.position);
        Debug.Log(owner.agent.destination);
        Debug.Log(owner.agent.speed);
        return;
    }

    public void Exit() {
        owner.UnequipWeapon();
        return;
    }
}