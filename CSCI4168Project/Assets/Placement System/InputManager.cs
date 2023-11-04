using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera placementCam;
    private Vector3 lastPos;
    [SerializeField] private LayerMask placementLayer;

    void Start()
    {
        
    }

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
