using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SheepController : MonoBehaviour
{
	// Each sheep will have 1 target in mind, which they will run to until interrupted
	[SerializeField]
	private SheepMischiefMarker target;

	// When the sheep is interrupted by the dog (player), it will have a 'scared' timer
	// During this time it will focus on just running away from the dog, and won't pick
	// a new target until the timer is over
	private float scaredTimer;

	// The sheep's rigidbody. Hide Component.rigidbody, as we want to have more control
	private new Rigidbody rigidbody;

	// The player
	private PlayerController player;

	[Header("Sheep properties")]
	// Max run force
	public float runForce;

	// The sheep's body
	public GameObject body;

	// The radius in which the sheep will be scared of the dog
	public float scaredRadius;

	// The time (in seconds) that the sheep will be scared for
	public float totalScaredTime;

	// Use this for initialization
	void Start ()
	{
		// Fetch rigidbody
		rigidbody = GetComponent<Rigidbody>();

		// Fetch the player
		player = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Check if no target is set
		if (target == null)
		{
			// Fetch all targets
			SheepMischiefMarker[] markers = GameObject.FindObjectsOfType<SheepMischiefMarker>();

			// Pick a random one
			int targetID = Random.Range(0, markers.Length);

			// Select this target
			target = markers[targetID];
		}
		else
		{
			// Set the target direction to the direction from the player to this sheep
			Vector3 direction = transform.position - player.transform.position;

			// Check if the player is near
			if (direction.magnitude < scaredRadius)
			{
				// Set scared timer
				scaredTimer = totalScaredTime;
			}

			// Check if the sheep is currently scared
			if (scaredTimer > 0)
			{
				// Decrement timer
				scaredTimer -= Time.deltaTime;
			}
			else
			{
				// Pursue the target
				// Get a direction vector
				direction = target.transform.position - transform.position;
			}

			// Go to target if we aren't there already
			if (direction.sqrMagnitude > 1)
			{
				// Normalize vector and move towards it
				direction.Normalize();

				// Don't add force if we are going too fast
				rigidbody.AddForce(direction * Time.deltaTime * runForce);

				// Create a point to look at
				/*Vector3 lookTarget = target.transform.position;
				lookTarget.y = transform.position.y;

				// Look at the target
				body.transform.LookAt(lookTarget);*/

				// Look in our move direction
				body.transform.LookAt(transform.position + direction);
			}
		}
	}
}
