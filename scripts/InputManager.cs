using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputManager : MonoBehaviour
{
    [SerializeField] ScenarioManager sceneManager;
    [SerializeField] FlyCamera cam;

    // Start is called before the first frame update
    void Start()
    {

        if (sceneManager == null)
        {
            sceneManager = FindObjectOfType<ScenarioManager>();
        }

        sceneManager.setScenarioAdvancable(true);
        SetCameraMovement(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
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

  
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetMouseButtonDown(2))
		{
            ToggleScenarioAdancableButton();
        }

        if (sceneManager.IsScenarioAdvancable())
		{
            ControlAdvanceScenario();
        }
    }

	private void ToggleScenarioAdancableButton()
	{
        sceneManager.setScenarioAdvancable(!sceneManager.IsScenarioAdvancable());
        SetCameraMovement(!sceneManager.IsScenarioAdvancable()); //always inverse of scene manager
    }

	public void ControlAdvanceScenario()
	{
        if (sceneManager.IsSpeedChangable())
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                Debug.Log("Add Speed");
                sceneManager.AddSpeed(1);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                Debug.Log("Decrease Speed");
                sceneManager.AddSpeed(-1);
            }
        }


        if (Input.GetMouseButtonDown(0))
        {
            //LeftMouse
            Debug.Log("Show Next Script Text");
            sceneManager.ShowNextScriptText();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            //RightMouse
            Debug.Log("To Next Scenario");
            sceneManager.ToNextScenario();
        }
    }

    private void SetCameraMovement(bool b)
    {
        Debug.Log("Set Camera Movement " + b);
        cam.enabled = b;
    }

}
