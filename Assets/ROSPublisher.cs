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
		return "/chatter20";
	}

	public static string GetMessageType() {
		return "synchronizer/Combined";
	}

	public static string ToYAMLString(CombinedMsg msg) {
		return msg.ToYAMLString();
	}

	public new static ROSBridgeMsg ParseMessage(JSONNode msg) {
		return new CombinedMsg(msg);
	}    
}
