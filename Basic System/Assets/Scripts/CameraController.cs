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

    bool reduceDist = false;

    bool gameFocus = false;

    private float distance = 10;
    public Transform camLookAt;
    Camera playerCam;
    Transform cameraTrans;
    SphereCollider camCollider;

    Vector3 newPlayerPos;

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
        if (gameFocus)
        {
            newPlayerPos = camLookAt.position;
            newPlayerPos.y += 10;
            camCurX += (Input.GetAxis("Mouse X") * rotSpeedX);
            camCurY -= (Input.GetAxis("Mouse Y") * rotSpeedY);

            camCurY = Mathf.Clamp(camCurY, CAM_Y_MIN, CAM_Y_MAX);

            if (reduceDist)
            {
                distance++;
            }
        }
        
    }

    void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(camCurY, camCurX, 0);
        cameraTrans.position = camLookAt.position + rotation * dir;
        cameraTrans.LookAt(camLookAt);

    }

    private void OnTriggerEnter(Collider other)
    {
        print("hit something");
        reduceDist = true;   
    }

    private void OnTriggerExit(Collider other)
    {
        print("hit nothing");
        reduceDist = false;
    }

    private void OnApplicationFocus(bool focus)
    {
        //print(focus);
        gameFocus = focus;
    }

    private void OnApplicationPause(bool pause)
    {
        //print("in pause");
    }

    private void OnApplicationQuit()
    {
        //print("quit app");
    }
}
