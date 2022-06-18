using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCoroutine : MonoBehaviour
{
    // waitforseconds�� ���� ����� ��
    public static IEnumerator Wait(float duration)
    {
        float elapsed = 0.0f;
        while (true)
        {
            elapsed += Time.deltaTime;
            if (elapsed >= duration)
            {
                break;
            }
            yield return null;
        }
    }

    // waituntil�� ���� �������
    public static IEnumerator WaitUntil(string AnimationName,Animator animator, float AnimationProgress)
    {
        while (true)
        {
            if(animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationName) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= AnimationProgress)
            {
                break;
            }
            yield return null;
        }
    }
}