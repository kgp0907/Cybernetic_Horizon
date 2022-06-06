using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone_State_Idle : Interface_Base<Drone>
{
    public void OnEnter(Drone drone)
    {

    }

    public void OnUpdate(Drone drone)
    {
        if (Input.GetKeyDown("o"))
        {
            drone.ChangeState(Drone.dState.Fire);
        }
    }

    public void OnFixedUpdate(Drone sender)
    {

    }
    public void OnExit(Drone drone)
    {

    }

   
}
