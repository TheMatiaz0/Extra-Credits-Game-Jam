using Cyberultimate;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
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
	private float staminaDamage = 15;

	[SerializeField]
	private Transform firePoint;

	private NavMeshAgent agent;
	private Rigidbody rb;

	private float nextAttackTime = 0;

	private Vector3 startPosition;

	[SerializeField]
	private Transform[] waypoints = null;

	public Transform[] Waypoints { get; set; }

	[SerializeField]
	private Animator animator = null;

	private static bool canBite = true;

	protected void Awake()
	{
		if (waypoints != null || waypoints.Length > 0)
		{
			Waypoints = waypoints;
		}

		// MovementSpeed = movementSpeed;
	}

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

	private IEnumerator AttackAnimation (HealthSystem healthSys, StaminaSystem staminaSys = null)
	{
		canBite = false;
		animator.SetTrigger("Bite");
		AudioManager.Instance.PlaySFX("bite");
		MovementController.Instance.BlockMovement = true;
		InteractionChecker.Instance.CheckInteractions = false;
		InteractionUI.Instance.HidePossibleInteraction();
		healthSys.Health.TakeValue(attackDamage, "Infected");
		staminaSys.Stamina.TakeValue(staminaDamage, "Infected");
		yield return Async.Wait(TimeSpan.FromSeconds(2.1f));
		MovementController.Instance.BlockMovement = false;
		InteractionChecker.Instance.CheckInteractions = true;
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
								StartCoroutine(AttackAnimation(healthSys, healthSys.GetComponent<StaminaSystem>()));
							}
						}
					}
				}
			}

			ChangeFocus(playerPos);
			agent.speed = movementSpeed * 2.35f;
		}

		else if (Vector3.Distance(this.transform.position, startPosition) > 3)
		{
			if (agent.remainingDistance - attackDistance < 0.01f)
			{
				ChangeFocus(startPosition);
				agent.speed = movementSpeed;
			}
		}

		else
		{
			if (agent.remainingDistance - attackDistance < 0.01f)
			{
				ChangeFocus(Waypoints[UnityEngine.Random.Range(0, Waypoints.Length)].position); 
				agent.speed = movementSpeed;
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