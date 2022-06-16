using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
/// <summary>
/// Ÿ�Ӷ��� ȣ�� ��ũ��Ʈ
/// </summary>
public class TimeLineManager : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public TimelineAsset timeLine;
  
    public void Play()
    {
        playableDirector.Play();
    }

    public void PlayFromTimeLine()
    {
        playableDirector.Play(timeLine);
    }
}
