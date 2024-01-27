using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

// 
public class Drone_Control : MonoBehaviour
{
    public Transform player; // �÷��̾� Ʈ������
    public Transform selfposition; // ��� Ʈ������
    //public float smallAgrro; // ���� ��׷� ����
    //public float largeAgrro; // ū ��׷� ����
    public float timer; // Ÿ�̸� ����
    public PlayerInput playerInput; // PlayerInput�� ������
    public bool isSpread; //�갳 ���θ� Ȯ���ϴ� bool ����

    public Transform droneTarget; //  ����� Target�� Transform�� ������ ����
    public Vector2 TargetPosition; //  Target�� Position�� ��Ÿ�� vector2 ����
    public RaycastHit2D[] Targets;  // CirclecastAll�� �������� ��� ������Ʈ���� ������ �迭
    public LayerMask Target_layer;  // �˻��� ������ layer(Enemy, ��ü���� �ش�)
    public Transform Nearest_enemy; // ĳ��Ʈ�� ������Ʈ �� ���� ����� Enemy ������Ʈ
    public float Scan_range;    //�˻� ������ ������ float ����


    public enum STATE
    {
        IDLE, // �ʱ� ����
        SPREAD, //�갳 ����
        ENGAGE,  //���� ����
        FOLLOW  //�÷��̾� ���� ����
    };

    public STATE statename; // STATE ���� (Drone_Movement ����)

    public void OnEnable()
    {
    
        // �÷��̾� ������Ʈ�� ã�� Ʈ������ �Ҵ�
        player = GameObject.Find("Player").transform;
        // PlayerInput�� ������
        playerInput = GetComponent<PlayerInput>();
        // �ڽ��� Transform ������Ʈ ������
        selfposition = GetComponent<Transform>();
        //��ĵ ���� ����
        Scan_range = 10f;

        // ������ �����ʰ� �ʱⰪ�� ����
        statename = STATE.IDLE;
        timer = 100;
        isSpread = false;
        droneTarget = null;
        Nearest_enemy = null;
 
    }//OnEnable


    public void FixedUpdate()
    {
        // �ڱ� ��ġ ����
        selfposition = GetComponent<Transform>();
        if (playerInput.special)
        {
            isSpread = true;
        }
        else
        {
            isSpread = false;
        }

        //����� Target�� ���������� �ʴٸ�
        if (droneTarget == null)
        {
            Find(); //��� �ֺ��� ���� Ž���ϴ� �Լ�
        }

        //����� Target�� �������ٸ�
        else
        {
            TargetPosition = droneTarget.position;
        }


    }//FixedUpdate

    //��� �ֺ��� ���� Ž���ϴ� �Լ�
    public void Find()
    {
        // �˻� ���̾ Enemy�� ����
        Target_layer = LayerMask.GetMask("Enemy");
        //��ó�� ��� Enemy ������Ʈ�� �˻�
        Targets = Physics2D.CircleCastAll(transform.position, Scan_range, Vector2.zero, 0, Target_layer);
        //���� ����� Enemy ������Ʈ�� Target���� ����
        Nearest_enemy = Nearest();
        if (Nearest_enemy != null)
        {
            droneTarget = Nearest_enemy;
        }//if

    }//Find


    //���� ����� ������Ʈ�� Transform�� return ���� �Լ�
    public Transform Nearest()
    {
        //���� ����� Target�� ������ ����
        Transform Result = null;

        //�� Target�� �� �ּ� �Ÿ��� ������ ����
        float distance = Scan_range * 2;

        //Targets �迭�� ����ִ� ��� ���ҿ� foreach�� ����
        foreach(RaycastHit2D Target in Targets)
        {
            //����� ��ǥ��, Target�� ��ǥ ��������
            Vector2 Drone_pos = transform.position;
            Vector2 Target_pos = Target.transform.position;

            //Distance �Լ��� ���� ��а� Target�� �Ÿ� ���
            float Cur_distance = Vector2.Distance(Drone_pos, Target_pos);

            //���� foreach���� Target���� �Ÿ�(Cur_distance)��, ����Ǿ� �ִ� distance���� ª����
            //distance�� Result�� ���� ���� �ʱ�ȭ
            if (Cur_distance < distance)
            {
                distance = Cur_distance;
                Result = Target.transform;
            }

           

        }//foreach

        

        return Result;

    }//Nearest


}