using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone_Idle : Drone_State
{
    //����� �ʱ� �⺻ Idle ���¸� ������ Script
    public Drone_Idle(GameObject _enemy, Transform _player, Drone_Control _control, float _currTime)
        : base(_enemy, _player, _control, _currTime)
    {
        name = STATE.IDLE;
    }


    //Idle State ���� ��
    public override void Enter()
    {
        currTime = 0f;
        Debug.Log("Idle");
        base.Enter();
    }//Enter

    // Idle ������ ��
    public override void FixedUpdate()
    {
        //�갳 ���¶��
        if (control.isSpread)
        {
            //���� State�� Spread�� ����
            nextState = new Drone_Spread(enemy, player, control, currTime);
            stage = EVENT.EXIT;

        }
        //�갳 ���°� �ƴϰ�, �ڵ� ����̰�, ����� Target�� �����Ǿ� �ִٸ�
        else if (control.playerInput.auto && control.TargetPosition != null)
        {
            //���� State�� Engage�� ����
            nextState = new Drone_Engage(enemy, player, control, currTime);
            stage = EVENT.EXIT;

        }


        base.FixedUpdate();
    }//FixedUpdate


    //State ���� �� 
    public override void Exit()
    {
        base.Exit();
    }//Exit

}