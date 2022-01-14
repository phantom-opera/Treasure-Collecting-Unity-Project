using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

	//Loads the game, main menu, control menu, or exits the game based on which button is pressed.

	public void newGame()
	{
		Cursor.visible = false;
		SceneManager.LoadScene("MainGame");
	}

	public void mainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void controlMenu()
	{
		SceneManager.LoadScene("Controls");
	}

	public void exitGame()
	{
		Application.Quit();
	}
}
