using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    private bool canJump;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask ground;
    public bool grounded;

    public Transform orientation;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDir;

    private Rigidbody rb;


    public GameObject switchCam;
    public GameObject buildingSystem;
    public bool placementMode;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        canJump = true;
        placementMode = false;
        
    }

    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);

        if (Input.GetKeyDown(KeyCode.K)) {
            switchCam.GetComponent<SwitchCamera>().ChangeCamera();
            placementMode = !placementMode;
            buildingSystem.SetActive(!buildingSystem.activeSelf);
        }

        if (!placementMode) {
            GetInput();
        }
        ControlSpeed();

        if (grounded) {
            rb.drag = groundDrag;
        }
        else {
            rb.drag = 0;
        }
        
    }

    private void FixedUpdate() {
        if (!placementMode) {
            MovePlayer();
        }

    }

    private void MovePlayer() {
        moveDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded) {
            rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded) {
            rb.AddForce(moveDir.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

    }

    private void GetInput() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(KeyCode.Space) && canJump && grounded) {
            canJump = false;
            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void ControlSpeed() {
        Vector3 velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if(velocity.magnitude > moveSpeed) {
            Vector3 limitedVelocity = velocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }

    private void Jump() {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump() {
        canJump = true;
    }
}
