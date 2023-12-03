using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{
    public Transform goal;

    public void setGoal(Transform goal){
        this.goal = goal;
    }

    void Start()
    {
        // set navmesh destination to homebase
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
    }

}
