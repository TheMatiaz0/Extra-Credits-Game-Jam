using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
	[SerializeField]
	private float attackDistance = 3f;

	[SerializeField]
	private float movementSpeed = 4f;

	[SerializeField]
	private float attackRate = 0.5f;

	[SerializeField]
	private Transform firePoint;

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
	}

	protected void Update()
	{
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

						healthSys.Health.TakeValue(10, "Żombi");
					}
				}
			}
		}
	}
}
