using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rStateMachine<T>
{
    public Robot_State<T> CurState { get; protected set; }

    private T r_sender;

    // 스테이트의 상태를 설정
    public rStateMachine(T sender, Robot_State<T> state)
    {
        r_sender = sender;
        SetState(state);
    }

    // sender가 비어있다면 리턴
    public void SetState(Robot_State<T> state)
    {
        if (r_sender == null)
        {
            //  Debug.LogError("invalid m_sender");
            return;
        }

        // curstate가 state라면? 리턴
        if (CurState == state)
        {
            //  Debug.LogWarningFormat("Already Define State - {0}", state);
            return;
        }

        if (CurState != null)
            CurState.OnExit(r_sender);

        CurState = state;

        if (CurState != null)
            CurState.OnEnter(r_sender);
    }

    public void OnFixedUpdate()
    {
        if (r_sender == null)
        {
            // Debug.LogError("invalid m_sener");
            return;
        }
        CurState.OnFixedUpdate(r_sender);
    }

    public void OnUpdate()
    {
        if (r_sender == null)
        {
            //  Debug.LogError("invalid m_sener");
            return;
        }
        CurState.OnUpdate(r_sender);
    }
}