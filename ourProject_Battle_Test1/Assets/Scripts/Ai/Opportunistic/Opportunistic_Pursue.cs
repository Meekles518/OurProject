using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// ��ȸ���������� �÷��̾ �����ϴ� ����
public class Opportunistic_Pursue : Opportunistic_State
{
    public Opportunistic_Pursue(GameObject _enemy, Transform _player, Enemy_Control _control)
        : base(_enemy, _player, _control)
    {

        name = STATE.PURSUE;
    }

    public override void Enter()
    {
        Debug.Log("Pursue");
        base.Enter();
    }

    public override void FixedUpdate()
    {
        // ��� ����
        control.isShoot = true;

        if (!Aggro())
        {
            GameManager.instance.isNotOppEngage &= true;
        }

        // ������ ��׷ΰ� �������� �ʰ� ���������� ��׷ΰ� �������� ���� ��
        if ((!Aggro() && !GameManager.instance.isDefensiveEngage))
        {
            // �÷��̾ �ڽ��� ū ��׷� ���� ���� ���ٸ�
            if (control.PlayertoFleetSpawn > control.largeAgrro)
            {
                // ���� ������Ʈ�� Retreat�� ����
                nextState = new Opportunistic_Retreat(enemy, player, control);
                stage = EVENT.EXIT;

            }
        }
    }
    public override void Exit()
    {


        base.Exit();
    }
}
