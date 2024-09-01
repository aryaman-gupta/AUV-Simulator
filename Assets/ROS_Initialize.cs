﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROSBridgeLib;
using UnityEngine.UI;
public class ROS_Initialize : MonoBehaviour {

	public ROSBridgeWebSocketConnection ros = null;
	// Use this for initialization
	void Start () {
		ros = new ROSBridgeWebSocketConnection ("ws://"+MainMenu.ip, 9090);
		ros.AddSubscriber (typeof(ROSSubscriber));
		ros.AddPublisher (typeof(ImagePublisher));

		ros.AddPublisher (typeof(ROSPublisher));

		ros.Connect ();
	}

	// Extremely important to disconnect from ROS. Otherwise packets continue to flow
	void OnApplicationQuit() {
		if(ros!=null) {
			Debug.Log ("Disconnecting!");
			ros.Disconnect ();
		}
	}

	// Update is called once per frame
	void Update () {
		ros.Render();
	}
}

