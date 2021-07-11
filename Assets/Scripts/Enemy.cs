using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateEngine engine = new StateEngine();
    private float wanderTimer = 0;
    private GameObject gun;

    public float waitTime = 10;
    public float wanderRadius = 5;
    public float maxWanderRadius = 20;
    public bool isAlert;
    public float calmdownTime = 50;
    private float seenTimer = 50;

    [HideInInspector]
    public Vector3 spawnPoint;
    [HideInInspector]
    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        isAlert = false;
        spawnPoint = gameObject.transform.position;
        agent = gameObject.GetComponent<NavMeshAgent>();
        foreach (Transform child in transform) {
            if (child.gameObject.tag == "Gun") {
                gun = child.gameObject;
            }
        }
        engine.ChangeState(new EnemyWaitState(this));
        engine.Update();
    }

    public void Alert() {
        isAlert = true;
        seenTimer = calmdownTime;
        Debug.Log("Alert!");
        if (engine.currentState.stateName != "alert") {
            engine.ChangeState(new EnemyAlertState(this));
        }
    }

    void CheckIfReachedDestination() {
        float distanceToDestination = Vector3.Distance(transform.position, agent.destination);
        if (distanceToDestination < 2f) {
            engine.ChangeState(new EnemyWaitState(this));
        }
    }

    void LateUpdate() {
        if (engine.currentState.stateName == "wander") {
            CheckIfReachedDestination();
        }

        if (wanderTimer >= waitTime) {
            engine.ChangeState(new EnemyWanderState(this));
            engine.Update();
            wanderTimer = 0;
        }
    }

    void FixedUpdate() {
        if (engine.currentState.stateName == "wait") {
            wanderTimer++;
        }

        if (seenTimer > 0) {
            seenTimer--;
        } 

        if (isAlert) {

        }
    }

    public void EquipWeapon() {
        gun.SetActive(true);
    }

    public void UnequipWeapon() {
        gun.SetActive(false);
    }
}
