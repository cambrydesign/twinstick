using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWanderState : IState
{
    private Enemy owner;
    public string stateName => "wander";

    private GameObject enemy;
    private Transform target;
    public Vector3 newPosition;

    public EnemyWanderState(Enemy owner) {
        this.owner = owner;
    }

    public void Enter()
    {
        enemy = owner.gameObject;
        newPosition = RandomNavSphere(enemy.transform.position, owner.wanderRadius, -1);
    }

    public void Execute() {
        owner.agent.SetDestination(newPosition);
    }

    public Vector3 RandomNavSphere(Vector3 origin, float distance, int layerMask) {
        Vector3 randDirection = Random.insideUnitSphere * distance;
        randDirection += origin;
        NavMeshHit navhit;
        NavMesh.SamplePosition(randDirection, out navhit, distance, layerMask);
        if (Vector3.Distance(navhit.position, owner.spawnPoint) > owner.maxWanderRadius) {
            return RandomNavSphere(origin, distance, layerMask);
        }
        return navhit.position;
    }

    public void Exit() {
        return;
    }
}
