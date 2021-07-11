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
    public float attackRange = 15;
    public float alertUpdateTime = 10f;
    public float alertUpdateTimer;

    public float shootTimer = 0;
    public float shootDelay = 5f;
    public float bulletForce = 50f;
    public Transform firePoint;

    public string currentState;


    private float seenTimer = 50;

    [HideInInspector]
    public Vector3 spawnPoint;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        isAlert = false;
        spawnPoint = gameObject.transform.position;
        agent = gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        foreach (Transform child in transform) {
            if (child.gameObject.tag == "Gun") {
                gun = child.gameObject;
            }
        }
        foreach (Transform child in gun.transform) {
            if (child.gameObject.tag == "FirePoint") {
                firePoint = child;
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
            engine.Update();
        } else {
            engine.Update();
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

        if (shootTimer > 0) {
            shootTimer--;
        } 

        currentState = engine.currentState.stateName;

        if (isAlert) {
            if (alertUpdateTimer > 0) {
                alertUpdateTimer--;
            }

            if (seenTimer <= 0) {
                isAlert = false;
                engine.ChangeState(new EnemyWanderState(this));
                engine.Update();
                return;
            }

            if (Vector3.Distance(transform.position, player.transform.position) <= attackRange && shootTimer <= 0) {
                engine.ChangeState(new EnemyAttackState(this));
                engine.Update();
                shootTimer = shootDelay;
                engine.ChangeState(new EnemyAlertState(this));
                engine.Update();
                return;
            } else {
                if (engine.currentState.stateName != "alert") {
                    engine.ChangeState(new EnemyAlertState(this));
                }
                if (alertUpdateTimer <= 0) {
                    engine.Update();
                    return;
                }
            }
        }
    }

    public void EquipWeapon() {
        gun.SetActive(true);
    }

    public void UnequipWeapon() {
        gun.SetActive(false);
    }
}
