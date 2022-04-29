using UnityEngine;
using System.Collections;
using System;

public class FlyCamera : MonoBehaviour
{

	/*
    Original by by Windexglow 11-13-10
	Modified by Cil
	*/

	[SerializeField] float verticalCameraSensitive = 0.25f;
	[SerializeField] float horizontalCameraSensitive = 0.25f;
	[SerializeField] float zoomSpeed = 5f;
	[SerializeField] float panSpeed = 1f;
	[SerializeField] float maxPitchAngle = 85.0f;

	private Vector3 oldRotationMousePosition = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
	private Vector3 oldPanMousePosition = new Vector3(255, 255, 255);

	public Vector3 defaultPosition = new Vector3();
	public Vector3 defaultEulerAngle = new Vector3();

	private void Start()
	{
		defaultPosition = transform.position;
		defaultEulerAngle = transform.eulerAngles;
	}

	void Update()
	{
		TransformToPoint(Input.GetMouseButton(0) && Input.GetMouseButtonDown(1), defaultPosition, defaultEulerAngle);
		CameraRotationTransform(Input.GetMouseButtonDown(0), Input.GetMouseButton(0), Input.mousePosition, verticalCameraSensitive, horizontalCameraSensitive);
		ZoomTransform(Input.GetAxis("Mouse ScrollWheel"), zoomSpeed);
		PanTransform(Input.GetMouseButtonDown(1), Input.GetMouseButton(1), Input.mousePosition, panSpeed);

	}

	private void TransformToPoint(bool condition, Vector3 toPosition, Vector3 toEulerAngle)
	{
		if (condition)
		{
			transform.position = toPosition;
			transform.eulerAngles = toEulerAngle;
		}
	}

	private void PanTransform(bool startCondition, bool moveCondition, Vector3 newMousePosition, float panSpeed)
	{
		if (startCondition)
		{
			oldPanMousePosition = newMousePosition; // reset when we begin
		}

		if (moveCondition)
		{
			Vector3 deltaPosition = newMousePosition - oldPanMousePosition;

			//inverse movement
			Vector3 p_Velocity = new Vector3(-deltaPosition.x, -deltaPosition.y, 0);
			p_Velocity = Vector3.Normalize(p_Velocity) * panSpeed * Time.deltaTime;

			transform.Translate(p_Velocity);

			oldPanMousePosition = newMousePosition;
		}
	}

	private void ZoomTransform(float zoomAmount, float zoomSpeed)
	{
		if(zoomAmount == 0) { return;  }
		Vector3 p_Velocity = new Vector3();
		if (zoomAmount > 0f)
		{
			p_Velocity += new Vector3(0, 0, zoomSpeed * Time.deltaTime);
		}
		else {
			p_Velocity += new Vector3(0, 0, -zoomSpeed * Time.deltaTime);
		}

		//translate is default to self(local); 
		//Translate(float x, float y, float z, Space relativeTo = Space.Self);
		//Space.World to world space
		//other transform to other space
		transform.Translate(p_Velocity);
	}

	private void CameraRotationTransform(bool startCondition, bool moveCondition, 
		Vector3 newMousePosition, float verticalSensitive, float horizontalSensitive)
	{
		if (startCondition)
		{
			oldRotationMousePosition = newMousePosition; // reset when we begin
		}
			
		if (moveCondition)
		{
			Vector3 deltaPosition = newMousePosition - oldRotationMousePosition;
			//Debug.Log(oldMousePosition);
			//verital pitch => rotate along x-axis
			//horizontal yaw => rotate along y-axis
			//pan roll => rotate along z-axis
			float pitch = transform.eulerAngles.x - deltaPosition.y * verticalSensitive;
			if(pitch > 180)
			{
				pitch -= 360;
			}
			pitch = Mathf.Clamp(pitch, -maxPitchAngle, maxPitchAngle);
			float yaw = transform.eulerAngles.y + deltaPosition.x * horizontalSensitive;
			float roll = 0f;
			Vector3 rotationTransform = new Vector3(pitch, yaw, roll);
			//Debug.Log(rotationTransform);
			transform.eulerAngles = rotationTransform;
			oldRotationMousePosition = newMousePosition; 
		}
	}

}


