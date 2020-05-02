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

    private float jumpCooldown = 0;
    private float jumpTime = 0;

    private const float JUMP_TIME_MAX = 1.5f;
    private const float JUMP_TIME_MIN = 0;

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

        moveDir = new Vector3(((camForward.x * moveZ) + (camRight.x * moveX)) * moveSpeed * Time.deltaTime, 0, ((camForward.z * moveZ) + (camRight.z * moveX)) * moveSpeed * Time.deltaTime);

        if (moveX != 0 || moveZ != 0)
        {
            newRotation = Quaternion.LookRotation(moveDir);
        }

        /*if (playerController.isGrounded && Input.GetButtonDown("Jump"))
        {
            moveDir.y += jumpForce * Time.deltaTime;
            if (jumpTime < JUMP_TIME_MAX)
            {
                
                animController.SetBool("IsGrounded", playerController.isGrounded);
                //jumpCooldown += 3f;
                jumpTime = Mathf.Clamp(jumpTime += Time.deltaTime, JUMP_TIME_MIN, JUMP_TIME_MAX);
                //print(jumpTime);
            }
        }
        else if (!playerController.isGrounded && Input.GetButtonUp("Jump"))
        {
            moveDir.y -= gravity * Time.deltaTime;
            if (jumpTime > JUMP_TIME_MIN)
            {
                
                jumpTime = Mathf.Clamp(jumpTime -= Time.deltaTime, JUMP_TIME_MIN, JUMP_TIME_MAX);
                //print(jumpTime);
            }
        }*/

        /*if (Input.GetButton("Jump") && jumpTime < JUMP_TIME_MAX)
        {
            moveDir.y = jumpForce * Time.deltaTime;
            jumpTime = Mathf.Clamp(jumpTime += Time.deltaTime, JUMP_TIME_MIN, JUMP_TIME_MAX);
        }
        else if (!Input.GetButton("Jump") && jumpTime > JUMP_TIME_MIN)
        {
            moveDir.y -= gravity * Time.deltaTime;
            jumpTime = Mathf.Clamp(jumpTime -= Time.deltaTime, JUMP_TIME_MIN, JUMP_TIME_MAX);
        }
        else if (!playerController.isGrounded)
        {
            moveDir.y -= gravity * Time.deltaTime;
        }*/

       
        playerController.Move(moveDir);
        
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpDir.y = Mathf.Sqrt(0.05f * 4f);
        }

        jumpDir.y -= 0.8f * Time.deltaTime;

        playerController.Move(jumpDir);

        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 0.2f);
        

        /*if (jumpCooldown > 0)
        {
            jumpCooldown -= Time.deltaTime;
            jumpCooldown = Mathf.Clamp(jumpCooldown, 0, 3);
        }*/

        animController.SetBool("IsGrounded", isGrounded);
        animController.SetFloat("PlayerSpeed", moveSpeed);
        moveDir = Vector3.zero;
        //print(playerController.isGrounded);
    }

    private void PlayerJump()
    {
        playerController.Move(jumpDir);
    }
}
