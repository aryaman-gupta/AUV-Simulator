#define notSelf
#define UsePics


using System.Collections;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using ROSBridgeLib;
using ROSBridgeLib.auv_msgs;
using ROSBridgeLib.std_msgs;

public class ControlThrusters : MonoBehaviour {

	int ImageWidth = 648;
	int ImageHeight = 488;

	internal Boolean socketReady = false;
	TcpClient mySocket;
	NetworkStream theStream;
	StreamWriter theWriter;
	StreamReader theReader;
	public Int32 WritePort = 55001;
	Int32 Port;
//	bool Ready = false;
	GameObject bottomCam;
	GameObject frontCam;
	RenderTexture bottomImage;
	RenderTexture frontImage;
	Texture2D imageToSend;
	Texture2D imageToSend2;
	Vector3 prevVelocity = Vector3.zero;
	Vector3 prevRot;
	bool firstSend = true;
	public int colThreshHigh = 100;
	public int colThreshLow = 100;

	public string IPaddress = "192.168.2.4";
	public bool Lock = false;
	int iterations;
	GameObject obj;
	Ctrl_InputMsg msg;
	StringMsg imgMsg;
	void Start () {
		Port = WritePort;
		iterations = 0;
		Time.fixedDeltaTime = 0.04f;
		obj = GameObject.Find ("Main Camera");
		prevRot = transform.parent.transform.rotation.eulerAngles;
		#if UsePics
		bottomImage = new RenderTexture (ImageWidth, ImageHeight, 16, RenderTextureFormat.ARGB32);
		bottomImage.Create ();
		bottomCam = GameObject.Find ("BottomCam");
		bottomCam.GetComponent<Camera> ().targetTexture = bottomImage;
		bottomCam.GetComponent<Camera> ().Render ();

		frontImage = new RenderTexture (ImageWidth, ImageHeight, 16, RenderTextureFormat.ARGB32);
		frontImage.Create ();
		frontCam = GameObject.Find ("FrontCam");
		frontCam.GetComponent<Camera> ().targetTexture = frontImage;
		frontCam.GetComponent<Camera> ().Render ();

		imageToSend = new Texture2D(bottomImage.width, bottomImage.height, TextureFormat.RGB24, false);
		imageToSend2 = new Texture2D (frontImage.width, frontImage.height, TextureFormat.RGB24, false);
		#endif
	}


