using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolingManager : MonoBehaviour
{

    private static BulletPoolingManager instance;
    public static BulletPoolingManager Instance { get { return instance; }}

    public GameObject bulletPrefab;
    public int bulletAmount;

    private List<GameObject> bullets;

    void Awake() {
        instance = this;

        // Preload bullets
        bullets = new List<GameObject>(bulletAmount);

        for (int i = 0; i < bulletAmount; i++) {
            GameObject prefabInstance = Instantiate (bulletPrefab);
            prefabInstance.transform.SetParent (transform);
            prefabInstance.SetActive (false);

            bullets.Add(prefabInstance);
        }
    }

    public GameObject GetBullet() {
        foreach (GameObject bullet in bullets) {
            if (!bullet.activeInHierarchy) {
                bullet.SetActive(true);
                return bullet;
            }
        }

        GameObject prefabInstance = Instantiate(bulletPrefab);
        prefabInstance.transform.SetParent (transform);
        bullets.Add (prefabInstance);

        return prefabInstance;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
