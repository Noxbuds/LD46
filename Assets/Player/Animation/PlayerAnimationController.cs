using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
	// The animator
	private Animator animator;

	// Whether the dog is running
	private bool isRunning;

	/// <summary>
	///	Sets the running trigger 
	/// </summary>
	public void StartRunning()
	{
		if (!isRunning)
		{
			// Set the trigger
			animator.SetTrigger("Run");

			// Set run flag
			isRunning = true;
		}
	}

	/// <summary>
	/// Stops the dog from running
	/// </summary>
	public void StopRunning()
	{
		if (isRunning)
		{
			// Set the trigger
			animator.SetTrigger("Stop");

			// Set run flag
			isRunning = false;
		}
	}

	// Use this for initialization
	void Start ()
	{
		// Fetch the animator
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
