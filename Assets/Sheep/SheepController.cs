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
	[SerializeField]
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

	[Header("Sound")]
	public AudioSource[] sounds;

	// Sound interval
	private float soundTimer;
	public float soundInterval;

	[Header("Animation")]
	// Used to control the sheep's animations
	public SheepAnimationControl animator;

	/// <summary>
	/// Selects a new target
	/// </summary>
	private void SelectNewTarget()
	{
		// Fetch all targets
		SheepMischiefMarker[] markers = FindObjectOfType<SheepManager>().markers;

		// Pick a random one
		int targetID = Random.Range(0, markers.Length);

		// Select this target
		target = markers[targetID];
	}

	/// <summary>
	/// Triggers a random sheep sound
	/// </summary>
	private void TriggerSound()
	{
		// Pick random sound
		int soundID = Random.Range(0, sounds.Length);

		// Play the sound if it isn't already playing
		if (!sounds[soundID].isPlaying && soundTimer <= 0)
		{
			// Play the sound
			sounds[soundID].Play();

			// Set sound interval
			soundTimer = soundInterval;
		}
	}

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
		// Decrement sound timer
		soundTimer -= Time.deltaTime;

		// Check if no target is set
		if (target == null)
		{
			// Select new target
			SelectNewTarget();

			// Start running
			animator.StartRunning();
		}
		else
		{
			// If the sheep is not scared, we will set this to a vector pointing towards the target
			// By default we will handle the case where the sheep is scared. In this case, we want the sheep
			// to run away from the dog, but also to not run out of the fence
			Vector3 direction = transform.position - player.transform.position;

			// Check if the player is near
			if (direction.magnitude < scaredRadius)
			{
				// Set scared timer
				scaredTimer = totalScaredTime;

				// Play a 'baaa' sound
				TriggerSound();

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

				// Select new target
				SelectNewTarget();
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
			// Only stop if the sheep is not scared
			else if (scaredTimer <= 0)
			{
				// Stop running and idle
				animator.StopRunning();

				// Play a sound to show that the sheep is satisfied
				TriggerSound();

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
