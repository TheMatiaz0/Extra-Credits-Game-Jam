using Cyberultimate;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
	[SerializeField]
	private float attackDistance = 3f;

	[SerializeField]
	private float viewDistance = 10;

	[SerializeField]
	private float movementSpeed = 4f;

	[SerializeField]
	private float attackRate = 0.5f;

    [SerializeField]
    private float attackDamage = 10;

    [SerializeField]
    private float poisonTime = 10;

	[SerializeField]
	private Transform firePoint;

	private NavMeshAgent agent;
	private Rigidbody rb;

	private float nextAttackTime = 0;

	private Vector3 startPosition;

	[SerializeField]
	private Transform[] waypoints = null;

	[SerializeField]
	private Animator animator = null;

	private static bool canBite = true;

	protected void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		agent.stoppingDistance = attackDistance;
		agent.speed = movementSpeed;
		rb = GetComponent<Rigidbody>();
		rb.useGravity = false;
		rb.isKinematic = true;

		startPosition = this.transform.position;
	}

	private IEnumerator AttackAnimation ()
	{
		canBite = false;
		animator.SetTrigger("Bite");
		AudioManager.Instance.PlaySFX("bite");
		MovementController.Instance.BlockMovement = true;
		GameManager.Instance.HealthSys.Health.TakeValue(attackDamage, "Infected");
		yield return Async.Wait(TimeSpan.FromSeconds(2.1f));
		MovementController.Instance.BlockMovement = false;
		yield return Async.Wait(TimeSpan.FromSeconds(3.2f));
		canBite = true;
	}

	protected void Update()
	{

		Vector3 playerPos = MovementController.Instance.transform.position;

		if (Vector3.Distance(this.transform.position, playerPos) <= viewDistance)
		{
			if (agent.remainingDistance - attackDistance < 0.01f)
			{
				if (Time.time > nextAttackTime)
				{
					nextAttackTime = Time.time + attackRate;

					if (Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit hit, attackDistance))
					{
						HealthSystem healthSys = null;
						if (healthSys = (hit.transform.GetComponent<HealthSystem>()))
						{
							if (canBite)
							{
								StopAllCoroutines();
								StartCoroutine(AttackAnimation());
							}
						}
					}
				}
			}

			ChangeFocus(playerPos);
		}

		else if (Vector3.Distance(this.transform.position, startPosition) > 3)
		{
			if (agent.remainingDistance - attackDistance < 0.01f)
			{
				ChangeFocus(startPosition);
			}
		}

		else
		{
			if (agent.remainingDistance - attackDistance < 0.01f)
			{
				ChangeFocus(waypoints[UnityEngine.Random.Range(0, waypoints.Length)].position);
			}
		}
	}

	private void ChangeFocus (Vector3 pos)
	{
		agent.destination = pos;
		transform.LookAt(new Vector3(pos.x, this.transform.position.y, pos.z));
		rb.velocity *= 0.99f;
	}
}