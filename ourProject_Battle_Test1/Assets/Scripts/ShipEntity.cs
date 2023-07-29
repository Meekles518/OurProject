using System;
using UnityEngine;

//�÷��̾�, �� ���ּ�, ��е� ���ֿ��� �̵��ϰ� ü���ִ� ��� ������Ʈ���� ���� ���� ����
public class ShipEntity : MonoBehaviour, IOnDamage //IOnDamage ���
{
    public bool dead { get; protected set; } //���ּ��� ��� ���θ� �� �� �ִ� ����
    public float startingHealth { get; protected set; } = 100f; //���ּ��� �ʱ� ü��
    public float health; //���ּ��� ����ü��
    public float damage; //���ּ��� ������
    public event Action onDeath; // ����� �ߵ��� �̺�Ʈ

    //onEnable�� �ʱ� �� ����
    protected virtual void OnEnable()
    {
        dead = false; //������
        health = startingHealth; //����ü�� = �ʱ�ü��

    }

    //�������̽� IOnDamage���� ������ �ǰݽ� ü�°��� ó�� �޼���
    public virtual void onDamage(float health, float damage, float otherDamage)
    {
        //���Ƿ� ���� �����غ�, �޴� �������� ��� �������� �����, �� ������ ���� �� ����
        health -= otherDamage * 10 / (10 + damage); 

        // ü���� 0 ���� && ���� ���� �ʾҴٸ� ��� ó�� ����
        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    // ��� ó��
    public virtual void Die()
    {
        // onDeath �̺�Ʈ�� ��ϵ� �޼��尡 �ִٸ� ����
        // ShipEntity�� ��� ���� �ٸ� Ŭ����(�÷��̾�, �� ��)���� onDeath �̺�Ʈ�� ����� �߰��Ұ���
        if (onDeath != null)
        {
            onDeath();
        }

        // ��� ���¸� ������ ����
        dead = true;
    }






}
