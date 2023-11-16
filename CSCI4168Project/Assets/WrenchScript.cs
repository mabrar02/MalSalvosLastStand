using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float wrenchRange;
    private GameObject lastHighlightedObject;

    private Camera cam;
    private RaycastHit hit;
    private LayerMask turretLayer;

  
    void Start()
    {
        cam = Camera.main;
        turretLayer = LayerMask.GetMask("Tower");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, wrenchRange, turretLayer)) {
            if(hit.collider.gameObject !=  lastHighlightedObject) {
                Debug.Log("TURRET DETECTED: " + hit.transform.name);
                lastHighlightedObject = hit.collider.gameObject;
            }
        }
        else {
            lastHighlightedObject = null;
        }

        

    }

}
