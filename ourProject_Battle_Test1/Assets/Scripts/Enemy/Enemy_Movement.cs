using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    //�ڵ� ���Ÿ� ���� ����

    //�ڵ� ������ �� Ž�� ����
    public float Scan_range;

    //CircleCastAll �Լ�����, Ž���� Layer�� ������ ����
    public LayerMask Target_layer;

    public Collider2D Target;

    private Rigidbody2D enemyRigidbody; // �÷��̾��� ������ٵ�

    public float moveSpeed; // �̵� �ӵ�
    public float rotateSpeed; // ȸ�� �ӵ�

    private Vector2 moveDirection; // ���ּ��� �̵��ؾ��ϴ� ����
    private Vector2 rotateDirection; // ���ּ��� ȸ���ؾ��ϴ� ����
    private float actualRotate; // ���� �������� rotateDirection�� �����ϱ� ���� ȸ���ؾ��ϴ� ����(��)

    private void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        Scan_range = 30;
        moveSpeed = 5f;
        rotateSpeed = 100f;

    }


    private void Update()
    {

        //���� ������ �ֺ��� Cast�ϴ� �Լ�(��ĵ ��ġ, ���� ������, ��ĵ ����, ��ĵ �Ÿ�, Layermask)
        //�ֺ����� Scan����, ��ǥ ����� ������ Targets List�� �ֱ�?
        Target = Physics2D.OverlapCircle(transform.position, Scan_range, Target_layer);
        // ������ ����

        // ������ ����
        Move();
        // ȸ�� ����
        Rotate();

        //move towards the player
        /*if (Vector2.Distance(transform.position, Target.transform.position) > 4f)
        {//move if distance from target is greater than 1
            //transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0, 0));
            moveDirection = new Vector2(Target.transform.position.x - transform.position.x, Target.transform.position.y - transform.position.y);
            enemyRigidbody.AddForce(moveDirection.normalized * moveSpeed);
        }*/
  


    }

    private void Move()
    {
        if (Vector2.Distance(transform.position, Target.transform.position) > 10f)
        {
            // ������, ������ �Է°��� ���� moveDirection ����
            moveDirection = new Vector2(Target.transform.position.x - transform.position.x, Target.transform.position.y - transform.position.y);
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
        rotateDirection = Target.transform.position - transform.position;

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