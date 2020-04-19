using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepManager : MonoBehaviour
{
	// This keeps track of all the sheep and provides some utilities for sheep

	// The sheep alive
	private List<SheepController> sheepList;

	// Lost sheep and the time they have before they are completely lost
	private List<SheepController> lostSheep;
	private List<float> lostSheepTimers;

	[Header("Sheep things")]
	// The sheep prefab
	public GameObject sheepPrefab;

	// The markers available for sheep to select
	public SheepMischiefMarker[] markers;

	// Number of sheep to spawn
	public int sheepStartCount;

	// The area that sheep can spawn in
	public BoxCollider sheepSpawnArea;

	// How much time the player has to rescue a sheep
	public float sheepRescueTime;

	// How many sheep remaining leads to a game over
	public int minimumSheep;

	/// <summary>
	/// Gets the current number of sheep
	/// </summary>
	/// <returns>The current number of sheep</returns>
	public int GetCurrentSheepCount()
	{
		return sheepList.Count;
	}

	/// <summary>
	/// Generates a random position within a bounding box
	/// </summary>
	/// <param name="centre">The centre of the box</param>
	/// <param name="dimensions">The dimensions of the box</param>
	/// <returns>A random position within the box</returns>
	private Vector3 RandomPosition(Vector3 centre, Vector3 dimensions)
	{
		// Generate first bound
		Vector3 bound1 = centre - dimensions / 2f;
		Vector3 bound2 = centre + dimensions / 2f;

		// Generate co-ordinates 1 by 1
		float x = Random.Range(bound1.x, bound2.x);
		float y = Random.Range(bound1.y, bound2.y);
		float z = Random.Range(bound1.z, bound2.z);

		// Return the new position
		return new Vector3(x, y, z);
	}

	/// <summary>
	/// Called when a sheep gets out of the enclosure
	/// </summary>
	/// <param name="sheep">The sheep that got out</param>
	public void SheepLost(SheepController sheep)
	{
		// Remove from sheep list
		sheepList.Remove(sheep);

		// Add the sheep to the lost sheep list
		lostSheep.Add(sheep);

		// Add a new timer
		lostSheepTimers.Add(sheepRescueTime);
	}

	/// <summary>
	/// Removes a lost sheep from the world
	/// </summary>
	/// <param name="sheep">The sheep to remove</param>
	public void RemoveSheep(SheepController sheep)
	{
		// Fetch index of the sheep
		int id = lostSheep.IndexOf(sheep);

		// If we have a valid ID, continue
		if (id >= 0)
		{
			// Remove sheep from lists
			lostSheep.Remove(sheep);
			lostSheepTimers.RemoveAt(id);

			// Notify
			Debug.Log("Sheep is lost");

			// Destroy sheep object
			Destroy(sheep.gameObject);
		}
	}

	// Use this for initialization
	void Start ()
	{
		// Set up sheep list
		sheepList = new List<SheepController>();

		// Set up lost sheep lists
		lostSheep = new List<SheepController>();
		lostSheepTimers = new List<float>();

		// Create each sheep
		for (int i = 0; i < sheepStartCount; i++)
		{
			// Create the sheep
			GameObject newSheep = Instantiate(sheepPrefab);

			// Position the sheep somewhere in the arena
			Vector3 position = RandomPosition(sheepSpawnArea.transform.position + sheepSpawnArea.center, sheepSpawnArea.size);
			newSheep.transform.position = position;

			// Parent the sheep to this
			newSheep.transform.SetParent(transform);

			// Fetch sheep controller and add it to the list
			SheepController controller = newSheep.GetComponent<SheepController>();
			sheepList.Add(controller);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Decrement lost sheep timers
		for (int i = 0; i < lostSheepTimers.Count; i++)
		{
			// Decrement timer
			lostSheepTimers[i] -= Time.deltaTime;

			// If the timer has reached zero, get rid of the sheep
			if (lostSheepTimers[i] < 0)
				RemoveSheep(lostSheep[i]);
		}

		// If we reach minimum sheep, trigger game over
		if (sheepList.Count <= minimumSheep)
		{
			FindObjectOfType<GameController>().TriggerGameOver();
		}
	}
}
