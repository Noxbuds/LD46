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

	// Turn speed
	public float turnSpeed;

	// The sheep's body
	public GameObject body;

	// The radius in which the sheep will be scared of the dog
	public float scaredRadius;

	// The time (in seconds) that the sheep will be scared for
	public float totalScaredTime;

	// The square of the distance where a sheep will be satisfied with a marker
	public float sqrSatisfiedDistance;

	[Header("Animation")]
	// Used to control the sheep's animations
	public SheepAnimationControl animator;

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
			SheepMischiefMarker[] markers = FindObjectOfType<SheepManager>().markers;// GameObject.FindObjectsOfType<SheepMischiefMarker>();

			// Pick a random one
			int targetID = Random.Range(0, markers.Length);

			// Select this target
			target = markers[targetID];

			// Start running
			animator.StartRunning();
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

				// Start running
				animator.StartRunning();

				// Trigger a bark
				FindObjectOfType<DogBarkController>().TriggerBark();
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

			// Calculate distance to target
			float distance = direction.sqrMagnitude;

			// Go to target if we aren't there already
			if (distance > sqrSatisfiedDistance)
			{
				// Normalize vector and move towards it
				direction.Normalize();

				// Don't add force if we are going too fast
				rigidbody.AddForce(body.transform.forward * Time.deltaTime * runForce);

				// Set up a 'look target' and force its Y position to this sheep's Y position for correct angles
				Vector3 lookTarget = transform.position + direction;
				lookTarget.y = transform.position.y;

				// Target position
				Vector3 currentTarget = transform.position + body.transform.forward * distance;

				// Point to look at
				Vector3 lookPoint = Vector3.Lerp(currentTarget, lookTarget, Time.deltaTime * turnSpeed);

				// Set rotation
				body.transform.LookAt(lookPoint);
			}
			else
			{
				// Stop running and idle
				animator.StopRunning();

				// Check for next location
				if (target.nextLocation != null)
				{
					// Go to next location
					target = target.nextLocation;

					// Start running
					animator.StartRunning();
				}
				else
				{
					// Pick new location
					//target = null;
				}
			}
		}
	}
}
