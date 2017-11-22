using UnityEngine;
using System.Collections;

public class Buoyancy : MonoBehaviour {
	public float waterLevel;
//	public Vector3 buoyancyCentreOffset;
//	public float bounceDamp;
	Vector3 dims;
	float l, b, h;
	public Vector3 CenterOfMass = Vector3.zero;
	public float rho = 1f;
	Vector3 upthrust;

	void Start()
	{
		dims = GetComponent<Transform> ().localScale;
		Debug.Log (dims);
		l = dims.x;
		h = dims.y;
		b = dims.z;
		GetComponent<Rigidbody> ().centerOfMass = CenterOfMass;
	}

	void FixedUpdate () {
		float y = transform.position.y;
		Vector3 MaxUpthrust = l * b * h * rho * Physics.gravity * -1000f;

		if (waterLevel - y > h / 2)
			upthrust = MaxUpthrust;
		else if (y - waterLevel > h / 2)
			upthrust = Vector3.zero;
		else
			upthrust = MaxUpthrust / 2 + (waterLevel - y) * MaxUpthrust / h;
		GetComponent<Rigidbody>().AddForceAtPosition(upthrust, transform.position);
	}
}
