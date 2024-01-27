using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone_Engage : Drone_State
{
    //드론의 Engage 상태를 정의할 Script
    public Drone_Engage(GameObject _enemy, Transform _player, Drone_Control _control, float _currTime)
        : base(_enemy, _player, _control, _currTime)
    {
        name = STATE.ENGAGE;
    }

}
