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
    public float smallAgrro; // ���� ��׷� ����
    public float largeAgrro; // ū ��׷� ����
    public float timer; // Ÿ�̸� ����
    private PlayerInput playerInput; // PlayerInput�� ������
    public bool isSpread;

    public enum STATE
    {
        IDLE, // �ʱ� ����
        SPREAD,
        ENGAGE
    };

    public STATE statename; // STATE ���� (Drone_Movement ����)

    public void OnEnable()
    {
        // �÷��̾� ������Ʈ�� ã�� Ʈ������ �Ҵ�
        player = GameObject.Find("Player").transform;
        // PlayerInput�� ������
        playerInput = GetComponent<PlayerInput>();
        // �ڱ� ��ġ �ʱ�ȭ
        selfposition = GetComponent<Transform>();
        // ��׷� ���� ���� (�̰Ÿ� ����� ����Ƽ �ν����Ϳ��� �ǽð����� ���� ���뺰 ��������)
        smallAgrro = 15f;
        largeAgrro = 25f;
        // ������ �����ʰ� �ʱⰪ�� ����
        statename = STATE.IDLE;
        timer = 100;
        isSpread = false;
 
    }


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

    }
}