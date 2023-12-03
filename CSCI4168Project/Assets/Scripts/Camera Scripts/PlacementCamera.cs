using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class used to handle top down camera movement
 */
public class PlacementCamera : MonoBehaviour
{
    private Vector3 initialPos;
    [SerializeField] private float camSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;

    private Vector3 targetPosition;

    private void Awake()
    {
        // grab initial pos to reset cam each time
        initialPos = transform.position;
        
    }

    private void OnEnable() {

        // whenever placement cam enabled, move back to the initial spot
        transform.position = initialPos;
    }

    private void Update() {
        // take in directional input to move camera
        Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        targetPosition += camSpeed * Time.deltaTime * moveDir;

        // smooth camera movement
        targetPosition = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * acceleration);

        transform.position = targetPosition;

        if (moveDir == Vector3.zero) {
            targetPosition = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * deceleration);
            transform.position = targetPosition;
        }

        // set position to desired location
        transform.position = new Vector3(transform.position.x, initialPos.y, transform.position.z);
    }
}
