using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

namespace ROSBridgeLib {
	namespace auv_msgs {
		public class Ctrl_InputMsg : ROSBridgeMsg {
			private float[] velocity, acceleration, angle, omega;
			private float depth;

			public Ctrl_InputMsg(float[] _velocity, float[] _acceleration, float[] _angle, float[] _omega, float _depth) {
				velocity = _velocity;
				acceleration = _acceleration;
				angle = _angle;
				omega = _omega;
				depth = _depth;
			}

			public Ctrl_InputMsg(JSONNode msg)
			{
				velocity = new float[msg["velocity"].Count];
				for (int i = 0; i < velocity.Length; i++) {
					velocity[i] = float.Parse(msg["velocity"][i]);
				}

				acceleration = new float[msg["acceleration"].Count];
				for (int i = 0; i < acceleration.Length; i++) {
					acceleration[i] = float.Parse(msg["acceleration"][i]);
				}

				angle = new float[msg["angle"].Count];
				for (int i = 0; i < angle.Length; i++) {
					angle[i] = float.Parse(msg["angle"][i]);
				}

				omega = new float[msg["omega"].Count];
				for (int i = 0; i < omega.Length; i++) {
					omega[i] = float.Parse(msg["omega"][i]);
				}

				depth = float.Parse(msg["depth"]);
			}

			public static string getMessageType() {
				return "new_hammerhead_control/ctrl_input";
			}

			public float[] GetVelocity() {
				return velocity;
			}

			public float[] GetAcceleration() {
				return acceleration;
			}

			public float[] GetAngle() {
				return angle;
			}

			public float[] GetOmega() {
				return omega;
			}

			public float GetDepth() {
				return depth;
			}

			public override string ToString ()
			{
				string array = "[";
				for (int i = 0; i < velocity.Length; i++) {
					array = array + velocity[i];
					if (velocity.Length - i > 1)
						array += ",";
				}
				array += "]";

				string array2 = "[";
				for (int i = 0; i < acceleration.Length; i++) {
					array2 = array2 + acceleration[i];
					if (acceleration.Length - i > 1)
						array2 += ",";
				}
				array2 += "]";

				string array3 = "[";
				for (int i = 0; i < angle.Length; i++) {
					array3 = array3 + angle[i];
					if (angle.Length - i > 1)
						array3 += ",";
				}
				array3 += "]";

				string array4 = "[";
				for (int i = 0; i < omega.Length; i++) {
					array4 = array4 + omega[i];
					if (omega.Length - i > 1)
						array4 += ",";
				}
				array4 += "]";


				return "CombinedMsg [velocity=" + array + ", acceleration=" + array2 + ", angle=" + array3 + 
					", omega=" + array4 + ", depth=" + depth +"]";
			}

			public override string ToYAMLString() {
				string array = "[";
				for (int i = 0; i < velocity.Length; i++) {
					array = array + velocity[i];
					if (velocity.Length - i > 1)
						array += ",";
				}
				array += "]";

				string array2 = "[";
				for (int i = 0; i < acceleration.Length; i++) {
					array2 = array2 + acceleration[i];
					if (acceleration.Length - i > 1)
						array2 += ",";
				}
				array2 += "]";

				string array3 = "[";
				for (int i = 0; i < angle.Length; i++) {
					array3 = array3 + angle[i];
					if (angle.Length - i > 1)
						array3 += ",";
				}
				array3 += "]";

				string array4 = "[";
				for (int i = 0; i < omega.Length; i++) {
					array4 = array4 + omega[i];
					if (omega.Length - i > 1)
						array4 += ",";
				}
				array4 += "]";


				return "{\"velocity\" : " + array + ", \"acceleration\" : " + array2 + ", \"angle\" : " + array3 +
					", \"omega\" : " + array4 + ", \"depth\" : " + depth +"}";
			}

		}
	}
}
