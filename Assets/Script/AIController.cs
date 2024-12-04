using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    private GameObject destination;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        destination = GameObject.FindGameObjectWithTag("Player");

        agent = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        agent.SetDestination(destination.transform.position);
    }
}
