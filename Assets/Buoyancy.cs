using UnityEngine;
using System.Collections;

public class Buoyancy : MonoBehaviour {
	public float waterLevel;

//	public float bounceDamp;
	Vector3 dims;
	float l, b, h;
	public Vector3 CenterOfMass = Vector3.zero;
	public float rho = 1f;
	Vector3 upthrust;
	GameObject COB_Pos;
	void Start()
	{
		dims = GetComponent<Transform> ().localScale;
		Debug.Log (dims);
		l = dims.x;
		h = dims.y;
		b = dims.z;
		GetComponent<Rigidbody> ().centerOfMass = CenterOfMass;
		COB_Pos = GameObject.Find("Center of Buoyancy");
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
//		Vector3 CenterOfBuoyancy = COB_Pos.transform.position;
//		Debug.Log (CenterOfBuoyancy);
//		COB_Pos.transform.GetComponent<Rigidbody>().AddForceAtPosition(upthrust, Vector3.zero);
		transform.GetComponent<Rigidbody>().AddForceAtPosition(upthrust, transform.position);
		Debug.Log ("Applying buoyancy at " + transform.position);

	}
}
