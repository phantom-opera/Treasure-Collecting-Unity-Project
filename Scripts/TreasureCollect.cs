using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureCollect : MonoBehaviour
{
	public GameObject self;
	public AudioSource treasureSound;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			UIManager.chestCount++;
			treasureSound.Play();
			self.SetActive(false);
		}
	}
}
