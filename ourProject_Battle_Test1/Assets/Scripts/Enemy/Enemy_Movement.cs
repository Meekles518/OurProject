using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{

    public GameObject player;

    private Rigidbody2D enemyRigidbody; // �÷��̾��� ������ٵ�

    public float moveSpeed; // �̵� �ӵ�
    public float rotateSpeed; // ȸ�� �ӵ�

    private Vector2 moveDirection; // ���ּ��� �̵��ؾ��ϴ� ����
    private Vector2 rotateDirection; // ���ּ��� ȸ���ؾ��ϴ� ����
    private float actualRotate; // ���� �������� rotateDirection�� �����ϱ� ���� ȸ���ؾ��ϴ� ����(��)
    public float x_Random;
    private float y_Random;
    private float xrandomRange = 2f;
    private float yrandomRange = 2f;
    private float currTime;

    private void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        moveSpeed = 5f;
        rotateSpeed = 100f;
        player = GameObject.Find("Player");

    }


    private void Update()
    {

        // ������ ����
        Move();
        // ȸ�� ����
        Rotate();
        currTime += Time.deltaTime;
        if(currTime >3)
        {
            x_Random = Random.Range(-xrandomRange, xrandomRange);
            y_Random = Random.Range(-yrandomRange, yrandomRange);
            currTime = 0;
        }
        
        


    }



    private void Move()
    {
        if (Vector2.Distance(transform.position, player.transform.position) > 10f)
        {
            // ������, ������ �Է°��� ���� moveDirection ����
            moveDirection = new Vector2(player.transform.position.x - transform.position.x + x_Random, player.transform.position.y - transform.position.y + y_Random);
            enemyRigidbody.velocity = moveDirection.normalized * moveSpeed;
        }
        else
        {
            enemyRigidbody.velocity = new Vector2(0, 0);
        }
    }

    // ���콺 �������� ���ּ��� ȸ��
    private void Rotate()
    {

        // ���콺 ��ǥ���� �ڽ��� ��ǥ ���� ���Ͱ� ���ϱ�
        rotateDirection = (player.transform.position - transform.position) + new Vector3(x_Random, y_Random, 0);

        // ȸ���ؾ� �ϴ� ������ ���� '��'�� ����
        actualRotate = Quaternion.FromToRotation((Vector2)transform.up, rotateDirection).eulerAngles.z;


        // ȸ���ؾ� �ϴ� ������ ���� ���� ȸ�� ������ ��������.
        if (actualRotate < 1 || actualRotate > 359)
        {
            // ������ �����ϱ� ���� ������ 1�� ����
            return;
        }

        // �ð�������� ȸ���� �� ����� �� �ð�������� ȸ��
        else if (actualRotate > 180)
        {
            transform.Rotate(0, 0, -Time.deltaTime * rotateSpeed, Space.Self);
        }

        // �ݽð�������� ȸ���� �� ����� �� �ݽð�������� ȸ��
        else if (actualRotate < 180)
        {
            transform.Rotate(0, 0, Time.deltaTime * rotateSpeed, Space.Self);
        }

    }



}