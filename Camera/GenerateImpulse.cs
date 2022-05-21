using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class GenerateImpulse : MonoBehaviour
{
    public static GenerateImpulse Instance { get; private set; }
    CinemachineImpulseSource impulse;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        impulse = transform.GetComponent<CinemachineImpulseSource>();
    }

    public void CameraShake(float shake)
    {
        impulse.GenerateImpulse(shake);
    }
}