	/* Update function sends images from the cameras attached to the AUV, to the control algorithm. In order
	 * to maintain sufficient communication frequency, images cannot be sent in their entirety. Therefore, 
	 * edge detection is performed on the images, to detect meaningful data.
	 * 
	*/
	void Update (){
		// Examine whether Lock is required
		Vector3 CurRot = transform.parent.transform.rotation.eulerAngles;
//		if(CurRot.x > 180.0f)
//			CurRot.x -= 360.0f;
//		if(CurRot.y > 180.0f)
//			CurRot.y -= 360.0f;
//		if(CurRot.z > 180.0f)
//			CurRot.z -= 360.0f;
//
//		CurRot *= 3.14f/180.0f;
//		Debug.Log (CurRot);
		#if UsePics

//		if(Input.GetKey(KeyCode.Space))
//			firstSend = true;
//		Debug.Log (Time.deltaTime);
		if(!Lock)
		{


		Lock = true;

		firstSend = false;
		RenderTexture.active = bottomImage;
		imageToSend.ReadPixels (new Rect (0, 0, bottomImage.width, bottomImage.height), 0, 0);
		imageToSend.Apply ();
		StringBuilder pixelsToSend1 = new StringBuilder("", 500000);
		StringBuilder pixelsToSend2 = new StringBuilder("", 500000);
		StringBuilder pixelsToSend3 = new StringBuilder ("", 500000);
		StringBuilder pixelsToSend4 = new StringBuilder("", 500000);
		StringBuilder pixelsToSend5 = new StringBuilder("", 500000);
		StringBuilder pixelsToSend6 = new StringBuilder ("", 500000);
		int cnt = 0;
		Color32[] allPixels = imageToSend.GetPixels32 ();
		
		//Bottom Image R Component
		for (int i = 0; i < bottomImage.height; i++) {
			bool prev = false;
			bool noChanges = true;
			for (int j = 0; j < bottomImage.width; j++) {
				Color32 currentVal = allPixels [(ImageHeight - 1 - i) * bottomImage.width+ j];
				if ((currentVal.r > colThreshHigh && currentVal.g < colThreshLow && currentVal.b < colThreshLow) != prev) {
					noChanges = false;
					prev = !prev;
					pixelsToSend1.Append (j).Append (":");
				}
			}
			if (noChanges)
				pixelsToSend1.Append ("-1");
			pixelsToSend1.Append (">");
		}
		
		//Bottom Image G Component
		for (int i = 0; i < bottomImage.height; i++) {
			bool prev = false;
			bool noChanges = true;
			for (int j = 0; j < bottomImage.width; j++) {
				Color32 currentVal = allPixels [(ImageHeight - 1 - i) * bottomImage.width+ j];
				if ((currentVal.g > colThreshHigh && currentVal.r < colThreshLow && currentVal.b < colThreshLow) != prev) {
					noChanges = false;
					prev = !prev;
					pixelsToSend2.Append (j).Append (":");
				}
			}
			if (noChanges)
				pixelsToSend2.Append ("-1");
			pixelsToSend2.Append (">");
		}

		//Bottom Image B Component
		for (int i = 0; i < bottomImage.height; i++) {
			bool prev = false;
			bool noChanges = true;
			for (int j = 0; j < bottomImage.width; j++) {
				Color32 currentVal = allPixels [(ImageHeight - 1 - i) * bottomImage.width+ j];
				if ((currentVal.b > colThreshHigh && currentVal.r < colThreshLow && currentVal.g < colThreshLow) != prev) {
					noChanges = false;
					prev = !prev;
					pixelsToSend3.Append (j).Append (":");
				}
			}
			if (noChanges)
				pixelsToSend3.Append ("-1");
			pixelsToSend3.Append (">");
		}
		//			for(int i=0; i<bottomImage.width*bottomImage.height; i++) {
		//				Color32 pixel = allPixels [i];
		//				if (pixel.r > colThresh) {
		//					pixelsToSend1.Append(cnt.ToString ("00000")).Append(":");
		//				}
		//				cnt++;
		//			}

		cnt = 0;
		RenderTexture.active = frontImage;
		imageToSend2.ReadPixels (new Rect (0, 0, frontImage.width, frontImage.height), 0, 0);
		imageToSend2.Apply ();

		allPixels = imageToSend2.GetPixels32 ();
		//Front Image R Component
		for (int i = 0; i < frontImage.height; i++) {
			bool prev = false;
			bool noChanges = true;
			for (int j = 0; j < frontImage.width; j++) {
				Color32 currentVal = allPixels [(ImageHeight - 1 - i) * frontImage.width+ j];
				if ((currentVal.r > colThreshHigh && currentVal.g < colThreshLow && currentVal.b < colThreshLow) != prev) {
					noChanges = false;
					prev = !prev;
					pixelsToSend4.Append (j).Append (":");
				}
			}
			if (noChanges)
				pixelsToSend4.Append ("-1");
			pixelsToSend4.Append (">");
		}

		//Front Image G Component
		for (int i = 0; i < frontImage.height; i++) {
			bool prev = false;
			bool noChanges = true;
			for (int j = 0; j < frontImage.width; j++) {
				Color32 currentVal = allPixels [(ImageHeight - 1 - i) * frontImage.width+ j];
				if ((currentVal.g > colThreshHigh && currentVal.r < colThreshLow && currentVal.b < colThreshLow) != prev) {
					noChanges = false;
					prev = !prev;
					pixelsToSend5.Append (j).Append (":");
				}
			}
			if (noChanges)
				pixelsToSend5.Append ("-1");
			pixelsToSend5.Append (">");
		}
		//Front Image B Component
		for (int i = 0; i < frontImage.height; i++) {
			bool prev = false;
			bool noChanges = true;
			for (int j = 0; j < frontImage.width; j++) {
				Color32 currentVal = allPixels [(ImageHeight - 1 - i) * frontImage.width+ j];
				if ((currentVal.b > colThreshHigh && currentVal.r < colThreshLow && currentVal.g < colThreshLow) != prev) {
					noChanges = false;
					prev = !prev;
					pixelsToSend6.Append (j).Append (":");
				}
			}
			if (noChanges)
				pixelsToSend6.Append ("-1");
			pixelsToSend6.Append (">");
		}

		//			for(int i=0; i<frontImage.width*frontImage.height; i++) {
		//				Color32 pixel = allPixels [i];
		//				if (pixel.r > colThresh) {
		//					pixelsToSend2.Append(cnt.ToString ("00000")).Append(":");
		//				}
		//				cnt++;
		//			}

//			Debug.Log(pixelsToSend1.Length + " " + pixelsToSend1);
//			Debug.Log("Bottom Cam" + pixelsToSend1.Length + " " + pixelsToSend1.ToString());

			pixelsToSend1.Append ("!").Append (pixelsToSend2).Append ("!").Append (pixelsToSend3).Append ("!")
				.Append (pixelsToSend4).Append ("!").Append (pixelsToSend5).Append ("!").Append (pixelsToSend6);
//			pixelsToSend1.Append ("!").Append (pixelsToSend3);
		try {

			#if notSelf
			//				theWriter.WriteLine(pixelsToSend1.Length.ToString("000000"));
			//				theWriter.Flush();
//			pixelsToSend1.Insert(0, pixelsToSend1.Length.ToString("00000000") + " ");
				imgMsg = new StringMsg(pixelsToSend1.ToString());
				obj.GetComponent<ROS_Initialize> ().ros.Publish(ImagePublisher.GetMessageTopic(), imgMsg);
                Debug.Log("Sending to topic: " + ImagePublisher.GetMessageTopic());

                //			theWriter.WriteLine(pixelsToSend1);//.ToString().Substring(0, 2000));//.ToString().Substring(0, 1022));
                //				theWriter.Flush();
                //				theWriter.WriteLine(pixelsToSend1.ToString().Substring(0, 2048));
                //				theWriter.WriteLine(pixelsToSend1);
                //				Debug.Log(Encoding.ASCII.GetBytes(pixelsToSend1.ToString()).Length);
                //				theStream.Write(Encoding.ASCII.GetBytes(pixelsToSend1.ToString()), 0, Encoding.ASCII.GetBytes(pixelsToSend1.ToString()).Length);
                //			theWriter.Flush();
#endif

                //			Debug.Log(pixelsToSend1.Length + " " + pixelsToSend1);

                //			Debug.Log(pixelsToSend2.Length + " " + pixelsToSend2);
                //				Debug.Log("Front Cam" + pixelsToSend2.Length + " " + pixelsToSend2.ToString());

                //				Debug.Log(pixelsToSend3.Length + " " + pixelsToSend3);
                //				Debug.Log(pixelsToSend3.Length + " " + pixelsToSend3.ToString());

            } catch (Exception e) {
			Debug.Log ("Socket error" + e);
		}


		//			pixelsToSend2 = "";
		cnt = 0;
		//			foreach (Color32 pixel in imageToSend.GetPixels32()) {
		//				if (pixel.r > colThresh) {
		////					pixelsToSend2 = pixelsToSend2 + " " + (cnt / bottomImage.width).ToString ("000") + " " + 
		////						(cnt % bottomImage.height).ToString ("000") + ":";
		//					Debug.Log (cnt);
		//				}
		//				cnt++;
		//			}
		//			try {
		//				#if notSelf
		//				theWriter.WriteLine(imageToSend.GetPixels32()[0]);
		//				theWriter.Flush();
		//				theWriter.WriteLine(":");
		//				theWriter.Flush();
		//				Debug.Log("afaeeagaggaegggggggggggggggggg " + imageToSend.GetPixels32()[0]);
		//
		//				#endif
		//
		////				Debug.Log(pixelsToSend1.Length + pixelsToSend1);
		//			} catch (Exception e) {
		//				Debug.Log ("Socket error" + e);
		//			}
		}
		#endif
	}


