using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
	private AudioSource music;
	private void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);
		music = GetComponent<AudioSource>();
	}

	public void PlayMusic()
	{
		if (music.isPlaying) return;
		music.Play();
	}
}
