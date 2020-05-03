using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    private Animator animController;
    private CharacterController playerController;

    public float rotateSpeed = 10;
    public float moveSpeed = 6f;
    public float gravity = 10;
    public float jumpForce = 8;
    public float jumpHeight = 2.5f;

    private float jumpCooldown = 0;
    private float jumpTime = 0;

    public GameObject leftFoot;
    public GameObject rightFoot;

    bool isGrounded;

    Vector3 moveDir;
    Vector3 jumpDir;

    Quaternion newRotation;

    private void Awake()
    {
        animController = transform.GetComponent<Animator>();
        playerController = transform.GetComponent<CharacterController>();
        if (playerCamera == null)
        {
            playerCamera = transform.Find("MainCamera").GetComponent<Camera>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animController.SetBool("IsGrounded", true);
        playerController.stepOffset = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 camForward;
        Vector3 camRight;

        isGrounded = playerController.isGrounded;

        camForward = playerCamera.transform.forward;
        camRight = playerCamera.transform.right;

        /*if (moveX != 0 && moveY !=0)
        {
            //print(moveX + "|" + moveY);
            moveDir = transform.position + new Vector3(moveX*.1f, 0, moveY * .1f);
        }*/

        if (isGrounded && jumpDir.y < 0)
        {
            jumpDir.y = -1f;
        }


        if (Input.GetButtonDown("Sprint"))
        {
            moveSpeed *= 3;
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            moveSpeed /= 3;
        }

        moveDir.x = (camForward.x * moveZ) + (camRight.x * moveX);
        moveDir.z = (camForward.z * moveZ) + (camRight.z * moveX);
        moveDir *= moveSpeed * Time.deltaTime;

        if (moveX != 0 || moveZ != 0)
        {
            newRotation = Quaternion.LookRotation(moveDir);
        }

        // check if player have ground to step on

        RaycastHit leftHit;
        RaycastHit rightHit;

        Ray leftRay = new Ray(leftFoot.transform.position, transform.up * -1);
        Ray rightRay = new Ray(rightFoot.transform.position, transform.up * -1);

        if (Physics.Raycast(leftRay, out leftHit, 1.9f) && Physics.Raycast(rightRay, out rightHit, 1.9f))
        {
            //Debug.Log(leftHit.collider.name);
            //Debug.Log("on ground");
        }
        else
        {
            //Debug.Log(moveDir.magnitude);
            if (moveDir.magnitude * 10 >= 1f && isGrounded)
            {
                jumpDir.y = Mathf.Sqrt(0.05f * jumpHeight);
                Debug.Log("jump");
            }
            else
            {
                Debug.Log("ledge");
            }
            
        }

        Debug.DrawRay(leftFoot.transform.position, transform.up * -2, Color.green, 1);
        Debug.DrawRay(rightFoot.transform.position, transform.up * -2, Color.red, 1);

        //Debug.DrawRay(leftFoot.transform.position, (transform.up + (-0.7f * transform.forward)) * -1, Color.green, 1);
        //Debug.DrawRay(rightFoot.transform.position, (transform.up + new Vector3(-0.7f, 0, 0)) * -1, Color.red, 1);

       
        playerController.Move(moveDir);
        
        /*if(Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpDir.y = Mathf.Sqrt(0.05f * jumpHeight);
        }*/

        jumpDir.y -= 0.8f * Time.deltaTime;

        playerController.Move(jumpDir);

        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 0.2f);
        
        animController.SetBool("IsGrounded", isGrounded);
        animController.SetFloat("PlayerSpeed", moveDir.magnitude*10);
        //Debug.Log(moveDir.magnitude*10);
        moveDir = Vector3.zero;
    }

    private void PlayerJump()
    {
        playerController.Move(jumpDir);
    }
}
