using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementCamera : MonoBehaviour
{
    private Vector3 initialPos;
    [SerializeField] private float camSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;

    private Vector3 targetPosition;

    private void Awake()
    {
        initialPos = transform.position;
        
    }

    private void OnEnable() {
        transform.position = initialPos;
    }

    private void Update() {
        Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        targetPosition += camSpeed * Time.deltaTime * moveDir;

        targetPosition = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * acceleration);

        transform.position = targetPosition;

        if(moveDir == Vector3.zero) {
            targetPosition = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * deceleration);
            transform.position = targetPosition;
        }
    }
}
