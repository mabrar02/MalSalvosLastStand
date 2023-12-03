using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class is used to transition between the player camera and placement camera
 */
public class SwitchCamera : MonoBehaviour
{
    public GameObject playerCam;
    public GameObject placementCam;
    public int manager = 0;

    // sets the trigger to do the fade to black transition
    public void ChangeCamera() {
        GetComponent<Animator>().SetTrigger("Change");
    }

    // swaps between player and placement camera
    public void ManageCameras() {
        if(manager == 0) {
            ActivePlacementCam();
            manager = 1;
        }
        else {
            ActivePlayerCam();
            manager = 0;
        }
    }

    // disables placement camera and locks cursor
    public void ActivePlayerCam() {
        placementCam.SetActive(false);
        playerCam.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // disables player camera and unlocks cursor
    public void ActivePlacementCam() {
        playerCam.SetActive(false);
        placementCam.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
