using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBarkController : MonoBehaviour
{
	// Controls the dog's barking

	[Header("Timers")]
	// Timer on the dog's current bark
	[SerializeField]
	private float barkTimer;

	// The time between barks
	public float barkInterval;

	[Header("Bark things")]
	public GameObject barkPrefab;

	// The offset to place barks at
	public Vector3 barkOffset;

	// The bark sounds
	public AudioSource[] barkSounds;

	/// <summary>
	/// Makes the dog bark
	/// </summary>
	public void TriggerBark()
	{
		// Make sure we can bark
		if (barkTimer <= 0)
		{
			// Create a new 'woof' speech bubble and place it at the dog's position
			GameObject newBark = Instantiate(barkPrefab);

			// Position the bark
			newBark.transform.position = transform.position + barkOffset;

			// Set bark timer
			barkTimer = barkInterval;
			newBark.GetComponent<BarkController>().despawnTimer = 0.8f;

			// Pick a bark sound and play it
			int barkID = Random.Range(0, barkSounds.Length);
			barkSounds[barkID].Play();
		}
	}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Decrement bark timer
		barkTimer -= Time.deltaTime;
	}
}
