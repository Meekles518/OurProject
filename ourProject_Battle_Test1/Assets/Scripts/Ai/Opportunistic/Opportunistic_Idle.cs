using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// ��ȸ������ ���� �ʱ� ������ġ�� �ִ� ����
public class Opportunistic_Idle : Opportunistic_State
{
    public Opportunistic_Idle(GameObject _enemy, Transform _player, Enemy_Control _control)
        : base(_enemy, _player, _control)
    {
        // ���� State�� �̸��� IDLE�� ����
        name = STATE.IDLE;
    }

    // Idle ���Խ�
    public override void Enter()
    {
        GameManager.instance.isNotOppEngage &= true;
        Debug.Log("Idle");
        base.Enter();
    }

    // Idle ������ ��
    public override void FixedUpdate()
    {
        // ��� �Ұ�
        control.isShoot = false;

        // ���������� ��׷ΰ� �����ų� ������ ��׷ΰ� ���ȴٸ�
        if (GameManager.instance.isDefensiveEngage || Aggro() || GameManager.instance.isNotOppEngage == false)
        {
            if (Aggro())
            {
                GameManager.instance.isNotOppEngage &= false;
            }
            // ���� ������Ʈ�� Pursue�� ����
            nextState = new Opportunistic_Pursue(enemy, player, control);
            // ���� �̺�Ʈ�� Exit���� ����
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {


        base.Exit();
    }
}
