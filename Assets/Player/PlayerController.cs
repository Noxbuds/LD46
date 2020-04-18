using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
	// Target to walk/run to
	private Vector3 target;

	[Header("Physical components")]
	// The player body
	public GameObject body;

	// The main camera
	public Camera playerCamera;

	// The terrain
	public Terrain terrain;

	// The rigidbody attached to the player
	private new Rigidbody rigidbody; // using 'new' here to hide Component.rigidbody, which is more finnicky to work with

	// The animation controller
	[SerializeField] // read-only in editor
	private PlayerAnimationController animator;

	[Header("Player properties")]
	// Running force
	public float runForce;

	// Max running speed (units/sec)
	public float runSpeed;

	// Use this for initialization
	void Start ()
	{
		// Set the camera if not set already
		if (playerCamera == null)
			playerCamera = Camera.main;

		// Fetch rigidbody
		rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Check for click
		if (Input.GetMouseButton(0))
		{
			// Get the target screen position
			Vector3 screenTarget = Input.mousePosition;

			// Create a ray from the screen position
			Ray ray = playerCamera.ScreenPointToRay(screenTarget);

			// Ray cast with the terrain
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100.0f))
			{
				// Set the target to the hit position
				target = hit.point;
			}
		}

		// Move if we have a target
		if (target != null)
		{
			// Get a vector from our position to the target position
			Vector3 direction = target - transform.position;

			// Check if we are at the target already
			if (direction.sqrMagnitude > 1)
			{

				// If not, we want to move towards the target
				// Normalize the direction vector and apply it
				direction.y *= 0.0f; // don't want to climb up mountains too much
				direction.Normalize();

				// Limit speed
				if (rigidbody.velocity.sqrMagnitude < runSpeed)
					rigidbody.AddForce(direction * runForce * Time.deltaTime); // multiply by delta time to have consistent run speed

				// Setup a look target
				Vector3 lookTarget = target;
				lookTarget.y = transform.position.y;

				// Apply the direction
				body.transform.LookAt(lookTarget);

				// Do running animation
				animator.StartRunning();
			}
			else
			{
				// Once we reach the target, stop
				target = transform.position;

				// Stop running
				animator.StopRunning();
			}
		}
	}
}
