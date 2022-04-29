using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRRigMover : MonoBehaviour
{
    /*
    Original by Windexglow 11-13-10 FlyCamera
	Modified by Cil for VR Grip Movement
	up/down, forward/backward translation

	*/

    private Vector3 defaultRigPosition;
    private Vector3 defaultRigEularAngle;
    private Vector3 oldControllerPosition = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
		this.defaultRigPosition = Vector3.zero + this.transform.position;
		this.defaultRigEularAngle = Vector3.zero + this.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDefaultPosition(Vector3 pos, Vector3 eular)
	{
		this.defaultRigPosition = Vector3.zero + pos;
		this.defaultRigEularAngle = Vector3.zero + eular;
    }

    public void ResetDefaultPosition(bool condition)
    {
        if (condition)
        {
            this.transform.position = Vector3.zero + this.defaultRigPosition;
            this.transform.eulerAngles = Vector3.zero + this.defaultRigEularAngle;
            Debug.Log("Reset Rig Transform");
        }
    }

	public void rigMoveInverse(bool startCondition, bool moveCondition, Vector3 newControllerPosition)
	{
		if (startCondition)
		{
			this.oldControllerPosition = newControllerPosition; // reset when we begin
		}

		if (moveCondition)
		{
			Vector3 deltaPosition = newControllerPosition - this.oldControllerPosition;

			if (deltaPosition.sqrMagnitude < Mathf.Epsilon) { return; }
			Debug.Log(string.Format("Controller delat pos: {0}",deltaPosition));

			//inverse movement
			this.transform.Translate(-deltaPosition);

			this.oldControllerPosition = newControllerPosition;
		}
	}

	public void rigHorizontalRotate(bool leftCondition, bool rightCondition, float positiveYawAngle)
	{

		if (leftCondition || rightCondition)
		{
			//verital pitch => rotate along x-axis
			//horizontal yaw => rotate along y-axis
			//pan roll => rotate along z-axis

			float yaw = 0f;
			if (leftCondition)
			{
				yaw += -positiveYawAngle;
			}
			if (rightCondition)
			{
				yaw += positiveYawAngle;
			}

			Debug.Log(string.Format("yaw: {0}", yaw));
			this.transform.Rotate(0f, yaw, 0f);
		}
	}

}
