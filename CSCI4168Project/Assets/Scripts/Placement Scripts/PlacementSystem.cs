using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
 * class used to place turrets on the map
 */
public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private GameObject mouseIndicator;
    [SerializeField] private InputManager inputManager;

    [SerializeField] private GameObject turretPref;
    [SerializeField] private GameObject turretPreview;
    [SerializeField] private int turretCost;

    [SerializeField] private GameObject player;

    [SerializeField] Grid grid;
    private int selectedObjectIndex = -1;


    private Vector3 mousePos;
    private Vector3Int gridPos;

    [SerializeField] private float towerRadius;
    [SerializeField] private float pathRadius;

    [SerializeField] private PreviewSystem preview;
    [SerializeField] private AudioSource placementSE;

    private Vector3Int lastDetectedPos = Vector3Int.zero;


    private void Start() {
        StopPlacement();
    }
    
    // if you have enough gears, you can start previewing where to place your tower
    public void StartPlacement() {
        if (GameManager.Instance.GetGears() - turretCost < 0) {
            MenuManager.Instance.SetError("Not enough gears!");
            return;
        }
        StopPlacement();
        selectedObjectIndex = 0;
        preview.StartShowingPlacementPreview(turretPreview);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    // check if the placement is valid, if so, place it
    private void PlaceStructure() {
        if (inputManager.IsPointerOverUI()) {
            return;
        }

        bool placementValidity = CheckPlacementValidity();
        bool pathValidity = CheckPathPlacementValidity();

   
        if(!placementValidity || !pathValidity) {

            return;
        }

        // instantiate a turret at the world space converted from a grid
        GameObject turret = Instantiate(turretPref);
        placementSE.Play();

        turret.transform.position = grid.CellToWorld(gridPos);

        preview.UpdatePosition(grid.CellToWorld(gridPos), false);
        GameManager.Instance.UseGears(turretCost);

        if(GameManager.Instance.GetGears() - turretCost < 0) {
            StopPlacement();
        }

    }
    
    // check if the turret is being placed near any other towers
    private bool CheckPlacementValidity() {
        int excludeLayers = LayerMask.GetMask("ground", "path");
        Collider[] hitColliders = Physics.OverlapSphere(mousePos, towerRadius, ~excludeLayers);
        CapsuleCollider[] capsuleColliders = hitColliders
            .Select(col => col.GetComponent<CapsuleCollider>())
            .Where(collider => collider != null)
            .ToArray();
        return capsuleColliders.Length == 0;
    }

    // check if the turret is being placed on a path
    private bool CheckPathPlacementValidity() {
        Collider[] hitColliders = Physics.OverlapSphere(mousePos, pathRadius, LayerMask.GetMask("path"));
        Collider[] wallColliders = Physics.OverlapSphere(mousePos, pathRadius, LayerMask.GetMask("wall"));
        Collider[] envColliders = Physics.OverlapSphere(mousePos, pathRadius, LayerMask.GetMask("environment"));

        return hitColliders.Length == 0 && wallColliders.Length == 0 && envColliders.Length == 0;
    }

    // hide preview and delete the tower
    public void StopPlacement() {
        selectedObjectIndex = -1;
        preview.StopShowingPreview();
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
        lastDetectedPos = Vector3Int.zero;
    }

    void Update()
    {
        // based on where the mouse position is, convert grid pos to world pos
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
