using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEnemyMovement : MonoBehaviour
{
    public int speed = 15;
    public Rigidbody thisRigidBody;

    void Start()
    {
    }

    void Update()
    {
        thisRigidBody.AddForce(transform.forward * 1);
    }
}
