using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Mode
{
    Unarmed,
    Melee,
    Range
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    private Animator animController;
    private CharacterController playerController;
    private PlayerInventory playerInventory;

    public float rotateSpeed = 10;
    public float moveSpeed = 6f;
    public float gravity = 10;
    public float jumpForce = 8;
    public float jumpHeight = 2.5f;

    private float jumpCooldown = 0;
    private float jumpTime = 0;

    public GameObject leftFoot;
    public GameObject rightFoot;

    public GameObject rightHand;
    public GameObject leftHand;

    // gear items
    public Gear curSword;
    public GameObject curBox;
    public GameObject curShield;
    public GameObject curTool;

    bool isGrounded;
    public bool autoJump = true;
    public bool canSprint = false;

    Vector3 moveDir;
    Vector3 jumpDir;

    Quaternion newRotation;

    static Quaternion testRotation;
    static Mode curPlayerMode;

    private void Awake()
    {
        animController = transform.GetComponent<Animator>();
        playerController = transform.GetComponent<CharacterController>();
        playerInventory = transform.GetComponent<PlayerInventory>();
        if (playerCamera == null)
        {
            playerCamera = transform.Find("MainCamera").GetComponent<Camera>();
        }
        //DontDestroyOnLoad(gameObject);
        if (testRotation != null)
        {
            transform.rotation = testRotation;
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

        if (isGrounded && jumpDir.y < 0)
        {
            jumpDir.y = -1f;
        }


        /*if (Input.GetButtonDown("Sprint") && canSprint)
        {
            moveSpeed *= 3;
        }
        else if (Input.GetButtonUp("Sprint") && canSprint)
        {
            moveSpeed /= 3;
        }*/

        if (Input.GetButtonDown("Switch"))
        {
            if (curPlayerMode == Mode.Melee)
            {
                curPlayerMode = Mode.Range;
                animController.SetBool("RangeMode", true);
                animController.SetBool("MeleeMode", false);
            }
            else if (curPlayerMode == Mode.Range)
            {
                curPlayerMode = Mode.Melee;
                animController.SetBool("MeleeMode", true);
                animController.SetBool("RangeMode", false);
            }
            else if (curPlayerMode == Mode.Unarmed)
            {
                curPlayerMode = Mode.Melee;
                animController.SetBool("MeleeMode", true);
                animController.SetBool("RangeMode", false);
            }
            Debug.Log(curPlayerMode);
        }

        if (Input.GetButtonDown("LMB"))
        {
            if (curPlayerMode == Mode.Melee)
            {
                // attack animation
                animController.SetTrigger("Attacking");
                //curSword.gearBase.GetComponent<WeaponScript>().UseWeapon();
            }
            else if (curPlayerMode == Mode.Range)
            {
                curPlayerMode = Mode.Melee;
            }
            else if (curPlayerMode == Mode.Unarmed)
            {
                curPlayerMode = Mode.Melee;
            }
        }

        if (Input.GetButtonDown("RMB"))
        {
            if (curPlayerMode == Mode.Melee)
            {
                // attack animation
                //animController.SetTrigger("Attacking");
                animController.SetBool("ShieldMode", true);
            }
            else if (curPlayerMode == Mode.Range)
            {
                // zoom in and aim
            }
            else if (curPlayerMode == Mode.Unarmed)
            {
                curPlayerMode = Mode.Melee;
            }
        }
        else if (Input.GetButtonUp("RMB"))
        {
            if (curPlayerMode == Mode.Melee)
            {
                // attack animation
                //animController.SetTrigger("Attacking");
                animController.SetBool("ShieldMode", false);
            }
            else if (curPlayerMode == Mode.Range)
            {
                // zoom in and aim
            }
            else if (curPlayerMode == Mode.Unarmed)
            {
                curPlayerMode = Mode.Melee;
            }
        }

        // Testing
        if (Input.GetKeyDown("v") && playerInventory)
        {
            Item testItem = playerInventory.GetFromItemBag("Gold", out bool haveItem1);
            Debug.Log(testItem.itemName + ": " + testItem.amount);
            Item testItem2 = playerInventory.GetFromItemBag("Token", out bool haveItem2);
            Debug.Log(testItem2.itemName + ": " + testItem2.amount);
        }

        moveDir.x = (camForward.x * moveZ) + (camRight.x * moveX);
        moveDir.z = (camForward.z * moveZ) + (camRight.z * moveX);
        moveDir *= moveSpeed * Time.deltaTime;

        if (moveX != 0 || moveZ != 0)
        {
            newRotation = Quaternion.LookRotation(moveDir);
            testRotation = newRotation;
        }
 
        playerController.Move(moveDir);

        if (autoJump)
        {
            // check if player have ground to step on

            RaycastHit leftHit;
            RaycastHit rightHit;

            Ray leftRay = new Ray(leftFoot.transform.position, transform.up * -1);
            Ray rightRay = new Ray(rightFoot.transform.position, transform.up * -1);

            if (!Physics.Raycast(leftRay, out leftHit, 1.9f) && !Physics.Raycast(rightRay, out rightHit, 1.9f))
            {
                if (moveDir.magnitude * 10 >= 1f && isGrounded)
                {
                    jumpDir.y = Mathf.Sqrt(0.05f * jumpHeight);
                    //Debug.Log("jump");
                }
                else
                {
                    //Debug.Log("ledge");
                }
            }
        }
        else if (!autoJump)
        {
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                jumpDir.y = Mathf.Sqrt(0.05f * jumpHeight);
            }
        }

        jumpDir.y -= 0.8f * Time.deltaTime;

        playerController.Move(jumpDir);

        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 0.2f);
        
        animController.SetBool("IsGrounded", isGrounded);
        animController.SetFloat("PlayerSpeed", moveDir.magnitude*10);
        //Debug.Log(moveDir.magnitude*10);
        moveDir = Vector3.zero;
    }

    private void OnEnable()
    {
        curPlayerMode = Mode.Unarmed;
    }

    private void PlayerJump()
    {
        playerController.Move(jumpDir);
    }

    public void TestAttach(GameObject curObject)
    {
        curObject.transform.parent = rightHand.transform;
        curObject.transform.position = rightHand.transform.position;
        curObject.transform.rotation = rightHand.transform.rotation * Quaternion.Euler(0, 0, 90);
    }

    public void AttachGear(Gear curGear)
    {
        GameObject curGameObject = curGear.gearBase;
        curGameObject.transform.parent = rightHand.transform;
        curGameObject.transform.position = rightHand.transform.position;
        curGameObject.transform.rotation = rightHand.transform.rotation * Quaternion.Euler(0, 0, 90);
    }
}
