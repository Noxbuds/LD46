using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to mark a location where sheep want to go and make mischief, eg jumping a fence
/// </summary>
public class SheepMischiefMarker : MonoBehaviour
{
	// Mischief type
	public enum MischiefType
	{
		Low_Fence, Broken_Fence
	}

	// This marker's type
	public MischiefType type;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	// Called in the editor when gizmos are drawn
	private void OnDrawGizmos()
	{
		// Draw a gizmo at the marker
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(transform.position, 1);
	}
}
