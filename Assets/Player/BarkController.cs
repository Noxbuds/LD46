using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarkController : MonoBehaviour
{
	// Deletes a bark bubble after some time
	public float despawnTimer;

	// Use this for initialization
	void Start ()
	{
		// will be set, but we need it to not be instantly destroyed
		despawnTimer = 1;
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Decrement timer
		despawnTimer -= Time.deltaTime;

		// Delete this after the right time
		if (despawnTimer < 0)
			Destroy(gameObject);
	}
}
