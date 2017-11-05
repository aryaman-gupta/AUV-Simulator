using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System;
using System.IO;
using System.Text;


public class ReadSocket : MonoBehaviour {

	// Use this for initialization
	TcpListener listener;
	String msg;
	public string[] ForceVals = {"0", "0", "0", "0"};
	bool firstCall = true;
	public Int32 ListenPort = 55002;
	void Start () {
		Debug.Log ("Here");
		listener=new TcpListener (ListenPort);
		listener.Start ();
		print ("is listening");
		Time.fixedDeltaTime = 0.04f;
	}
	// Update is called once per frame

	void Update()
	{
		if (Input.GetKey (KeyCode.Space)) {
			if (firstCall) {
				firstCall = false;
				StartCoroutine (gameObject.GetComponent<ControlThrusters> ().SetupSocket ());
				Debug.Log ("Back Here");
			}
		}
	}

	void FixedUpdate()
	{
		

		if (!listener.Pending ())
		{
			//			Debug.Log ("Listening");
		} 
		else 
		{

			Debug.Log("socket comes");
			TcpClient client = listener.AcceptTcpClient ();
			NetworkStream ns = client.GetStream ();
			StreamReader reader = new StreamReader (ns);
			msg = reader.ReadToEnd ();

			Debug.Log (msg);
			char[] splitter = { ' ' };
			ForceVals = msg.Split (splitter, StringSplitOptions.RemoveEmptyEntries);
//			gameObject.GetComponent<ControlThrusters> ().AddForces ();

			gameObject.GetComponent<ControlThrusters> ().Lock = false;

		}
	}

}