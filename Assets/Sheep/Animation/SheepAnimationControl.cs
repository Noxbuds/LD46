using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SheepAnimationControl : MonoBehaviour
{
	// This script controls the sheep's animations

	// The sheeps' animator
	private Animator animator;

	// Whether the sheep is currently running
	[SerializeField]
	private bool running;

	[Header("Timers")]
	// The timer for eating grass
	[SerializeField]
	private float eatTimer;

	// The timer for pooping
	[SerializeField]
	private float poopTimer;

	// Time between eating and pooping respectively
	public float eatIntervalTime;
	public float poopIntervalTime;

	/// <summary>
	/// Resets triggers related to running
	/// </summary>
	private void ResetRunningTriggers()
	{
		animator.ResetTrigger("Start Running");
		animator.ResetTrigger("Stop Running");
	}

	/// <summary>
	/// Resets triggers
	/// </summary>
	private void ResetTriggers()
	{
		ResetRunningTriggers();
		animator.ResetTrigger("Eat");
		animator.ResetTrigger("Poop");
	}

	/// <summary>
	/// Returns whether the running animation is playing
	/// </summary>
	/// <returns>Whether the running animation is playing</returns>
	public bool isRunning()
	{
		return running;
	}

	/// <summary>
	/// Makes the sheep start running
	/// </summary>
	public void StartRunning()
	{
		// Only set triggers if the sheep is not running
		if (!running)
		{
			// Start the running animation
			animator.SetTrigger("Start Running");

			// Set running flag
			running = true;
		}
	}

	/// <summary>
	/// Makes the sheep stop running
	/// </summary>
	public void StopRunning()
	{
		// Only set triggers if the sheep is already running
		if (running)
		{
			// Stop the running animation
			animator.SetTrigger("Stop Running");

			// Set running flag
			running = false;
		}
	}

	// Use this for initialization
	void Start ()
	{
		// Fetch animator
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		// If not running, loop through idle animations
		if (!isRunning())
		{
			// Reset running triggers
			//ResetRunningTriggers();

			// Decrement timers
			eatTimer -= Time.deltaTime;
			poopTimer -= Time.deltaTime;

			// Eat grass
			if (eatTimer < 0)
			{
				// Set the trigger
				animator.SetTrigger("Eat");

				// Reset timer
				eatTimer = eatIntervalTime;
			}

			// Make a poop
			if (poopTimer < 0)
			{
				// Set the trigger
				animator.SetTrigger("Poop");

				// Reset timer
				poopTimer = poopIntervalTime;
			}
		}
	}
}
