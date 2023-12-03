using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * class used to rotate players camera with the mouse
 */
public class PlayerCamera : MonoBehaviour
{
    public float sensX;
    public float sensY;
    public Transform orientation;
    public GameObject player;

    private float xRotation;
    private float yRotation;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {

        // get directional input from the mouse
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        
        // rotate player with mouse movement
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        player.transform.rotation = Quaternion.Euler(0, yRotation, 0);
        player.transform.Find("PivotArm").localRotation = Quaternion.Euler(xRotation, 0, 0);

    }
}
