using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepManager : MonoBehaviour
{
	// This keeps track of all the sheep and provides some utilities for sheep

	// The sheep alive
	private List<SheepController> sheep;
	
	[Header("Sheep things")]
	// The sheep prefab
	public GameObject sheepPrefab;

	// The markers available for sheep to select
	public SheepMischiefMarker[] markers;

	// Number of sheep to spawn
	public int sheepStartCount;

	// The area that sheep can spawn in
	public BoxCollider sheepSpawnArea;

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

	// Use this for initialization
	void Start ()
	{
		// Set up sheep list
		sheep = new List<SheepController>();

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
			sheep.Add(controller);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
