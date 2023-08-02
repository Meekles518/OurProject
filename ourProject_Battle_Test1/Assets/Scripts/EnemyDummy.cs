using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDummy : MonoBehaviour, IOnDamage
{
    public float health;
    public float damage;
    void Awake()
    {
      
    }


    public virtual void onDamage(float otherDamage)
    {
        //���Ƿ� ���� �����غ�, �޴� �������� ��� �������� �����, �� ������ ���� �� ����
        health -= otherDamage * 100 / (100 + damage);

        // ü���� 0 ���� && ���� ���� �ʾҴٸ� ��� ó�� ����
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag != "Enemy")
        {
            IOnDamage onDamage = other.GetComponent<IOnDamage>();
            if (onDamage != null)
            {
                onDamage.onDamage(damage);
            }
        }
    }
}
