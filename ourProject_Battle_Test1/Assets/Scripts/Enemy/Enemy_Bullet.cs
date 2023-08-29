using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// ������ �Ѿ��� �ൿ�� ����
public class Enemy_Bullet : MonoBehaviour
{
    private Rigidbody2D rb2; // �Ѿ��� ������ٵ�
    public GameObject enemy; // �� �Ѿ��� �߻��ϴ� ��ü �÷��̾�
    private bool dead; // �� �Ѿ��� Ȱ��ȭ ���θ� Ȯ������ ����
    public float speed; // �Ѿ��� �ӵ�

    private Vector3 moveDirection3; // �Ѿ��� �̵����� (Vector3)
    private Vector2 moveDirection2; // �Ѿ��� �̵����� (Vector2)
    private Vector2 bulletPosition; // ���� �Ѿ��� ��ġ
    public float spreadRange; // ź���� ����
    private float spread; // ���� ź����

    public RaycastHit2D[] Targets;
    public LayerMask Target_layer;
    public Transform Nearest_enemy;

    private void Awake()
    {
        // ���� ������Ʈ�� ������ٵ� ������
        rb2 = gameObject.GetComponent<Rigidbody2D>();
        // �ӵ� ����
        speed = 10f;
        // ź���� ���� ����
        spreadRange = 5f;
        // ���� ź������ ź���� ���� ���̿��� �����ϰ� ����
        spread = Random.Range(-spreadRange, spreadRange);
    }

    // Ǯ�Ŵ������� ��Ȱ��ȭ�� �Ѿ��� Ȱ��ȭ �ɶ� ���� �۵��� �ż���
    private void OnEnable()
    {
        Target_layer = LayerMask.GetMask("Enemy");
        Targets = Physics2D.CircleCastAll(transform.position, 10, Vector2.zero, 0, Target_layer);
        Nearest_enemy = Nearest();
        // �Ѿ��� ��Ȱ��ȭ ���θ� �������� �ٲ�
        dead = false;
        // �Ѿ��� �̵������� �÷��̾ ���ϴ� �������� ����
        moveDirection3 = Quaternion.AngleAxis(spread, new Vector3(0, 0, 1)) * Nearest_enemy.transform.up;
        // ��꿡 �ʿ��� Vector2 ������ ���� ������ ��������
        moveDirection2 = (Vector2)moveDirection3;
        // �Ѿ��� ���� ��ġ�� ���
        bulletPosition = new Vector2(transform.position.x, transform.position.y);
       
        // �Ѿ��� ��� �������� �ʵ��� �ϴ� �ڷ�ƾ Disable�� ����
        StartCoroutine(Disable());
    }

    Transform Nearest() {

        //���� ����� Target�� ������ ����
        Transform Result = null;

        //Player�� ���� ����� Target�� �Ÿ��� ������ ����, �ʱⰪ�� ���Ƿ� 100���� ����
        float Difference = 100f;

        //Targets�� ����ִ� ��� ���ҿ� foreach�� ����
        foreach (RaycastHit2D Target in Targets) {

            //Player�� ��ǥ��, Target�� ��ǥ�� ��������
            Vector3 My_pos = transform.position;
            Vector3 Target_pos = Target.transform.position;

            //Distance�� ���� Player�� Targer�� �Ÿ��� ��������
            float Current_difference = Vector3.Distance(My_pos, Target_pos);

            //���� foreach���� Target���� �Ÿ���, ����Ǿ� �ִ� Player - Target ���� �Ÿ����� ª����
            //Difference�� Result�� ���� �ʱ�ȭ
            if (Current_difference < Difference) {

                //�� ����� �Ÿ�, Target���� ��ü
                Difference = Current_difference;
                Result = Target.transform;
            }
        }

        return Result;
    }

    // �Ѿ˿� velocity�� �ο�����
    private void FixedUpdate()
    {
        // �Ѿ��� �ӵ��� ���ϴ� ������ ����
        rb2.velocity = moveDirection2.normalized * speed;
    }

    // �Ѿ��� ���� ������ �����ϸ� ��Ȱ��ȭ ��Ű�� �ڷ�ƾ
    private IEnumerator Disable()
    {
        // ���� ���°� �ƴ϶��
        while (!dead)
        {
            // �÷��̾�� �Ѿ� ������Ʈ ������ �Ÿ��� ���
            float distance = Vector2.Distance(Nearest_enemy.transform.position, gameObject.transform.position);

            // �Ÿ��� 100f �̳���� 1���� �ڷ�ƾ �����
            if (distance <= 100f)
            {
                yield return new WaitForSeconds(1f);
            }
            // �Ÿ��� 100f �ʰ���� �Ѿ��� ��Ȱ��ȭ, ���¸� �������� �ٲ�
            else
            {
                gameObject.SetActive(false);
                dead = true;
            }
        }
    }
}