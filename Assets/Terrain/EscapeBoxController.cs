using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeBoxController : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	/// <summary>
	/// Called when a collider enters this trigger
	/// </summary>
	/// <param name="other">The collider that entered the trigger</param>
	private void OnTriggerEnter(Collider other)
	{
		// Check if it is a sheep
		// The sheep collider is 2 levels under the sheep, so we must get its parent's parent
		if (other.transform.parent.parent == null) return; // do this to prevent errors in console with the dog
		SheepController sheep = other.transform.parent.parent.GetComponent<SheepController>();

		// If not null, it is a sheep
		if (sheep != null)
		{
			// Get the sheep manager and report a lost sheep
			FindObjectOfType<SheepManager>().SheepLost(sheep);
		}
	}
}
