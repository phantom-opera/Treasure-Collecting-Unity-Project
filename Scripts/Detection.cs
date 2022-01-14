using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
	public GameObject enemy;

	private EnemyAINav enemyScript;
	// Start is called before the first frame update

    //If the player is in the detection radius, attract the enemy's attention. If the player exits the radius stop alerting the enemy.

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			enemyScript = enemy.GetComponent<EnemyAINav>();
			enemyScript.playerDetected = true;
		}
	}

	void OnTriggerExit(Collider other)
	{

		if (other.tag == "Player")
		{
			enemyScript = enemy.GetComponent<EnemyAINav>();
			enemyScript.playerDetected = false;
		}
	}
}
