using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugInputManager : MonoBehaviour
{
    [SerializeField] GameObject rig;
    [SerializeField] GameObject centerEyeAnchor;
    [SerializeField] GameObject leftControllerAnchor;
    [SerializeField] GameObject rightControllerAnchor;

    [Header("Debug Button")]
    [SerializeField] OVRInput.Controller printController;
    [SerializeField] OVRInput.Button printTransform;
    [SerializeField] KeyCode altPrintTransformKeyCode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(altPrintTransformKeyCode) || OVRInput.GetDown(printTransform, printController))
		{
            //printLocation(player);
            printLocation(rig);
            printLocation(centerEyeAnchor);
            printLocation(leftControllerAnchor);
            printLocation(rightControllerAnchor);
        }
    }

    private void printLocation(GameObject obj)
	{
        Debug.Log(string.Format("Name:{0}, Pos:{1}, Rot:{2}, LoPos:{3}, LoRot:{4}", 
            obj.name, 
            obj.transform.position,
            obj.transform.eulerAngles,
            obj.transform.localPosition,
            obj.transform.localEulerAngles
            ));
	}
}
