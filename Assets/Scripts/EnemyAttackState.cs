using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : IState
{
    private Enemy owner;
    public string stateName => "attack";

    public EnemyAttackState(Enemy owner) 
    {
        this.owner = owner;
    }

    public void Enter()
    {
        owner.agent.isStopped = true;
        Debug.Log("Attacking");
        return;
    }

    public void Execute() 
    {
        owner.transform.LookAt(owner.player.transform);
        owner.firePoint.LookAt(owner.player.transform);
        Fire();
        return;
    }

    void Fire() 
    {
        GameObject bullet = BulletPoolingManager.Instance.GetBullet();
        bullet.transform.position = owner.firePoint.position;
        bullet.transform.forward = owner.firePoint.forward;
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.isKinematic = false;
        rb.AddForce(owner.firePoint.forward * owner.bulletForce, ForceMode.Impulse);
    }

    public void Exit() 
    {
        owner.agent.isStopped = false;
        return;
    }
}