using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Idle : DState<Drone>
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

    public void OnExit(Drone drone)
    {

    }

    public void OnFixedUpdate(Drone drone)
    {

    }
}
