using UnityEngine;
using System.Collections;

public class ThrusterControl : MonoBehaviour {

	public Vector3 dir;
	public int thrusterNumber;
	Vector3 posForce;
	GameObject water;

	/* PWM to force, as per BlueRobotics T100 thruster.
	 * */
	float adjustForces(float initial) {
		float adjusted = 0.0f;

		if (initial >= 1470.0 && initial <= 1530.0) {
			adjusted = 0.0f;
//			Debug.Log ("Adjusted = " + adjusted + " for thruster " + thrusterNumber);
		}
		else if (initial > 1530) {
			initial -= 1530.0f;
			adjusted = initial / 370.0f * 2.36f;
		} else {
			initial -= 1470.0f;
			adjusted = initial / 370.0f * 1.85f;
		}
		adjusted *= 9.8f * 1000.0f;
//		Debug.Log ("Adjusted before return = " + adjusted + " for thruster " + thrusterNumber);

		return adjusted;
	}

	void Start() {
		water = GameObject.Find ("Water Surface");
		posForce = transform.localPosition;
	}

	/* A public function which can be called from another script with the
	 * value of the force to be applied as argument. Applies the force to
	 * the thruster to which this script is attached, in the direction as
	 * specified in the global variable, for that thruster.
	 * */
	public void AddForce (float ForceMag) {

		ForceMag = adjustForces (ForceMag);
//		Debug.Log ("Force " + ForceMag + " being applied to thruster " + thrusterNumber);

		if(true)//gameObject.transform.position.y < water.transform.position.y)
		{
			
			transform.GetComponent<Rigidbody> ().AddRelativeForce (
				dir * ForceMag, ForceMode.Force);

		}
}
}