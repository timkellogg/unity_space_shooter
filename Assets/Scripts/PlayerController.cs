using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// need to serialize properties to view them in the inspector
[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float tilt;
	public Boundary boundary;
	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
	}

	// executed once per physics step
	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		// apply to player game object
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;

		rb.position = new Vector3
		(
			// clamp sets constraints within a given range
			Mathf.Clamp	(rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
		);

		// applies degrees of rotation around Vector3. Here applying a banking to the z axis
		rb.rotation = Quaternion.Euler (rb.velocity.z * -tilt/2, 0.0f, rb.velocity.x * -tilt);

	}
}
