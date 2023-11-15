using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject mouseIndicator;
    [SerializeField] private InputManager inputManager;

    [SerializeField] private GameObject turretPref;
    [SerializeField] private int turretCost;

    [SerializeField] private GameObject player;

    [SerializeField] Grid grid;
    private int selectedObjectIndex = -1;


    private Vector3 mousePos;
    private Vector3Int gridPos;

    [SerializeField] private float towerRadius;
    [SerializeField] private float pathRadius;

    [SerializeField] private PreviewSystem preview; 

    private Vector3Int lastDetectedPos = Vector3Int.zero;


    private void Start() {
        StopPlacement();
    }

    public void StartPlacement() {
        if (GameManager.Instance.GetGears() - turretCost < 0) {
            Debug.Log("NOT ENOUGH GEARS!");
            return;
        }
        StopPlacement();
        selectedObjectIndex = 0;
        preview.StartShowingPlacementPreview(turretPref);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    private void PlaceStructure() {
        if (inputManager.IsPointerOverUI()) {
            return;
        }

        bool placementValidity = CheckPlacementValidity();
        bool pathValidity = CheckPathPlacementValidity();

   
        if(!placementValidity || !pathValidity) {

            return;
        }

        GameObject turret = Instantiate(turretPref);
        turret.transform.position = grid.CellToWorld(gridPos);

        preview.UpdatePosition(grid.CellToWorld(gridPos), false);
        GameManager.Instance.UseGears(turretCost);

        if(GameManager.Instance.GetGears() - turretCost < 0) {
            StopPlacement();
        }

    }

    private bool CheckPlacementValidity() {
        int excludeLayers = LayerMask.GetMask("ground", "path");
        Collider[] hitColliders = Physics.OverlapSphere(mousePos, towerRadius, ~excludeLayers);
        CapsuleCollider[] capsuleColliders = hitColliders
            .Select(col => col.GetComponent<CapsuleCollider>())
            .Where(collider => collider != null)
            .ToArray();
        return capsuleColliders.Length == 0;
    }

    private bool CheckPathPlacementValidity() {
        Collider[] hitColliders = Physics.OverlapSphere(mousePos, pathRadius, LayerMask.GetMask("path"));

        return hitColliders.Length == 0;
    }


    public void StopPlacement() {
        selectedObjectIndex = -1;
        preview.StopShowingPreview();
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
        lastDetectedPos = Vector3Int.zero;
    }

    void Update()
    {
        if(selectedObjectIndex < 0) {
            return;
        }
        mousePos = inputManager.GetSelectedMapPosition();
        gridPos = grid.WorldToCell(mousePos);
        if(lastDetectedPos != gridPos) {
            bool placementValidity = CheckPlacementValidity();
            bool pathValiditity = CheckPathPlacementValidity();

            mouseIndicator.transform.position = mousePos;
            preview.UpdatePosition(grid.CellToWorld(gridPos), placementValidity && pathValiditity);
            lastDetectedPos = gridPos;
        }

    }
}
