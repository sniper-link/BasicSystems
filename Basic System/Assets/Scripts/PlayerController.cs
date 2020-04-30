using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    private Animator animController;
    private CharacterController characterController;

    public float rotateSpeed = 10;
    public float moveSpeed = 1;
    public float gravity = 20;
    public float jumpForce = 8;

    Vector3 moveDir;

    private void Awake()
    {
        animController = transform.GetComponent<Animator>();
        characterController = transform.GetComponent<CharacterController>();
        if (playerCamera == null)
        {
            playerCamera = transform.Find("MainCamera").GetComponent<Camera>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animController.SetBool("IsGrounded", true);
        characterController.stepOffset = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 camForward;
        Vector3 camRight;

        camForward = playerCamera.transform.forward;
        camRight = playerCamera.transform.right;

        /*if (moveX != 0 && moveY !=0)
        {
            //print(moveX + "|" + moveY);
            moveDir = transform.position + new Vector3(moveX*.1f, 0, moveY * .1f);
        }*/

        moveDir = new Vector3(((camForward.x * moveZ) + (camRight.x * moveX)) * moveSpeed, 0, ((camForward.z * moveZ) + (camRight.z * moveX)) * moveSpeed);

        if (characterController.isGrounded && Input.GetButton("Jump"))
        {
            moveDir.y = jumpForce;
            animController.SetBool("IsGrounded", characterController.isGrounded);
        }

        moveDir.y -= gravity * Time.deltaTime;

        characterController.Move(moveDir);

        moveDir = Vector3.zero;

        animController.SetBool("IsGrounded", characterController.isGrounded);
        animController.SetFloat("PlayerSpeed", moveDir.magnitude);
        print(characterController.isGrounded);
    }
}
