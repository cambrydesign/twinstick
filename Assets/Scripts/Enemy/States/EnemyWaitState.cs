using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWaitState : IState
{
    private Enemy owner;
    public string stateName => "wait";

    public EnemyWaitState(Enemy owner) {
        this.owner = owner;
    }

    public void Enter()
    {
        owner.UnequipWeapon();
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
