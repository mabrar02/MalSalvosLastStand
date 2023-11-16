using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float destroyTime = 3f;
    public Vector3 offset = new Vector3(0, 0.5f, 0);
    public Vector3 randomizeIntensity = new Vector3(0.5f, 0, 0);
    private Camera fpsCam;
    void Start()
    {
        fpsCam = Camera.main;
        Destroy(gameObject, destroyTime);
        transform.localPosition += offset;
        transform.localPosition += new Vector3(Random.Range(-randomizeIntensity.x, randomizeIntensity.x),
           Random.Range(-randomizeIntensity.y, randomizeIntensity.y), Random.Range(-randomizeIntensity.z, randomizeIntensity.z));
    }

    private void LateUpdate() {
        transform.LookAt(fpsCam.transform);
        transform.rotation = Quaternion.LookRotation(fpsCam.transform.forward);
    }

}
