using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
	public AudioSource attackSound;
	public int damageToGive;
	public GameObject enemy;
	private EnemyAINav enemyScript;

	// Start is called before the first frame update
	void Start()
	{
		enemyScript = enemy.GetComponent<EnemyAINav>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") // If the collision is with the player, deal damage to them and play a sound effect.
		{
			StartCoroutine("Wait");
			other.gameObject.GetComponent<PlayerManager>().DamagePlayer(damageToGive);
			attackSound.Play();
			enemyScript.playerDetected = false;
		}
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(1);
	}
}
