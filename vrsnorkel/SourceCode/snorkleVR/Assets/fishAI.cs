using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class fishAI : MonoBehaviour {

	public float thrust;
	public bool directionAdjustment;
	public float BezierTime = 0;
	public float minDistanceToEndPoint;
	public Plane left;
	public Plane right;
	public Plane up;
	public Plane down;
	public Plane front;
	public Plane back;

	Vector3 start, control, end;

	// Use this for initialization
	void Start () {
		setTarget ();

		if (directionAdjustment == false) {
			this.GetComponent<Rigidbody> ().AddForce (this.transform.forward * thrust, ForceMode.VelocityChange);
		} else {
			this.GetComponent<Rigidbody> ().AddForce (-this.transform.forward * thrust, ForceMode.VelocityChange);
		}
			this.GetComponent<Animation> ().Play ();
	} 
	
	// Update is called once per frame
	void Update () {
		BezierTime = BezierTime + Time.deltaTime;

		if (BezierTime >= 1) {
			BezierTime = 0;
		}

		if (minDistanceToEndPoint <= 1) {
			setTarget ();
		} else {
			float CurveX = (((1 - BezierTime) * (1 - BezierTime)) * start.x) + (2 * BezierTime * (1 - BezierTime) * control.x) + ((BezierTime * BezierTime) * end.x);
			float CurveY = (((1 - BezierTime) * (1 - BezierTime)) * start.y) + (2 * BezierTime * (1 - BezierTime) * control.y) + ((BezierTime * BezierTime) * end.y);
			float CurveZ = (((1 - BezierTime) * (1 - BezierTime)) * start.z) + (2 * BezierTime * (1 - BezierTime) * control.z) + ((BezierTime * BezierTime) * end.z);

			transform.position = new Vector3 (CurveX, CurveY, CurveZ);
		}

		//transform.localPosition = spline.GetPoint (progress);
	}

	void setTarget() {
		start = transform.position;


	}
}
