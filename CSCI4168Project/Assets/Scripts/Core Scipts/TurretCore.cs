using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class used to handle turret core application logic
 */
public class TurretCore : MonoBehaviour
{

    [SerializeField] private float coreRange;
    [SerializeField] private int coreIndex;
    [SerializeField] private GameObject coreAuraPrefab;
    [SerializeField] private AudioSource applyCoreSE;
    private GameObject lastHighlightedObject;



    private Camera cam;
    private RaycastHit hit;
    private LayerMask turretLayer;


    void Start() {
        // initialize variables
        cam = Camera.main;
        turretLayer = LayerMask.GetMask("Tower");

    }

    private void OnDestroy() {
        ResetHighlight();
    }

    // Update is called once per frame
    void Update() {

        // if looking at a turret, check if it was what you last looked at. if not, disable previous highlight and highlight current tower
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, coreRange, turretLayer)) {
            if (hit.collider.gameObject != lastHighlightedObject) {
                ResetHighlight();
                HighlightObject(hit.collider.gameObject);
                lastHighlightedObject = hit.collider.gameObject;
            }
        }
        else {
            // if not looking at a turret, don't highlight any 
            ResetHighlight();
            lastHighlightedObject = null;
        }

        // if a turret is highlighted, and you click, attempt to set the current core
        if (Input.GetButtonDown("Fire1") && lastHighlightedObject != null) {
            if (lastHighlightedObject.GetComponentInChildren<TurretStats>().SetCore(coreIndex)) {
                // if core isn't already applied, get rid of the held core from your inventory and apply the aura on the tower
                PlayerInventoryControl.instance.RemoveHeldItemFromInventory();
                Instantiate(coreAuraPrefab, lastHighlightedObject.transform.position, Quaternion.identity, lastHighlightedObject.transform);
                AudioSource.PlayClipAtPoint(applyCoreSE.clip, transform.position);
            };
        }

    }

    // turn on the highlighted outline
    private void HighlightObject(GameObject obj) {
        if (obj != null) {
            obj.GetComponent<Outline>().enabled = true;
        }
    }

    // turn off the highlighted object's outline
    private void ResetHighlight() {
        if (lastHighlightedObject != null) {
            lastHighlightedObject.GetComponent<Outline>().enabled = false;
        }
    }
}
