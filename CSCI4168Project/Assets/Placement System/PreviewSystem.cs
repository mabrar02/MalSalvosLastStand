using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewSystem : MonoBehaviour
{
    [SerializeField] private float previewYOffset = 0.06f;
    [SerializeField] private GameObject cellIndicator;
    private GameObject previewObject;

    [SerializeField] private Material previewMaterialsPrefab;
    private Material previewMaterialInstance;

    private Renderer cellIndicatorRenderer;

    private void Start() {
        previewMaterialInstance = new Material(previewMaterialsPrefab);
        cellIndicator.SetActive(false);
        cellIndicatorRenderer = cellIndicator.GetComponentInChildren<Renderer>();
    }

    public void StartShowingPlacementPreview(GameObject prefab) {
        previewObject = Instantiate(prefab);
        PreparePreview(previewObject);
        PrepareCursor();
        cellIndicator.SetActive(true);
    }

    private void PrepareCursor() {
        cellIndicator.transform.localScale = new Vector3(1, 1, 1);

    }

    private void PreparePreview(GameObject previewObject) {
        Renderer[] renderers = previewObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers) {
            Material[] materials = renderer.materials;
            for(int i = 0; i<materials.Length; i++) {
                materials[i] = previewMaterialInstance;
            }
            renderer.materials = materials;
        }

        Collider[] colliders = previewObject.GetComponentsInChildren<Collider>();
        for(int i = 0; i<colliders.Length;i++) {
            colliders[i].enabled = false;
        }
    }

    public void StopShowingPreview() {
        cellIndicator.SetActive(false);
        Destroy(previewObject);
    }

    public void UpdatePosition(Vector3 pos, bool validity) {
        MovePreview(pos);
        MoveCursor(pos);
        ApplyFeedback(validity);
    }

    private void ApplyFeedback(bool validity) {
        Color indiciatorCol = validity ? Color.green : Color.red;
        Color previewCol = validity ? Color.white : Color.red;
        indiciatorCol.a = 0.25f;
        previewCol.a = 0.5f;

        cellIndicatorRenderer.material.color = indiciatorCol;
        previewMaterialInstance.color = previewCol;
    }

    private void MovePreview(Vector3 pos) {
        cellIndicator.transform.position = pos;
    }

    private void MoveCursor(Vector3 pos) {
        previewObject.transform.position = new Vector3(pos.x, pos.y + previewYOffset, pos.z);
    }
}
