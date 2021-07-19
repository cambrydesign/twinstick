using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 100f, NavMesh.AllAreas)) {
            GameObject enemyObj = Instantiate(enemy, transform.position, transform.localRotation);
            enemyObj.GetComponent<NavMeshAgent>().Warp(hit.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
