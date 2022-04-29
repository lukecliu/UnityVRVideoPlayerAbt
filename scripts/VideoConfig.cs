using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(menuName = "Video Config")]
public class VideoConfig : ScriptableObject
{
	[SerializeField] VideoClip videoClip;
	[SerializeField] bool clipLoopable;
	[SerializeField] AudioClip audioClip;
	[SerializeField] AudioClip audioClipLevel1;
	[SerializeField] AudioClip audioClipLevel2;
	[SerializeField] bool audioLoopable = true;
	[SerializeField] VideoClip videoClipInternal;

	public VideoClip GetVideoClip()
	{
		return videoClip;
	}

	public bool IsClipLoopable()
	{
		return clipLoopable;
	}

	public bool IsAudioLoopable()
	{
		return audioLoopable;
	}

	public AudioClip GetAudioClip()
	{
		return audioClip;
	}

	public AudioClip GetAudioClipLevel1()
	{
		return audioClipLevel1;
	}

	public AudioClip GetAudioClipLevel2()
	{
		return audioClipLevel2;
	}

	public VideoClip GetVideoClipInternal()
	{
		return videoClipInternal;
	}
	
	public bool hasInternalClip()
	{
		return videoClipInternal != null;
	}
}
