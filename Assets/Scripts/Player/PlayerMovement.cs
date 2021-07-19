using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject firePoint;
    private CinemachineVirtualCamera cam;

    public float walkSpeed = 35f;
    public float sprintSpeed = 50f;
    public float maxEnergy = 50f;
    public bool isSprinting;
    public float sprintStartupTime;
    public float sprintFinishTime;
    
    private float currentEnergy;
    private float currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        foreach (Transform child in transform) {
            if (child.gameObject.tag == "FirePoint") {
                firePoint = child.gameObject;
            }
        }

        cam = GameObject.FindGameObjectWithTag("PlayerVCam").GetComponent<CinemachineVirtualCamera>();

        cam.Follow = transform;

        currentSpeed = walkSpeed;
        currentEnergy = maxEnergy;
        isSprinting = false;
    }

    void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) {
            ToggleSprint();
        }
    }

    private void MovePlayer()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        // Translate Y axis from Input into a X/Z vector
        Vector3 direction = new Vector3(hInput, 0, vInput);
        rb.AddForce(direction.normalized * currentSpeed);
    }

    private void ToggleSprint() 
    {
        Debug.Log("Toggling sprint");
        isSprinting = !isSprinting;
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", currentSpeed,
            "to", isSprinting ? walkSpeed : sprintSpeed,
            "time", isSprinting ? sprintFinishTime : sprintStartupTime,
            "onupdatetarget", gameObject,
            "onupdate", "updateSpeed",
            "easetype", iTween.EaseType.easeOutQuad
        ));
    }

    void updateSpeed(float newValue) 
    {
        currentSpeed = newValue;
    }

    protected void LateUpdate() 
    {
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }

    private void RotatePlayer() 
    {
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit hitinfo;
        if (Physics.Raycast (ray, out hitinfo, 1000)) {
            transform.LookAt(hitinfo.point);
            firePoint.transform.LookAt(hitinfo.point);
        }
    }
}
