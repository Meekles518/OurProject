using System;
using System.Collections;
using UnityEngine;

//�÷��̾�, �� ���ּ�, ��е� ���ֿ��� �̵��ϰ� ü���ִ� ��� ������Ʈ���� ����� Ŭ����
public class ShipEntity : MonoBehaviour
{
    public bool dead { get; protected set; } //���ּ��� ��� ���θ� �� �� �ִ� ����
    public float startingHealth { get; protected set; } = 1000f; //���ּ��� �ʱ� ü��
    public float health; //���ּ��� ����ü��
    public float damage; //���ּ��� ����(������)
    public event Action onDeath; // ����� �ߵ��� �̺�Ʈ
    private bool inCollision;
    private Collider2D collideEnemy;

    //onEnable�� �ʱ� �� ����
    protected virtual void OnEnable()
    {
        dead = false; //������
        //health = startingHealth; //����ü�� = �ʱ�ü��
        inCollision = false;
        StartCoroutine("giveDamage");

    }

    //�������̽� IOnDamage���� ������ �ǰݽ� ü�°��� ó�� �޼���
    public virtual void takeDamage(float otherDamage)
    {
        //���Ƿ� ���� �����غ�, �޴� �������� ��� �������� �����, �� ������ ���� �� ����
        health -= otherDamage* 100 / (100 + damage); 

        // ü���� 0 ���� && ���� ���� �ʾҴٸ� ��� ó�� ����
        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        inCollision = true;
        collideEnemy = other;
        Debug.Log(inCollision);
        Debug.Log(!dead);
        //StartCoroutine("giveDamage");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //inCollision = false;
        //collideEnemy = null;
        //StopCoroutine("giveDamage");
        //Debug.Log("off");
    }

    private IEnumerator giveDamage()
    {
        if (inCollision == true)
        {
            Debug.Log("1");
            if (collideEnemy.tag != gameObject.tag)
            {
                ShipEntity shipEntity = collideEnemy.GetComponent<ShipEntity>();
                Debug.Log("2");
                if (shipEntity != null)
                {
                    shipEntity.takeDamage(damage);
                    Debug.Log("3");
                }

            }
        }
        yield return new WaitForSeconds(1f);
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
