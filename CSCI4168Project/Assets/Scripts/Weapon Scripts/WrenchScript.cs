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
    private

  
    void Start()
    {
        cam = Camera.main;
        turretLayer = LayerMask.GetMask("Tower");
        
    }

    private void OnDestroy() {
        ResetHighlight();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, wrenchRange, turretLayer)) {
            if (hit.collider.gameObject != lastHighlightedObject) {
                ResetHighlight();
                HighlightObject(hit.collider.gameObject);
                lastHighlightedObject = hit.collider.gameObject;
            }
        }
        else {
            ResetHighlight();
            lastHighlightedObject = null;
        }

        if (Input.GetButtonDown("Fire1") && lastHighlightedObject != null) {
            lastHighlightedObject.GetComponentInChildren<TurretStats>().Upgrade();
            Transform repairUITransform = lastHighlightedObject.transform.Find("RepairUI");
            if(repairUITransform != null) {
                repairUITransform.gameObject.GetComponent<RepairUI>().UpdateText();
            }
        }

        if(Input.GetButtonDown("Fire2") && lastHighlightedObject != null) {
            lastHighlightedObject.GetComponentInChildren<TurretStats>().Repair();
            Transform repairUITransform = lastHighlightedObject.transform.Find("RepairUI");
            if (repairUITransform != null) {
                repairUITransform.gameObject.GetComponent<RepairUI>().UpdateText();
            }
        }

    }

    private void HighlightObject(GameObject obj) {
        if (obj != null) {
            obj.GetComponent<Outline>().enabled = true;
            Transform repairUITransform = obj.transform.Find("RepairUI");
            if(repairUITransform != null) {
                repairUITransform.gameObject.SetActive(true);
            }
        }
    }

    private void ResetHighlight() {
        if(lastHighlightedObject != null) {
            lastHighlightedObject.GetComponent<Outline>().enabled = false;
            Transform repairUITransform = lastHighlightedObject.transform.Find("RepairUI");
            if (repairUITransform != null) {
                repairUITransform.gameObject.SetActive(false);
            }
        }
    }

}
