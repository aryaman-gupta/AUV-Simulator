using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROSBridgeLib;
using ROSBridgeLib.std_msgs;
using ROSBridgeLib.auv_msgs;
using SimpleJSON;

public class FlarePublisher : ROSBridgePublisher
{

    // The following three functions are important
    public static string GetMessageTopic()
    {
        return "/angle_flare";
    }

    public static string GetMessageType()
    {
        return "std_msgs/Float32Msg";
    }

    public static string ToYAMLString(Float32Msg msg)
    {
        return msg.ToYAMLString();
    }

    public new static ROSBridgeMsg ParseMessage(JSONNode msg)
    {
        return new Float32Msg(msg);
    }
}
