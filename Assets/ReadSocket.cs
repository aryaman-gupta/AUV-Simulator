using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System;
using System.IO;
using System.Text;
using ROSBridgeLib;
using ROSBridgeLib.auv_msgs;
using SimpleJSON;


public class ReadSocket : MonoBehaviour {

	// Use this for initialization
//	TcpListener listener;
	String msg;
	public static short[] ForceVals = {1500, 1500, 1500, 1500, 1500, 1500};
	bool firstCall = true;
//	public Int32 ListenPort = 55002;
//	private static GameObject thrusters;
	void Start () {
		Debug.Log ("In Start");
//		listener=new TcpListener (ListenPort);
//		listener.Start ();
//		print ("is listening");
		Time.fixedDeltaTime = 0.04f;
	}



	/* During initialisation of the system, the user needs to press the space key to attempt to 
	 * set up a socket connection with the control algorithm. This should be done after the server
	 * at the control algorithm machine has been started.
	 * */
	void Update()
	{
//		if (Input.GetKey (KeyCode.Space)) {
//			if (firstCall) {
//				firstCall = false;
//				StartCoroutine (gameObject.GetComponent<ControlThrusters> ().SetupSocket ());
//				Debug.Log ("Back Here");
//			}
//		}
	}

	/* The FixedUpdate function is responsible for receiving data from the control algorithm, i.e.,
	 * the values of forces at the thrusters. Once the control algorithm connects as a client, this
	 * function is responsible for periodically receiving the data, processing it, and storing the 
	 * values in the global variable 'ForceVals'.
	 * */
	void FixedUpdate()
	{
//		if (!listener.Pending ())
//		{
////						Debug.Log ("Listening");
//		} 
//		else 
//		{
//
//			//Debug.Log("socket comes");
//			TcpClient client = listener.AcceptTcpClient ();
//			NetworkStream ns = client.GetStream ();
//			StreamReader reader = new StreamReader (ns);
//			msg = reader.ReadToEnd ();
//
//
//			char[] split1 = { '$' };
//			char[] splitter = { ' ' };
//			firstSplit = msg.Split (split1, StringSplitOptions.RemoveEmptyEntries);
//			Debug.Log (firstSplit[0]);
//			ForceVals = firstSplit[0].Split (splitter, StringSplitOptions.RemoveEmptyEntries);
//
//
//		}
		ForceVals = ROSSubscriber.ForceVals;
		Debug.Log ("ForceVals = " + ForceVals[0] + " " + ForceVals[1] + " " + ForceVals[2] + " " + ForceVals[3] + " " + 
			ForceVals[4] + " " + ForceVals[5] + " " );
		gameObject.GetComponent<ControlThrusters> ().Lock = false;

	}

}