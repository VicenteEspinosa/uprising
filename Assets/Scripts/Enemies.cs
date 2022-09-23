using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemies : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    //patroling
    public Vector2 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Atacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Firefighter").transform;
        agent = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        //Check for sight and attack range
        // playerInSightRange = Physics2D.CircleCast(transform.position, sightRange, player.position);
        // playerInAttackRange = Physics2D.CircleCast(transform.position, sightRange, player.position);
        // if(!playerInSightRange && !playerInAttackRange) Patroling();
        // if(playerInSightRange && !playerInAttackRange) ChasePlayer();
        // if(playerInSightRange && playerInAttackRange) AttackPlayer();
        Patroling();

    }

    private void Patroling()
    {
        if(!walkPointSet)SearchWalkPoint();

        if(walkPointSet)
            agent.SetDestination(walkPoint);
        
        Vector2 distanceToWalkPoint = new Vector2(transform.position.x,transform.position.y) - walkPoint;

        //WalkPoint reached
        if(distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        // Calculate random point in range
        float randomX = Random.Range(-walkPointRange,walkPointRange);
        float randomY = Random.Range(-walkPointRange,walkPointRange);

        walkPoint = new Vector2(transform.position.x + randomX, transform.position.y + randomY);
        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }
    
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    
    private void AttackPlayer()
    {
        // Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if(!alreadyAttacked)
        {
            // attack code here
            // Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            // rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            // rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            //
            alreadyAttacked = true; 
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDammage(int damage)
    {
        health -= damage;

        if(health <= 0) Invoke(nameof(DestroyEnemy), .5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

}
