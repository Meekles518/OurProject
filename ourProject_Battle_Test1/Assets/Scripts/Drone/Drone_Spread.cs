using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone_Spread : Drone_State
{
    //����� Spread ���¸� ������ Script
    public Drone_Spread(GameObject _enemy, Transform _player, Drone_Control _control, float _currTime)
        : base(_enemy, _player, _control, _currTime)
    {
        name = STATE.SPREAD;
    }



}
