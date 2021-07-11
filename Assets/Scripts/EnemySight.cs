using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{

    private Enemy owner;

    // Start is called before the first frame update
    void Start()
    {
        owner = transform.parent.gameObject.GetComponent<Enemy>();
    }

    void OnTriggerEnter(Collider col) {
        Debug.Log("Triggered");
        if (col.gameObject.tag == "Player") {
            owner.Alert();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
