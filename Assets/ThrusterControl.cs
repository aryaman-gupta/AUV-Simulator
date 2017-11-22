using UnityEngine;
using System.Collections;

public class ThrusterControl : MonoBehaviour {

	public Vector3 dir;
	Vector3 posForce;
	GameObject water;
	void Start()
	{
		water = GameObject.Find ("Water Surface");
		posForce = transform.localPosition;
	}

	/* A public function which can be called from another script with the
	 * value of the force to be applied as argument. Applies the force to
	 * the thruster to which this script is attached, in the direction as
	 * specified in the global variable, for that thruster.
	 * */
	public void AddForce (float ForceMag) {

		if(gameObject.transform.position.y < water.transform.position.y)
		{
			
			transform.GetComponent<Rigidbody> ().AddRelativeForce (
				dir * ForceMag, ForceMode.Force);

		}
}
}