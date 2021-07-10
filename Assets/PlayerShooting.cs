using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform firePoint;
    public GameObject bulletPrefab;

    public string gunStyle = "semi";

    public float bulletForce = 20f;

    private float shootDelay = 5f;
    private float shootTimer;

    void Update()
    {
        Shoot();
    }

    void FixedUpdate() {
        if (shootTimer > 0) {
            shootTimer--;
        }
    }

    void Shoot() {
        switch (gunStyle) {
            case "full":
                if (Input.GetMouseButton(0)) {
                    Fire();
                }
                break; 
            default:
                if (Input.GetMouseButtonDown(0)) {
                    Fire();
                }
                break;           
        }
    }

    void Fire() 
    {
        if (shootTimer <= 0) {
            shootTimer = shootDelay;
            GameObject bullet = ObjectPoolingManager.Instance.GetBullet();
            bullet.transform.position = firePoint.position;
            bullet.transform.forward = firePoint.forward;
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.isKinematic = false;
            rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        }
    }
}
