using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾�� ���� ������Ʈ ���� �� ����� ȣ��Ǵ� �޼���
/// </summary>
/// <typeparam name="T"></typeparam>

public class Fsm_Base<T> : MonoBehaviour
{
    public Base_Interface<T> CurState { get; protected set; }

    private T fsm_sender;

    // ���� ������Ʈ�� ���¸� �����Ѵ�.  
    public Fsm_Base(T sender, Base_Interface<T> state)
    {
        fsm_sender = sender;
        SetState(state);
    }

    // ������Ʈ�� �����Ҷ� ȣ��Ǵ� �Լ�.
    public void SetState(Base_Interface<T> state)
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
