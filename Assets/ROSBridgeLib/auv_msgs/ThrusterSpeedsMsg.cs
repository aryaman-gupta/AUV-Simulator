using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;


namespace ROSBridgeLib {
	namespace auv_msgs {
		public class ThrusterSpeedsMsg : ROSBridgeMsg {
			private short[] data, reverse;

			public ThrusterSpeedsMsg(short[] data,short[] reverse) {
				this.data = data;
				this.reverse = reverse;
			}

			public ThrusterSpeedsMsg(JSONNode msg)
			{
				data = new short[msg["data"].Count];
				for (int i = 0; i < data.Length; i++) {
					data[i] = short.Parse(msg["data"][i]);
				}

				reverse = new short[msg["reverse"].Count];
				for (int i = 0; i < reverse.Length; i++) {
					reverse[i] = short.Parse(msg["reverse"][i]);
				}
			}

			public static string getMessageType() {
				return "thruster_controller/ThrusterSpeeds";
			}

			public short[] GetData() {
				return data;
			}

			public short[] GetReverse() {
				return reverse;
			}

			public override string ToString ()
			{
				string array = "[";
				for (int i = 0; i < data.Length; i++) {
					array = array + data[i];
					if (data.Length - i > 1)
						array += ",";
				}
				array += "]";

				string array2 = "[";
				for (int i = 0; i < reverse.Length; i++) {
					array2 = array2 + reverse[i];
					if (reverse.Length - i > 1)
						array2 += ",";
				}
				array2 += "]";
				return "ThrusterSpeedsMsg [data=" + array + ", reverse=" + array2 + "]";
			}

			public override string ToYAMLString() {
				string array = "[";
				for (int i = 0; i < reverse.Length; i++) {
					array = array + reverse[i];
					if (reverse.Length - i > 1)
						array += ",";
				}
				array += "]";

				string array2 = "[";
				for (int i = 0; i < reverse.Length; i++) {
					array2 = array2 + reverse[i];
					if (reverse.Length - i > 1)
						array2 += ",";
				}
				array2 += "]";
				return "{\"data\" : " + array + ", \"reverse\" : " + array2 + "}";
			}

		}
	}
}