	/* Applies the forces received from the control algorithm to the thrusters.
	 * */
	public void AddForces()
	{
//		string[] ForceVals = gameObject.GetComponent<ReadSocket> ().ForceVals;
		short[] ForceVals = ReadSocket.ForceVals;
		transform.GetChild (0).GetComponent<ThrusterControl> ().AddForce (ForceVals [0]);
		transform.GetChild (1).GetComponent<ThrusterControl> ().AddForce (ForceVals [1]);
		transform.GetChild (2).GetComponent<ThrusterControl> ().AddForce (ForceVals [2]);
		transform.GetChild (3).GetComponent<ThrusterControl> ().AddForce (ForceVals [3]);
		transform.GetChild (4).GetComponent<ThrusterControl> ().AddForce (ForceVals [4]);
		transform.GetChild (5).GetComponent<ThrusterControl> ().AddForce (ForceVals [5]);
	}

	void FixedUpdate () {
//		if (Ready) {
			AddForces ();
			if (!Lock) {
				Lock = true;
				SendData ();
			}
//		}
	}
	

	/* Sets up a TCP socket connection with the server running on the machine runing
	 * the control algorithm.
	 * */
//	public IEnumerator SetupSocket()
//	{
//		Debug.Log ("Setting the socket up");
//		yield return null;
//	try{
//		#if notSelf
//		mySocket = new TcpClient  (IPAddress.Parse(IPaddress).ToString(), Port);
//		theStream = mySocket.GetStream();
//
//		theWriter = new StreamWriter(theStream);
//
//		#endif
//		Ready = true;
//	}
//	catch (Exception e) {
//		Debug.Log("Socket error: " + e);
//	}
//	}


