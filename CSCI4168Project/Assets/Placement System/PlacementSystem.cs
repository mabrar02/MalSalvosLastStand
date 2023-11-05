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

    private Renderer previewRenderer;

    private Vector3 mousePos;
    private Vector3Int gridPos;

    [SerializeField] private Vector3 spaceReq;

    private void Start() {
        StopPlacement();
        previewRenderer = cellIndicator.GetComponentInChildren<Renderer>();
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

        bool placementValidity = CheckPlacementValidity();
        if(!placementValidity) {
            return;
        }

        GameObject turret = Instantiate(turretPref);
        turret.transform.position = grid.CellToWorld(gridPos);

    }

    private bool CheckPlacementValidity() {
        Collider[] hitColliders = Physics.OverlapBox(mousePos, spaceReq, Quaternion.identity, ~LayerMask.GetMask("ground"));
        return hitColliders.Length == 0;
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
        mousePos = inputManager.GetSelectedMapPosition();
        gridPos = grid.WorldToCell(mousePos);

        bool placementValidity = CheckPlacementValidity();
        Color newColor = placementValidity ? new Color(0f, 1f, 0f, previewRenderer.material.color.a) : new Color(1f, 0f, 0f, previewRenderer.material.color.a);
        previewRenderer.material.color = newColor;

        mouseIndicator.transform.position = mousePos;
        cellIndicator.transform.position = grid.CellToWorld(gridPos);
    }
}
