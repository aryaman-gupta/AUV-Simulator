using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROSBridgeLib;
using ROSBridgeLib.auv_msgs;
using SimpleJSON;


public class ROSSubscriber : ROSBridgeSubscriber  {

	public static short[] ForceVals = {1500, 1500, 1500, 1500, 1500, 1500};

	public new static string GetMessageTopic() {
		return "/thruster_speeds";
	}

	public new static string GetMessageType() {
		return "thruster_controller/ThrusterSpeeds";
	}

	public new static ROSBridgeMsg ParseMessage(JSONNode msg) {
		return new ThrusterSpeedsMsg (msg);
	}

	// This function should fire on each ros message
	public new static void CallBack(ROSBridgeMsg msg) {
		Debug.Log (msg.ToYAMLString());
		ThrusterSpeedsMsg thrustVals = (ThrusterSpeedsMsg)msg;
		ForceVals = thrustVals.GetData ();
	}
}