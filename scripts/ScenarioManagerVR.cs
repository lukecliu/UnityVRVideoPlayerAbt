using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScenarioManagerVR : MonoBehaviour
{
    [SerializeField] Sherry sherry;
    [SerializeField] ScenarioConfig startScenario;
    [SerializeField] TextMeshProUGUI displayInputHintArea;
    [SerializeField] string hintFormat;
    [SerializeField] TextMeshProUGUI displayScriptArea;

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
        if (scenario.IsResetParameters())
        {
            AddSpeed(0);
            ChangeSherryExcitment(0);
        }

        currScenario = scenario;
        currScripts = scenario.GetTextScriptConfig();
        currToAnimationStateName = scenario.GetToAnimationStateName();
        nextScenario = scenario.GetNextScenario();

        ChangeSherryExcitment(currScenario.GetExcitmentLevel());
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

    private void ChangeSherryExcitment(int lv)
    {
        sherry.setExcitmentLevel(lv);
    }

    public void ToggleInternalView()
	{
        sherry.ToggleInternalView();
	}
}
