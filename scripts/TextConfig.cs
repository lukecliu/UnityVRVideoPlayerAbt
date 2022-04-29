using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Text Config")]
public class TextConfig : ScriptableObject
{
	[SerializeField] string descrpition;
	[SerializeField] [TextArea(3,10)] string[] scripts;

	public string GetScript(int index)
	{
		if(index < 0) { return null; }
		if(index > GetScriptSize()-1) { return null; }
		return scripts[index];
	}

	public int GetScriptSize()
	{
		return scripts.Length;
	}
}
