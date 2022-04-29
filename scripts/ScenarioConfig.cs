using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scenario Config")]
public class ScenarioConfig : ScriptableObject
{
	[SerializeField] string description;

	[Header("Text Area Configs")]
	[SerializeField] TextConfig scripts;

	[Header("Animation")]
	[SerializeField] string toAnimationState;
	[SerializeField] bool speedChangable;
	[SerializeField] int minSpeed;
	[SerializeField] int maxSpeed;
	[SerializeField] int excitmentLevel = 0;

	[Header("Next Scenario")]
	[SerializeField] string nextScenarioActionHint;
	[SerializeField] ScenarioConfig nextScenario;
	[SerializeField] bool resetParameters = false;

	public TextConfig GetTextScriptConfig()
	{
		return scripts;
	}

	public string GetToAnimationStateName()
	{
		return toAnimationState;
	}

	public ScenarioConfig GetNextScenario()
	{
		return nextScenario;
	}

	public bool IsSpeedChangable()
	{
		return speedChangable;
	}

	public int GetMinSpeed()
	{
		return minSpeed;
	}
	public int GetMaxSpeed()
	{
		return maxSpeed;
	}

	public string GetNextScenarioActionHint()
	{
		return nextScenarioActionHint;
	}

	public bool IsResetParameters()
	{
		return resetParameters;
	}

	public int GetExcitmentLevel()
	{
		return excitmentLevel;
	}
}
