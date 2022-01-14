using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
	
	CharacterController controller;

	public AudioSource playerAttackSound;
	public AudioSource energySound;
	public float playerMaxHealth;
	public float playerCurrentHealth;

	public float playerMaxEnergy;
	public float playerCurrentEnergy;

	public int enemiesKilled = 0;
	public float energyTick = 3;
	public float timeXTick = 1;
	public int totalTicks = 10;
	public bool decreasingEnergy = false;
	public bool isFullPower = false;
	public float timer;
	public float waitTime = 1f;
	[SerializeField] private Transform respawnPoint;

	public GameObject canvas;
	private UIManager uiScript;

	// Start is called before the first frame update
	void Start()
    {
		
		playerCurrentHealth = playerMaxHealth;
		playerCurrentEnergy = 0;
		uiScript = GameObject.Find("Canvas").GetComponent<UIManager>();
	}

    // Update is called once per frame
    void Update()
    {
		
		if(playerCurrentEnergy == playerMaxEnergy) //If the player's energy is at max, gradually reduce the energy and make the player stronger.
		{
			timer += Time.deltaTime;

			if (timer >= waitTime)
			{
				timer = 0;
				decreasingEnergy = true;
				isFullPower = true;
				StartCoroutine("ReduceEnergy");
			}
		}

		if(playerCurrentHealth < 1) //If the player's health drops below 1 send them to the game over screen
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			SceneManager.LoadScene("GameOver");
		}

		if(UIManager.chestCount == 3 && enemiesKilled == 3) //If the player collects all the chests and kills all the enemies send them to the win screen.
		{
			SceneManager.LoadScene("WinScreen");
		}

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Energy") //If the player collides with this object give them energy and temporarily destroy the object.
		{
			EnergizePlayer();
			energySound.Play();
			other.gameObject.SetActive(false);
			StartCoroutine(Respawn(other, 4));
		}

		if (other.tag == "Enemy" && isFullPower) //If the player collides with the enemy while at full power, kill the enemy.
		{
			playerAttackSound.Play();
			Destroy(other.gameObject);
			enemiesKilled++; 
		}
	}

	public void DamagePlayer(int damageToGive)
	{
		if (isFullPower)
		{
			damageToGive = 0;
		}
		else
		{
			playerCurrentHealth -= damageToGive;
			controller = GetComponent<CharacterController>();
			controller.enabled = false;
			transform.position = respawnPoint.position;
			controller.enabled = true;
		}
	}

	public void EnergizePlayer()
	{
		playerCurrentEnergy += 10;
	}

	public void SetMaxHealth()
	{
		playerCurrentHealth = playerMaxHealth;
	}

	IEnumerator Respawn(Collider collision, int time)
	{
		yield return new WaitForSeconds(time);
		collision.gameObject.SetActive(true);
	}

	IEnumerator ReduceEnergy()
	{
		int ticks = 0;
		int totalTicksTemp = totalTicks;

		if (decreasingEnergy == true)
		{
			decreasingEnergy = false;

			while (ticks < totalTicksTemp)
			{
				ticks++;
				playerCurrentEnergy -= energyTick;
				yield return new WaitForSecondsRealtime(timeXTick);
			}
			isFullPower = false; 

		}
	}
}
