using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownTrigger : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			TaskManager.Instance.RemoveTask("Explore the city");
		}
	}
}
