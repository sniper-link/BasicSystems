using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    const float CAM_Y_MIN = 10f;
    const float CAM_Y_MAX = 60f;

    float camCurX = 0;
    float camCurY = 10;

    float rotSpeedX = 8;
    float rotSpeedY = 5;

    public float distance = 10;
    public Transform playerTrans;
    Camera playerCam;
    Transform cameraTrans;
    SphereCollider camCollider;

    private void Awake()
    {
        playerCam = this.GetComponent<Camera>();
        cameraTrans = playerCam.transform;
        camCollider = this.GetComponent<SphereCollider>();
        Cursor.visible = false; // hide mouse cursor
        Cursor.lockState = CursorLockMode.Locked; // Bound cursor with in the screen
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        camCurX += (Input.GetAxis("Mouse X") * rotSpeedX);
        camCurY -= (Input.GetAxis("Mouse Y") * rotSpeedY);

        camCurY = Mathf.Clamp(camCurY, CAM_Y_MIN, CAM_Y_MAX);
    }

    void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(camCurY, camCurX, 0);
        cameraTrans.position = playerTrans.position + rotation * dir;
        cameraTrans.LookAt(playerTrans.position);
    }
}
