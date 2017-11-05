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
	public void AddForce (float ForceMag) {
//		Debug.Log ("Applying force " + ForceMag + " at pos" + posForce);
//		Vector3 localDir = gameObject.transform.parent.parent.transform.rotation.eulerAngles;
//		Vector3 actualDir;
//		Debug.Log (localDir);
//		Debug.Log (dir);
//		actualDir.x = dir.x * localDir.x;
//		actualDir.y = dir.y * localDir.y;
//		actualDir.z = dir.z * localDir.z;
//		Debug.Log (actualDir);

		if(gameObject.transform.position.y < water.transform.position.y)
		{
			
			transform.GetComponent<Rigidbody> ().AddRelativeForce (
				dir * ForceMag, ForceMode.Force);

		}
}
}