using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SheepUIManager : MonoBehaviour
{
	// Controls the UI showing sheep count and timer

	[Header("References")]
	// The game controller and sheep manager
	public GameController gameManager;
	public SheepManager sheepManager;

	[Header("UI")]
	public Text timer;
	public Text sheepCounter;

	// Game over screen
	public GameObject gameOverScreen;

	// Game over progress text
	public string sheepScoreText;
	public Text gameOverScoreText;

	/// <summary>
	/// Shows the game over screen
	/// </summary>
	public void ShowGameOver()
	{
		gameOverScreen.SetActive(true);

		// Set the score text
		// Use System.TimeSpan to format the seconds into mm:ss
		TimeSpan time = TimeSpan.FromSeconds(gameManager.GetTimer());
		gameOverScoreText.text = sheepScoreText + string.Format("{0:D2}:{1:D2}", time.Minutes, time.Seconds);
	}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Update the timer
		// Use System.TimeSpan to format the seconds into mm:ss
		TimeSpan time = TimeSpan.FromSeconds(gameManager.GetTimer());
		timer.text = string.Format("{0:D2}:{1:D2}", time.Minutes, time.Seconds);

		// Update sheep count
		sheepCounter.text = sheepManager.GetCurrentSheepCount() + "/" + sheepManager.sheepStartCount;
	}
}
