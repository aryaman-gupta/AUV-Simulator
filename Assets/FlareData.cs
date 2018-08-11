using System.Collections;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using ROSBridgeLib;
using ROSBridgeLib.auv_msgs;
using ROSBridgeLib.std_msgs;

public class FlareData : MonoBehaviour {

    public GameObject flare;
    public GameObject auv;
    Float32Msg flaremsg;
    GameObject obj;

    void Start() {
        obj = GameObject.Find("Main Camera");

        //placing the flare at a random position in a specific area :
        float rz = UnityEngine.Random.Range(-70, 70);
        if (rz < 20 && rz > -20)
        {
            rz = rz + 20;
        }

        float rx = UnityEngine.Random.Range(0, 30);
        Vector3 pos = new Vector3(rx, -7, rz);
        flare.transform.position = pos;
        
    }
    public float FlareAngle()
    {
        
        Vector3 a = auv.transform.right;
        a.y = 0;
        Vector3 f = flare.transform.position - auv.transform.position;
        f.y = 0;
        float d = Vector3.Angle(a, f);
        if (Vector3.Cross(a, f).y > 0)
        {
            return d;
        }
        else
        {
            return -d;
        }
        
       
   }
    void Update()
    {
        try
        {
            float d = FlareAngle();
            flaremsg = new Float32Msg(d);
            obj.GetComponent<ROS_Initialize>().ros.Publish(FlarePublisher.GetMessageTopic(), flaremsg);
            Debug.Log("Sending: FlareAngle = " + d);
            Debug.Log("Sending to topic: " + FlarePublisher.GetMessageTopic());
            
        }
        catch (Exception e)
        {
            Debug.Log("Socket error: " + e);
        }
    }
}
