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
        //임의로 식을 설정해봄, 받는 데미지는 상대 데미지에 정비례, 내 데미지 증가 시 감소
        health -= otherDamage * 100 / (100 + damage);

        // 체력이 0 이하 && 아직 죽지 않았다면 사망 처리 실행
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
