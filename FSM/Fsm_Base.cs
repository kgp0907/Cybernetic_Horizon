using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어와 적의 스테이트 설정 및 변경시 호출되는 메서드
/// </summary>
/// <typeparam name="T"></typeparam>

public class Fsm_Base<T> : MonoBehaviour
{
    public Base_Interface<T> CurState { get; protected set; }

    private T fsm_sender;

    // 현재 스테이트의 상태를 정의한다.  
    public Fsm_Base(T sender, Base_Interface<T> state)
    {
        fsm_sender = sender;
        SetState(state);
    }

    // 스테이트를 변경할때 호출되는 함수.
    public void SetState(Base_Interface<T> state)
    {
        //현재 스테이트가 설정되지 않았으면 리턴한다.
        if (fsm_sender == null)
        {
            return;
        }

        // 현재 스테이트와 동일한 스테이트를 호출 할 경우 리턴한다.
        if (CurState == state)
        {
            return;
        }
        // 현재 스테이트가 null이 아니라면 상태이탈시 OnExit문을 실행한다.
        if (CurState != null)
            CurState.OnExit(fsm_sender);

        CurState = state;
        // 현재 스테이트가 null이 아니라면 상태진입시 OnEnter문을 실행한다.
        if (CurState != null)
            CurState.OnEnter(fsm_sender);
    }

    public void OnFixedUpdate()
    {
        if (fsm_sender == null)
        {
            return;
        }
        CurState.OnFixedUpdate(fsm_sender);
    }

    public void OnUpdate()
    {
        //스테이트가 null이 아닐경우 해당 스테이트의 Update를 실행한다.
        if (fsm_sender == null)
        {
            return;
        }
        CurState.OnUpdate(fsm_sender);
    }
}
