using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 체력과 데미지 변수만을 가진 전투시스템 테스트를 위한 허수아비 (더미)
public class EnemyDummy : MonoBehaviour, IOnDamage
{
    public float health; // 더미의 체력
    public float damage; // 더미의 데미지
    void Awake()
    {
      
    }

    // 피격시 체력감소 처리를 담당하는 인터페이스
    public virtual void onDamage(float otherDamage)
    {
        //임의로 식을 설정해봄, 받는 데미지는 상대 데미지에 정비례, 내 데미지 증가 시 감소
        health -= otherDamage * 100 / (100 + damage);

        // 체력이 0 이하 && 아직 죽지 않았다면 사망 처리 실행
        if (health <= 0)
        {
            // 오브젝트 비활성화
            gameObject.SetActive(false);
        }
    }

    // 아군이 아닌 오브젝트와 충돌시 상대 오브젝트에 데미지를 주는 매서드
    private void OnTriggerStay2D(Collider2D other)
    {
        // 충돌한 상대의 태그가 Enemy(아군)이 아닐때
        if (other.tag != "Enemy")
        {
            // 상대의 OnDamage를 가져오는 것을 시도
            IOnDamage onDamage = other.GetComponent<IOnDamage>();
            // 만약 가져오는 것을 실패하지 않았다면
            if (onDamage != null)
            {
                // 상대의 OnDamage에 자신의 데미지 값을 전달
                onDamage.onDamage(damage);
            }
        }
    }
}
