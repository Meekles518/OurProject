using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Defensive_Idle : Defensive_State
{

    public Defensive_Idle(GameObject _enemy, Transform _player)
        : base(_enemy, _player)
    {

        name = STATE.IDLE;
    }

    public override void Enter()
    {

        base.Enter();
    }

    public override void Update()
    {

        if (Aggro())
        {
            Debug.Log("attack");
            //nextState = new Pursue(enemy, player);
            stage = EVENT.EXIT;
        }
        
    }

    public override void Exit()
    {


        base.Exit();
    }
}