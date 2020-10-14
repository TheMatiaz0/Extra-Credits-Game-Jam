using Cyberultimate.Unity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum SpawnPlace
{
	Soil,
	Water
}

public class EnemySpawner : MonoSingleton<EnemySpawner>
{
	[SerializeField]
	private Transform waterWaypointParent = null;

	[SerializeField]
	private Transform soilWaypointParent = null;

	[SerializeField]
	private Transform waterContainer = null;

	[SerializeField]
	private Transform soilContainer = null;

	[SerializeField]
	private EnemyBehaviour enemyPrefab = null;

	public bool EnemyCanBite { get; set; } = true;


	public void GreatPlaceSpawn ()
	{
		if (soilContainer.childCount == waterContainer.childCount)
		{
			int rnd = Random.Range(0, 2);
			SpawnEnemy((SpawnPlace)rnd);
		}

		else if (soilContainer.childCount > waterContainer.childCount)
		{
			SpawnEnemy(SpawnPlace.Water);
		}

		else if (waterContainer.childCount > soilContainer.childCount)
		{
			SpawnEnemy(SpawnPlace.Soil);
		}
	}

	private void SpawnEnemy (SpawnPlace where)
	{
		EnemyBehaviour enemy = null;

		switch (where)
		{
			case SpawnPlace.Soil:
				enemy = Instantiate(enemyPrefab, soilWaypointParent.GetChild(Random.Range(0, soilWaypointParent.childCount)).position, Quaternion.identity, soilContainer);
				enemy.Waypoints = soilWaypointParent.GetChildren().ToArray();
				break;


			case SpawnPlace.Water:
				enemy = Instantiate(enemyPrefab, waterWaypointParent.GetChild(Random.Range(0, waterWaypointParent.childCount)).position, Quaternion.identity, waterContainer);
				enemy.Waypoints = waterWaypointParent.GetChildren().ToArray();
				break;
		}

	}
}
