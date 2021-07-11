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
        return;
    }

    public void Exit() {
        return;
    }
}