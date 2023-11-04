using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject mouseIndicator;
    [SerializeField] private GameObject cellIndicator;
    [SerializeField] private InputManager inputManager;

    [SerializeField] private GameObject turretPref;

    [SerializeField] Grid grid;
    private int selectedObjectIndex = -1;

    private void Start() {
        StopPlacement();
    }
    public void StartPlacement() {
        StopPlacement();
        selectedObjectIndex = 0;
        cellIndicator.SetActive(true);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    private void PlaceStructure() {
        if (inputManager.IsPointerOverUI()) {
            return;
        }

        Vector3 mousePos = inputManager.GetSelectedMapPosition();
        Vector3Int gridPos = grid.WorldToCell(mousePos);
        GameObject turret = Instantiate(turretPref);
        turret.transform.position = grid.CellToWorld(gridPos);

    }

    private void StopPlacement() {
        selectedObjectIndex = -1;
        cellIndicator.SetActive(false);
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
    }

    void Update()
    {
        if(selectedObjectIndex < 0) {
            return;
        }
        Vector3 mousePos = inputManager.GetSelectedMapPosition();
        Vector3Int gridPos = grid.WorldToCell(mousePos);
        mouseIndicator.transform.position = mousePos;
        cellIndicator.transform.position = grid.CellToWorld(gridPos);
    }
}
