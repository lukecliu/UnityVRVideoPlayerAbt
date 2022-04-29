using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class VRInputManager : MonoBehaviour
{

	[SerializeField] VRRigMover rigMover;
	
	[Header("Reset Button")]
	[SerializeField] OVRInput.Button resetButton;
	[SerializeField] OVRInput.Controller resetButtonController;
	[SerializeField] KeyCode altResetDefaultPositionKeyCode;

	[Header("Move Button")]
	[SerializeField] GameObject handAnchor;
	[SerializeField] OVRInput.Button moverButton;
	[SerializeField] OVRInput.Controller moverButtonController;
	[SerializeField] KeyCode altMoverButtonKeyCode;

	[Header("Horizontal Rotation Dpad")]
	[SerializeField] float positiveYawAngle = 5f;
	[SerializeField] OVRInput.Button rotateLeftButton;
	[SerializeField] OVRInput.Controller rotateLeftButtonController;
	[SerializeField] KeyCode altRotateLeftButtonKeyCode;
	[SerializeField] OVRInput.Button rotateRightButton;
	[SerializeField] OVRInput.Controller rotateRightButtonController;
	[SerializeField] KeyCode altRotateRightButtonKeyCode;

	[Header("Scene Manager VR")]
	[SerializeField] ScenarioManagerVR sceneManagerVR;
	[SerializeField] OVRInput.Button advSceneButton;
	[SerializeField] OVRInput.Controller advSceneController;
	[SerializeField] KeyCode altAdvSceneKeyCode;
	[SerializeField] OVRInput.Button advTextButton;
	[SerializeField] OVRInput.Controller advTextController;
	[SerializeField] KeyCode altAdvTextKeyCode;
	[SerializeField] OVRInput.Button speedUpButton;
	[SerializeField] OVRInput.Controller speedUpController;
	[SerializeField] KeyCode altSpeedUpKeyCode;
	[SerializeField] OVRInput.Button speedDownButton;
	[SerializeField] OVRInput.Controller speedDownController;
	[SerializeField] KeyCode altSpeedDownKeyCode;
	[SerializeField] OVRInput.Button toggleInternalButton;
	[SerializeField] OVRInput.Controller toggleInternalController;
	[SerializeField] KeyCode toggleInternalKeyCode;

	[Header("Application Quit")]
	[SerializeField] KeyCode altAppQuitKeyCode = KeyCode.Escape;

	private void Start()
	{
		if(rigMover == null)
		{
			rigMover = FindObjectOfType<VRRigMover>();
		}

		if (sceneManagerVR == null)
		{
			sceneManagerVR = FindObjectOfType<ScenarioManagerVR>();
		}

	}

	void Update()
	{
		ApplicationQuitControl();
		ControlRigMovement();
		ControlAdvanceScenario();
	}

	private void ApplicationQuitControl()
	{
		if (Input.GetKeyDown(altAppQuitKeyCode))
		{
			Debug.Log("Quit Application...");
#if UNITY_EDITOR
			// Application.Quit() does not work in the editor so
			// UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
			UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
		}
	}

	private void ControlRigMovement()
	{
		//Debug use
		KeyboardDebugHandAnchorMove();

		rigMover.ResetDefaultPosition(
			OVRInput.GetDown(resetButton, resetButtonController) || Input.GetKeyDown(altResetDefaultPositionKeyCode));

		rigMover.rigMoveInverse(
			OVRInput.GetDown(moverButton, moverButtonController) || Input.GetKeyDown(altMoverButtonKeyCode),
			OVRInput.Get(moverButton, moverButtonController) || Input.GetKey(altMoverButtonKeyCode),
			rigMover.transform.InverseTransformPoint(handAnchor.transform.position));

		rigMover.rigHorizontalRotate(
			OVRInput.GetDown(rotateLeftButton, rotateLeftButtonController) || Input.GetKeyDown(altRotateLeftButtonKeyCode),
			OVRInput.GetDown(rotateRightButton, rotateRightButtonController) || Input.GetKeyDown(altRotateRightButtonKeyCode),
			positiveYawAngle);
	}

	private void KeyboardDebugHandAnchorMove()
	{
		if (Input.GetKey(altMoverButtonKeyCode))
		{
			if (Input.GetKeyDown(KeyCode.A))
			{
				handAnchor.transform.Translate(Vector3.left * 0.1f, rigMover.transform);
			}
			if (Input.GetKeyDown(KeyCode.D))
			{
				handAnchor.transform.Translate(Vector3.right * 0.1f, rigMover.transform);
			}
			if (Input.GetKeyDown(KeyCode.W))
			{
				handAnchor.transform.Translate(Vector3.forward * 0.1f, rigMover.transform);
			}
			if (Input.GetKeyDown(KeyCode.S))
			{
				handAnchor.transform.Translate(Vector3.back * 0.1f, rigMover.transform);
			}
			if (Input.GetKeyDown(KeyCode.E))
			{
				handAnchor.transform.Translate(Vector3.up * 0.1f, rigMover.transform);
			}
			if (Input.GetKeyDown(KeyCode.Q))
			{
				handAnchor.transform.Translate(Vector3.down * 0.1f, rigMover.transform);
			}
		}
	}

	public void ControlAdvanceScenario()
	{
		//speed input
		if (sceneManagerVR.IsSpeedChangable())
		{
			if (OVRInput.GetDown(speedUpButton, speedUpController) 
				|| Input.GetKeyDown(altSpeedUpKeyCode))
			{
				Debug.Log("Add Speed");
				sceneManagerVR.AddSpeed(1);
			}
			else if (OVRInput.GetDown(speedDownButton, speedDownController)
				|| Input.GetKeyDown(altSpeedDownKeyCode))
			{
				Debug.Log("Decrease Speed");
				sceneManagerVR.AddSpeed(-1);
			}
		}

		//text adv
		if (OVRInput.GetDown(advTextButton, advTextController)
				|| Input.GetKeyDown(altAdvTextKeyCode))
		{
			Debug.Log("Show Next Script Text");
			sceneManagerVR.ShowNextScriptText();
		}
		
		//scene adv
		if (OVRInput.GetDown(advSceneButton, advSceneController)
				|| Input.GetKeyDown(altAdvSceneKeyCode))
		{
			Debug.Log("To Next Scenario");
			sceneManagerVR.ToNextScenario();
		}

		//internal toggle
		if (OVRInput.GetDown(toggleInternalButton, toggleInternalController)
				|| Input.GetKeyDown(toggleInternalKeyCode))
		{
			Debug.Log("Toggle Internal");
			sceneManagerVR.ToggleInternalView();
		}
	}
}


