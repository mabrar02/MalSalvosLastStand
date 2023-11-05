using System;
using System.Collections;
using System.Collections.Generic;
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
    private PlayerStats playerStats;

    [SerializeField] private Vector3 spaceReq;

    [SerializeField] private PreviewSystem preview; 

    private Vector3Int lastDetectedPos = Vector3Int.zero;

    private void Start() {
        StopPlacement();
        playerStats = player.GetComponent<PlayerStats>();
    }

    public void StartPlacement() {
        if (playerStats.GetGears() - turretCost < 0) {
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
        if(!placementValidity) {
            return;
        }

        GameObject turret = Instantiate(turretPref);
        turret.transform.position = grid.CellToWorld(gridPos);

        preview.UpdatePosition(grid.CellToWorld(gridPos), false);
        playerStats.UseGears(turretCost);

        if(playerStats.GetGears() - turretCost < 0) {
            StopPlacement();
        }
    }

    private bool CheckPlacementValidity() {
        Collider[] hitColliders = Physics.OverlapBox(mousePos, spaceReq, Quaternion.identity, ~LayerMask.GetMask("ground"));
        return hitColliders.Length == 0;
    }

    private void StopPlacement() {
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

            mouseIndicator.transform.position = mousePos;
            preview.UpdatePosition(grid.CellToWorld(gridPos), placementValidity);
            lastDetectedPos = gridPos;
        }

    }
}
