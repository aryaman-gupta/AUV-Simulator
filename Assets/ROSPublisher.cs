using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROSBridgeLib;
using ROSBridgeLib.std_msgs;
using ROSBridgeLib.auv_msgs;
using SimpleJSON;

public class ROSPublisher : ROSBridgePublisher  {

	// The following three functions are important
	public static string GetMessageTopic() {
		return "/imu_data";
	}

	public static string GetMessageType() {
		return "new_hammerhead_control/ctrl_input";
	}

	public static string ToYAMLString(Ctrl_InputMsg msg) {
		return msg.ToYAMLString();
	}

	public new static ROSBridgeMsg ParseMessage(JSONNode msg) {
		return new Ctrl_InputMsg(msg);
	}    
}
