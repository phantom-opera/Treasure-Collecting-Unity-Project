using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

	public Slider healthBar;
	public Slider energyBar;
	public PlayerManager playerHealth;
	public PlayerManager playerEnergy;
	public GameObject chestText;
	public static int chestCount;

	public GameObject playerChar;
	private PlayerManager playerScript;

	void Start()
    {
		playerScript = playerChar.GetComponent<PlayerManager>();
		chestCount = 0;
	}

    // Update is called once per frame
    void Update()
    {
		healthBar.maxValue = playerHealth.playerMaxHealth;
		healthBar.value = playerHealth.playerCurrentHealth;

		energyBar.maxValue = playerHealth.playerMaxEnergy;
		energyBar.value = playerHealth.playerCurrentEnergy;

		chestText.GetComponent<Text>().text = "Treasures: " + chestCount;

		if(chestCount == 3)
		{
			playerScript.isFullPower = true;
		}
		
	}

}
