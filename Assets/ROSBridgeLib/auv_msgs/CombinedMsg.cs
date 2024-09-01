using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

namespace ROSBridgeLib {
	namespace auv_msgs {
		public class CombinedMsg : ROSBridgeMsg {
			private float[] angular, linear;

			public CombinedMsg(float[] _angular, float[] _linear) {
				angular = _angular;
				linear = _linear;
			}

			public CombinedMsg(JSONNode msg)
			{
				angular = new float[msg["angular"].Count];
				for (int i = 0; i < angular.Length; i++) {
					angular[i] = float.Parse(msg["angular"][i]);
				}

				linear = new float[msg["linear"].Count];
				for (int i = 0; i < linear.Length; i++) {
					linear[i] = float.Parse(msg["linear"][i]);
				}
			}

			public static string getMessageType() {
				return "synchronizer/Combined";
			}

			public float[] GetAngular() {
				return angular;
			}

			public float[] GetLinear() {
				return linear;
			}

			public override string ToString ()
			{
				string array = "[";
				for (int i = 0; i < angular.Length; i++) {
					array = array + angular[i];
					if (angular.Length - i > 1)
						array += ",";
				}
				array += "]";

				string array2 = "[";
				for (int i = 0; i < linear.Length; i++) {
					array2 = array2 + linear[i];
					if (linear.Length - i > 1)
						array2 += ",";
				}
				array2 += "]";
				return "CombinedMsg [angular=" + array + ", linear=" + array2 + "]";
			}

			public override string ToYAMLString() {
				string array = "[";
				for (int i = 0; i < angular.Length; i++) {
					array = array + angular[i];
					if (angular.Length - i > 1)
						array += ",";
				}
				array += "]";

				string array2 = "[";
				for (int i = 0; i < linear.Length; i++) {
					array2 = array2 + linear[i];
					if (linear.Length - i > 1)
						array2 += ",";
				}
				array2 += "]";
				return "{\"angular\" : " + array + ", \"linear\" : " + array2 + "}";
			}

		}
	}
}
