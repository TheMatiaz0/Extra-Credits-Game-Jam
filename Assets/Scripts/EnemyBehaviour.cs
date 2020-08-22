using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Moving")]
	[SerializeField]
	private float movementSpeed = 4f;
    [SerializeField]
    private Vector2 nextMoveRandomDelay=new Vector2(4,8);
    
    [Header("Ranges")]
    [SerializeField]
    private float chasingRange = 3f;
    [SerializeField]
    private float maxMovingRange = 20;
    [SerializeField]
    private float movingRangeMaxMinYX = 5;

    [Header("Attacking")]
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private float attackDistance = 3f;
    [SerializeField]
	private float attackRate = 0.5f;

    private Vector2 startingPos;

	private NavMeshAgent agent;
	private Rigidbody rb;

	private float nextAttackTime = 0;

	protected void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		agent.stoppingDistance = attackDistance;
		agent.speed = movementSpeed;
		rb = GetComponent<Rigidbody>();
		rb.useGravity = false;
		rb.isKinematic = true;

        startingPos = transform.position;
	}

    private bool chasingPlayer = false;

	protected void Update()
	{
        HandleOutside();
        HandleChase();
        HandleSoloMoving();

		rb.velocity *= 0.99f;
	}

    void HandleChase()
    {
        Vector3 playerPos = MovementController.Instance.transform.position;
        if (Vector3.Distance(transform.position, playerPos) <= chasingRange) chasingPlayer = true; else chasingPlayer = false;

        if (chasingPlayer)
        {
            Debug.Log("I see player!");
            if (agent.remainingDistance - attackDistance < 0.01f)
            {
                if (Time.time > nextAttackTime)
                {
                    nextAttackTime = Time.time + attackRate;

                    RaycastHit hit;
                    if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, attackDistance))
                    {
                        HealthSystem healthSys = null;
                        if (healthSys = (hit.transform.GetComponent<HealthSystem>()))
                        {
                            Debug.DrawLine(firePoint.position, firePoint.position + firePoint.forward * attackDistance, Color.cyan);

                            healthSys.Health.TakeValue(15, "Żombi");
                        }
                    }
                }
            }
   
            agent.destination = playerPos;

            transform.LookAt(new Vector3(playerPos.x, this.transform.position.y, playerPos.z));
        } 
    }

    void HandleOutside()
    {
        if (Vector2.Distance(startingPos, transform.position) >= maxMovingRange)
        {
            //nowa pozycja w kwadracie dookoła środka
            agent.Move(NewMovingPosition(startingPos));
            Debug.Log("Too far away ;(");
        }
    }

    float nextMoveTimer = 0;
    float nextMoveTime = 4;
    void HandleSoloMoving()
    {
        if (agent.remainingDistance<0.01f || agent.remainingDistance==Mathf.Infinity) //stoi w miejscu
        {
            nextMoveTimer += Time.deltaTime;
            Debug.Log("Waiting for next move");
            if (nextMoveTimer>=nextMoveTime)
            {
                Debug.Log("Next move!");
                nextMoveTimer = 0;
                nextMoveTime = Random.Range(nextMoveRandomDelay.x, nextMoveRandomDelay.y);
                agent.Move(NewMovingPosition(transform.position));
            }
        }
    }

    Vector2 NewMovingPosition(Vector2 pos)
    {
        return pos + new Vector2(Random.Range(-movingRangeMaxMinYX, movingRangeMaxMinYX), Random.Range(-movingRangeMaxMinYX, movingRangeMaxMinYX));
    }
}
