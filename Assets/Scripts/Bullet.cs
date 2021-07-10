using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float lifetime = 10f;
    private float lifeTimer;

    void OnEnable()
    {
        lifeTimer = lifetime;
    }

    void FixedUpdate() 
    {
        lifeTimer--;

        if (lifeTimer <= 0f) {
            gameObject.SetActive(false);
        }
    }
}