	/* Send sensor data as feedback to the control algorithm. The data sent includes the orientation of the vehicle,
	 * acceleration in all three dimensions, depth under water, and forward velocity in local frame.
	 * */
	void SendData() {
		try {
			Vector3 CurRot = transform.parent.transform.rotation.eulerAngles;
//			Debug.Log("The angles are: " + CurRot);
			if(CurRot.x > 180.0f)
				CurRot.x -= 360.0f;
			if(CurRot.y > 180.0f)
				CurRot.y -= 360.0f;
			if(CurRot.z > 180.0f)
				CurRot.z -= 360.0f;

			CurRot *= (float)Math.PI/180.0f;
			Vector3 curVelocity = transform.parent.transform.InverseTransformVector(transform.parent.GetComponent<Rigidbody>().velocity);
			Vector3 CurAcc = (curVelocity - prevVelocity)/Time.deltaTime;
			prevVelocity = curVelocity;
//			Vector3 Omega = (CurRot - prevRot)/Time.deltaTime;
			Vector3 Omega = transform.parent.transform.InverseTransformVector(transform.parent.GetComponent<Rigidbody>().angularVelocity);
			prevRot = CurRot;
//			string temp = (-CurRot.x).ToString("+000.00;-000.00") + " " + (-CurRot.z).ToString("+000.00;-000.00") + " "
//				+ (CurRot.y).ToString("+000.00;-000.00") + " " + CurAcc.x.ToString("+000.00;-000.00") + " "
//				+ CurAcc.z.ToString("+000.00;-000.00") + " " + (-CurAcc.y).ToString("+000.00;-000.00") + " "
//				+ (-transform.parent.position.y).ToString("+000.00;-000.00") + " "
//				+ transform.parent.GetComponent<Rigidbody>().velocity.x.ToString("+000.00;-000.00") + " $";
//			Debug.Log(temp);

			float modifiedDepth = (-transform.parent.position.y*15.0f)+930.0f;

			//Uncomment for old controller
//			float[] angular = new float[]{-CurRot.x, CurRot.z, CurRot.y};
//			float[] linear = new float[]{CurAcc.x, -CurAcc.z, modifiedDepth};
//
//			msg = new CombinedMsg(angular, linear);

			//For new controller
			float[] velocity = new float[]{curVelocity.x, -curVelocity.z, -curVelocity.y};
			float[] acceleration = new float[]{CurAcc.x, -CurAcc.z, -CurAcc.y};
			float[] angle = new float[]{-CurRot.x, CurRot.z, CurRot.y};
			float[] omega = new float[]{-Omega.x, Omega.z, Omega.y};

			msg = new Ctrl_InputMsg(velocity, acceleration, angle, omega, modifiedDepth);

			
			#if notSelf
			Debug.Log ("Sending: Depth = " + modifiedDepth);
			Debug.Log ("Sending: Force = " + CurAcc.y * 15000);

			Debug.Log("Sending to topic: " + ROSPublisher.GetMessageTopic());
			obj.GetComponent<ROS_Initialize> ().ros.Publish(ROSPublisher.GetMessageTopic(), msg);
			#endif

		}
		catch (Exception e) {
			Debug.Log("Socket error: " + e);
		}
	}
}