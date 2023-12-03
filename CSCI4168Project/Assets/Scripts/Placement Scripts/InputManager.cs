using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * class used to handle turret placement input
 */
public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera placementCam;
    private Vector3 lastPos;
    [SerializeField] private LayerMask placementLayer;

    public event Action OnClicked;
    public event Action OnExit;

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            OnClicked?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            OnExit?.Invoke();
        }
    }
    
    // use event system to detect if mouse is over UI to not detect a click
    public bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();

    // based on where the player clicks, place the turret there
    public Vector3 GetSelectedMapPosition() {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = placementCam.nearClipPlane;

        Ray ray = placementCam.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100, placementLayer)) {
            lastPos = hit.point;
        }
        return lastPos;
    }
}
