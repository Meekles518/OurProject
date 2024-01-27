using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone_Engage : Drone_State
{
    //����� Engage ���¸� ������ Script
    public Drone_Engage(GameObject _enemy, Transform _player, Drone_Control _control, float _currTime)
        : base(_enemy, _player, _control, _currTime)
    {
        name = STATE.ENGAGE;
    }

}
