using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ü�°� ������ �������� ���� �����ý��� �׽�Ʈ�� ���� ����ƺ� (����)
public class EnemyDummy : MonoBehaviour, IOnDamage
{
    public float health; // ������ ü��
    public float damage; // ������ ������
    void Awake()
    {
      
    }

    // �ǰݽ� ü�°��� ó���� ����ϴ� �������̽�
    public virtual void onDamage(float otherDamage)
    {
        //���Ƿ� ���� �����غ�, �޴� �������� ��� �������� �����, �� ������ ���� �� ����
        health -= otherDamage * 100 / (100 + damage);

        // ü���� 0 ���� && ���� ���� �ʾҴٸ� ��� ó�� ����
        if (health <= 0)
        {
            // ������Ʈ ��Ȱ��ȭ
            gameObject.SetActive(false);
        }
    }

    // �Ʊ��� �ƴ� ������Ʈ�� �浹�� ��� ������Ʈ�� �������� �ִ� �ż���
    private void OnTriggerStay2D(Collider2D other)
    {
        // �浹�� ����� �±װ� Enemy(�Ʊ�)�� �ƴҶ�
        if (other.tag != "Enemy")
        {
            // ����� OnDamage�� �������� ���� �õ�
            IOnDamage onDamage = other.GetComponent<IOnDamage>();
            // ���� �������� ���� �������� �ʾҴٸ�
            if (onDamage != null)
            {
                // ����� OnDamage�� �ڽ��� ������ ���� ����
                onDamage.onDamage(damage);
            }
        }
    }
}
