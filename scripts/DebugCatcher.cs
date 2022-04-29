using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DebugCatcher : MonoBehaviour
{
    [SerializeField] GameObject ovrCameraRig;
    [SerializeField] OVRInput.Button toggleButtonforVR;
    [SerializeField] OVRInput.Controller toggleControllerForVR;
    [SerializeField] KeyCode altToggleKeyCode = KeyCode.LeftShift;

    [Header("Debug Output Port")]
    [SerializeField] Vector3 displayLocationRefCamera = new Vector3(0, 0, 0.3f);
    [SerializeField] GameObject displayCanvas;
    [SerializeField] TextMeshProUGUI displayLogText;
    [SerializeField] int maxNumberOfItems = 10;

    List<string> debugLogItemList = new List<string>();
    bool isListDirty = false;

    // Start is called before the first frame update
    void Start()
    {
        if (ovrCameraRig == null)
        {
            ovrCameraRig = GameObject.Find("OVRCameraRig");
        }

        Application.logMessageReceived += ReceiveLog;
        displayLogText.text = "";
        ActiveCanvas(false);
    }

	// Update is called once per frame
	void Update()
    {
        if (OVRInput.GetDown(toggleButtonforVR, toggleControllerForVR) || Input.GetKeyDown(altToggleKeyCode))
        {
            ActiveCanvas(!displayCanvas.activeInHierarchy);
        }

		if (displayCanvas.activeInHierarchy && isListDirty)
		{
            UpdateDisplayPort();
            isListDirty = false;
        }
    }


    //condition: Log contexts
    //stackTrace: whe logged script Location
    //type: LogType => Error, Assert, Warning, Log, Exception
    //DO NOT WRITE LOG IN THIS METHOD! Otherwise will call Infinite Application.logMessageReceived Events

    private void ReceiveLog(string condition, string stackTrace, LogType type)
    {
        string formatedItem = string.Format("{0} {1}", getSystemTimeString(), condition);
        debugLogItemList.Add(formatedItem);
        isListDirty = true;
    }

	private void UpdateDisplayPort()
	{
        //Empty List
        if(debugLogItemList.Count <= 0)
		{
            displayLogText.text = "";
            return;
        }

        string result = "";
        int endIndex = debugLogItemList.Count-1; //include
        int beginIndex = endIndex - maxNumberOfItems; //Include
        if(beginIndex < 0)
		{
            beginIndex = 0;
		}

        for (int i = beginIndex; i<= endIndex; i++)
		{
            result += debugLogItemList[i] + Environment.NewLine;
        }

        displayLogText.text = result;
    }

    private string getSystemTimeString()
	{
        return string.Format("[{0}:{1}:{2}]", 
            System.DateTime.Now.Hour, System.DateTime.Now.Minute, System.DateTime.Now.Second);
    }

    private void AdjustPosition()
    {
        transform.SetParent(ovrCameraRig.transform.Find("TrackingSpace/CenterEyeAnchor"));


        transform.localPosition = displayLocationRefCamera;
        transform.localRotation = Quaternion.Euler(Vector3.zero);

        Vector3 originalPos = transform.position;
        Quaternion originalRot = transform.rotation;
        //Setting the z rotation as 0 to adjust the debug console's orientation so that the debug console stays readable.
        originalRot = Quaternion.Euler(originalRot.eulerAngles.x, originalRot.eulerAngles.y, 0.0f);

        transform.SetParent(null);

        transform.position = originalPos;
        transform.rotation = originalRot;
    }

    private void ActiveCanvas(bool v)
    {
        displayCanvas.SetActive(v);
        if (v)
        {
            AdjustPosition();
        }
    }
}
