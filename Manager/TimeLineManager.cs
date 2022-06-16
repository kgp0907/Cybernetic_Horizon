using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
/// <summary>
/// 타임라인 호출 스크립트
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
