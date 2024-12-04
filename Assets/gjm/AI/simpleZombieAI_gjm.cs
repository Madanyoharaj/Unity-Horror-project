// *  Simple Zombie AI (line of sight navigation only)
// *  Minkworks Simulation Technologies Ltd.
// *  November, 2021
// *  Contact: minkworksSimulation@gmail.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleZombieAI_gjm : MonoBehaviour
{

    public Transform player;
    public Rigidbody attractor;

    public LayerMask Ground;

    public LayerMask enemy;

    bool isGrounded = true;
    public Transform attractorGroundCheck_gjm;

    public float attractorSpeed = 5;

    Vector3 playerPosition;
    Vector3 attractorPosition;

    Vector3 x;
    Vector3 xNorm;

    public float tht = 0f;

    float range = 1000;

    public bool enemyToPlayerRayStatus = false;

    Vector3 targetPosition;

    public bool rayHitEnemy;

    public bool isTouchingWall;

    public LayerMask wallLayer_gjm;
    

    public Transform wallCheckTransform;
    public float wallTouchBuffer_gjm = 0.2f;

    public Transform dummaryVar2;

    public float noiseParameter;

    public bool isTouchingStair = false;
    public bool isTouchingEnemy = false;

    public Vector3 computedVelocity;


    public LayerMask includeLayersInNavRaycast;


    void getEnemyToPlayerPathStatus()
    {
        RaycastHit rayHit = new RaycastHit();
        Ray ray = new Ray(transform.position, player.position - transform.position);
        if (Physics.Raycast(ray, out rayHit, 1000, includeLayersInNavRaycast))
        {

            enemyToPlayerRayStatus = rayHit.transform == player;

            rayHitEnemy = rayHit.transform.gameObject.layer == LayerMask.NameToLayer("enemy");

        }
    }


    void followTarget()
    {
        // * Get the player and attractor positions
        targetPosition = player.position;
        attractorPosition = attractor.position;

        // * Compute the vector difference x = a-b
        x = targetPosition - attractorPosition;

        // * Force AI to grounded path to player!!!
        x.y = 0f;

        // * Compute the normalized vector x
        xNorm = x / x.magnitude;
        computedVelocity = xNorm * attractorSpeed;
        computedVelocity.y = attractor.linearVelocity.y;

        // * Set the attractor velocity
        attractor.linearVelocity = computedVelocity;

        // * Check to see if stuck on wall
        wallCheck_gjm();

    }


    void wallCheck_gjm()
        {
            isTouchingWall  = Physics.CheckSphere(wallCheckTransform.position, wallTouchBuffer_gjm, wallLayer_gjm);
            isTouchingStair = Physics.CheckSphere(wallCheckTransform.position, wallTouchBuffer_gjm, Ground);
            

            if(isTouchingWall || rayHitEnemy)
            {
                attractor.linearVelocity = attractor.linearVelocity + new Vector3 (Random.Range(0.0f,noiseParameter*10f),0f,Random.Range(0.0f,noiseParameter*10f));
            }
            
            if(isTouchingStair)
            {
                attractor.linearVelocity = attractor.linearVelocity + new Vector3 (0f,Random.Range(noiseParameter*1.0f,noiseParameter*5f),0f);
            }

        }

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        // * Stop zombies from moving? (for debugging)
        if (Input.GetKeyDown("y"))
        {
            if (attractorSpeed == 5f)
            {
                attractorSpeed = 0f;
            }
            else
            {
                attractorSpeed = 5f;
            }
        }


        transform.LookAt(player);
        transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
        followTarget();
    }
}
