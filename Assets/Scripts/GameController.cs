using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	// Controls main game things like game over and score

	// Time the player has kept the sheep for
	private float timer;

	// Whether the game is over
	private bool gameOver;

	/// <summary>
	/// Gets the amount of time the player has kept the sheep in
	/// </summary>
	/// <returns>The amount of time the game has been going on for</returns>
	public float GetTimer()
	{
		return timer;
	}

	/// <summary>
	/// Triggers the 'game over' state
	/// </summary>
	public void TriggerGameOver()
	{
		// Set game over flag
		gameOver = true;

		// Show game over screen
		FindObjectOfType<SheepUIManager>().ShowGameOver();
	}

	/// <summary>
	/// Starts the game over again
	/// </summary>
	public void PlayAgain()
	{
		// Load the scene again
		SceneManager.LoadScene(0);
	}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Increment timer
		if (!gameOver)
			timer += Time.deltaTime;
	}
}
