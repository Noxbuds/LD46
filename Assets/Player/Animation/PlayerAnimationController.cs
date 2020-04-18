using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
	// Target run speed
	public float targetRunSpeed;

	// The animator
	private Animator animator;

	// Whether the dog is running
	private bool isRunning;

	// Reference to the rigidbody
	public new Rigidbody rigidbody;

	/// <summary>
	///	Sets the running trigger 
	/// </summary>
	public void StartRunning()
	{
		animator.ResetTrigger("Stop");
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
		animator.ResetTrigger("Run");
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
		// Set run speed
		animator.SetFloat("Run Speed", rigidbody.velocity.magnitude / targetRunSpeed);
	}
}
