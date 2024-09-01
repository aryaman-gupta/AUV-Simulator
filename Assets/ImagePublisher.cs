using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROSBridgeLib;
using ROSBridgeLib.std_msgs;
using ROSBridgeLib.auv_msgs;
using SimpleJSON;

public class ImagePublisher : ROSBridgePublisher  {

	// The following three functions are important
	public static string GetMessageTopic() {
		return "/images";
	}

	public static string GetMessageType() {
		return "std_msgs/String";
	}

	public static string ToYAMLString(StringMsg msg) {
		return msg.ToYAMLString();
	}

	public new static ROSBridgeMsg ParseMessage(JSONNode msg) {
		return new StringMsg(msg);
	}    
}
