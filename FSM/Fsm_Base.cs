using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
/// <summary>
/// �÷��̾�� ���� ������Ʈ ���� �� ����� ȣ��Ǵ� �޼���
/// </summary>
/// <typeparam name="T"></typeparam>

public class Fsm_Base<T> : MonoBehaviour
{
    public Interface_Base<T> CurState { get; set; }

    private T fsm_sender { get; set; }
    public string animation_id;

    public Animator m_Animator;
    public bool AnimationName => m_Animator.GetCurrentAnimatorStateInfo(0).IsName(animation_id);
    public float AnimationProgress => m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

    // ���� ������Ʈ�� ���¸� �����Ѵ�.  
    public virtual void First_State(T sender, Interface_Base<T> state)
    {
        fsm_sender = sender;
        SetState(state);
    }

    // ������Ʈ�� �����Ҷ� ȣ��Ǵ� �Լ�.
    public void SetState(Interface_Base<T> state)
    {
        //���� ������Ʈ�� �������� �ʾ����� �����Ѵ�.
        if (fsm_sender == null)
        {
            return;
        }

        // ���� ������Ʈ�� ������ ������Ʈ�� ȣ�� �� ��� �����Ѵ�.
        if (CurState == state)
        {
            return;
        }
        // ���� ������Ʈ�� null�� �ƴ϶�� ������Ż�� OnExit���� �����Ѵ�.
        if (CurState != null)
            CurState.OnExit(fsm_sender);

        CurState = state;
        // ���� ������Ʈ�� null�� �ƴ϶�� �������Խ� OnEnter���� �����Ѵ�.
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
        //������Ʈ�� null�� �ƴҰ�� �ش� ������Ʈ�� Update�� �����Ѵ�.
        if (fsm_sender == null)
        {
            return;
        }
        CurState.OnUpdate(fsm_sender);
    }
}
