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



public class PingerData : MonoBehaviour {

    public GameObject[] drums;
    public GameObject pinger;
    public GameObject auv;
    Float32Msg pingermsg;
    GameObject obj;
    
    void Awake()
    {
        drums = GameObject.FindGameObjectsWithTag("Drum");
    }

    void Start () {

        obj = GameObject.Find("Main Camera");

        //randomizing the positions of drums
        for (int i = 0; i < drums.Length; i++)
        {
            GameObject obj = drums[i];
            int random_i = UnityEngine.Random.Range(0,i);
            drums[i] = drums[random_i];
            drums[random_i] = obj;
            Vector3 pos = drums[i].transform.position;
            drums[i].transform.position = drums[random_i].transform.position;
            drums[random_i].transform.position = pos;
            
        }

        //placing the pinger in a random drum
        int random_drum = UnityEngine.Random.Range(0, drums.Length);
       pinger.transform.position= drums[random_drum].transform.position;
        
	}
    float PingerAngle()
    {
        Vector3 a = auv.transform.right;
        a.y = 0;
        Vector3 p = pinger.transform.position - auv.transform.position;
        p.y = 0;
        float d = Vector3.Angle(a, p);
        if (Vector3.Cross(a, p).y > 0)
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
            float p = PingerAngle();
            pingermsg = new Float32Msg(p);
            obj.GetComponent<ROS_Initialize>().ros.Publish(PingerPublisher.GetMessageTopic(), pingermsg);
            Debug.Log("Sending: PingerAngle = " + p);
            Debug.Log("Sending to topic: " + PingerPublisher.GetMessageTopic());
        }
        catch(Exception e)
        {
            Debug.Log("Socket error: " + e);
        }
    }






}
