using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bullet : MonoBehaviour, IOnDamage
{
    private Rigidbody2D rb2; // �Ѿ��� ������ٵ�
    public GameObject player; // �� �Ѿ��� �߻��ϴ� ��ü �÷��̾�
    private bool dead; // �� �Ѿ��� Ȱ��ȭ ���θ� Ȯ������ ����

    public float damage; // �Ѿ��� ������
    public float health; // �Ѿ��� ü��
    public float speed; // �Ѿ��� �ӵ�

    private Vector3 moveDirection3; // �Ѿ��� �̵����� (Vector3)
    private Vector2 moveDirection2; // �Ѿ��� �̵����� (Vector2)
    private Vector2 bulletPosition; // ���� �Ѿ��� ��ġ
    public float spreadRange; // ź���� ����
    private float spread; // ���� ź����

    private void Awake()
    {
        // ���� ������Ʈ�� ������ٵ� ������
        rb2 = gameObject.GetComponent<Rigidbody2D>();       
        // �ʿ��� �������� �ʱ�ȭ
        damage = 10f;
        health = 100f;
        speed = 10f;
        spreadRange = 5f;
        // ���� ź������ ź���� ���� ���̿��� �����ϰ� ����
        spread = Random.Range(-spreadRange, spreadRange);
    }

    // Ǯ�Ŵ������� ��Ȱ��ȭ�� �Ѿ��� Ȱ��ȭ �ɶ� ���� �۵��� �ż���
    private void OnEnable()
    {
        // �Ѿ��� ��Ȱ��ȭ ���θ� �������� �ٲ�
        dead = false;
        // �Ѿ��� �߻� �� ��ü�� �÷��̾ ã��
        player = GameObject.Find("Player");
        // �Ѿ��� �̵������� �÷��̾ ���ϴ� �������� ����
        moveDirection3 = Quaternion.AngleAxis(spread, new Vector3(0, 0, 1)) * player.transform.up;
        // ��꿡 �ʿ��� Vector2 ������ ���� ������ ��������
        moveDirection2 = (Vector2)moveDirection3;
        // �Ѿ��� ���� ��ġ�� ���
        bulletPosition = new Vector2(transform.position.x, transform.position.y);
        // �Ѿ��� ��� �������� �ʵ��� �ϴ� �ڷ�ƾ Disable�� ����
        StartCoroutine(Disable());
    }

    // �Ѿ˿� velocity�� �ο�����
    private void FixedUpdate()
    {
        rb2.velocity = moveDirection2.normalized * speed;       
    }

    private IEnumerator Disable()
    {
        while (!dead)
        {
            float distance = Vector2.Distance(player.transform.position, gameObject.transform.position);

            if (distance <= 100f)
            {
                yield return new WaitForSeconds(1f);
            }
            else
            {
                gameObject.SetActive(false);
                dead = true;
            }          
        }
    }

    public virtual void onDamage(float otherDamage)
    {
        //���Ƿ� ���� �����غ�, �޴� �������� ��� �������� �����, �� ������ ���� �� ����
        health -= otherDamage * 100 / (100 + damage);

        // ü���� 0 ���� && ���� ���� �ʾҴٸ� ��� ó�� ����
        if (health <= 0)
        {
           gameObject.SetActive(false);
           dead = true;
            
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag != "Player")
        {
            IOnDamage onDamage = other.GetComponent<IOnDamage>();
            if (onDamage != null)
            {
                onDamage.onDamage(damage);
            }
           
        }
    }

}
