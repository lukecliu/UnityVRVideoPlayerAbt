using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScenarioManager : MonoBehaviour
{
    [SerializeField] Sherry sherry;
    [SerializeField] ScenarioConfig startScenario;
    [SerializeField] TextMeshProUGUI displayInputHintArea;
    [SerializeField] string hintFormat;
    [SerializeField] TextMeshProUGUI displayScriptArea;

    [Header("Display Button")]
    [SerializeField] bool isScenarioAdvancable;
    [SerializeField] TextMeshProUGUI buttonScenarioText;
    [SerializeField] Button buttonScenario;
    [SerializeField] string scenarioAdvTrueText = "O";
    [SerializeField] string scenarioAdvFalseText = "X";
    [SerializeField] Color scenarioAdvTrueColor = Color.green;
    [SerializeField] Color scenarioAdvFalseColor = Color.red;

    [Header("DEBUG")]
    [SerializeField] float currentSpeed;
    [SerializeField] int currScriptIndex;
    [SerializeField] TextConfig currScripts;
    [SerializeField] string currToAnimationStateName;
    [SerializeField] ScenarioConfig currScenario;
    [SerializeField] ScenarioConfig nextScenario;


	//const float MIN_SPEED = 0f;
    //const float MAX_SPEED = 6f;

    // Start is called before the first frame update
    void Start()
    {
        if (sherry == null)
        {
            sherry = FindObjectOfType<Sherry>();
        }

        PlayScenario(startScenario);
    }

	private void PlayScenario(ScenarioConfig scenario)
	{
        currScenario = scenario;
        currScripts = scenario.GetTextScriptConfig();
        currToAnimationStateName = scenario.GetToAnimationStateName();
        nextScenario = scenario.GetNextScenario();

        PlayScripts();
        PlayAnimation();
    }

	private void PlayScripts()
	{
        ResetScriptIndex();
        ShowHintText();
        ShowNextScriptText();
    }

    private void ShowHintText()
    {
        displayInputHintArea.text = string.Format(hintFormat, currScenario.GetNextScenarioActionHint());
    }

    public void ShowNextScriptText()
    {
        if (currScriptIndex + 1 > currScripts.GetScriptSize() - 1) { return; }

        currScriptIndex++;
        string text = currScripts.GetScript(currScriptIndex);
        if (currScriptIndex >= currScripts.GetScriptSize() - 1)
        {
            text += Environment.NewLine + Environment.NewLine + string.Format("Åm{0}Ån", currScenario.GetNextScenarioActionHint());
        }
        displayScriptArea.text = text;
    }

    private void ResetScriptIndex()
    {
        currScriptIndex = -1;
    }

    private void PlayAnimation()
    {
        sherry.GetComponent<Animator>().Play(currToAnimationStateName);
    }	

	public void ToNextScenario()
	{
        if(nextScenario == null) { return; }
        PlayScenario(nextScenario);
	}

	public bool IsSpeedChangable()
    {
        return currScenario.IsSpeedChangable();
    }

    public void AddSpeed(int s)
    {
        currentSpeed = Mathf.Clamp(currentSpeed + s, currScenario.GetMinSpeed(), currScenario.GetMaxSpeed());
        sherry.GetComponent<Animator>().SetFloat("Speed", currentSpeed);
    }

    public void setScenarioAdvancable(bool b)
	{
        Debug.Log("Set Scenario Advancable " + b);
        isScenarioAdvancable = b;

        //Text
        buttonScenarioText.text = b ? scenarioAdvTrueText : scenarioAdvFalseText;

        //Colors is struct
        ColorBlock bscb = buttonScenario.GetComponent<Button>().colors;
        bscb.normalColor = b ? scenarioAdvTrueColor : scenarioAdvFalseColor;
        bscb.selectedColor = b ? scenarioAdvTrueColor : scenarioAdvFalseColor;
        buttonScenario.GetComponent<Button>().colors = bscb;
    }

    public bool IsScenarioAdvancable()
	{
        return isScenarioAdvancable;
    }
}
