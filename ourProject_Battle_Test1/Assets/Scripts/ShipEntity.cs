using System;
using UnityEngine;

//플레이어, 적 우주선, 드론등 우주에서 이동하고 체력있는 모든 오브젝트들을 위한 뼈대 제공
public class ShipEntity : MonoBehaviour, IOnDamage //IOnDamage 계승
{
    public bool dead { get; protected set; } //우주선의 사망 여부를 알 수 있는 변수
    public float startingHealth { get; protected set; } = 100f; //우주선의 초기 체력
    public float health; //우주선의 현재체력
    public float damage; //우주선의 데미지
    public event Action onDeath; // 사망시 발동할 이벤트

    //onEnable로 초기 값 설정
    protected virtual void OnEnable()
    {
        dead = false; //안죽음
        health = startingHealth; //현재체력 = 초기체력

    }

    //인터페이스 IOnDamage에서 가져온 피격시 체력감소 처리 메서드
    public virtual void onDamage(float health, float damage, float otherDamage)
    {
        //임의로 식을 설정해봄, 받는 데미지는 상대 데미지에 정비례, 내 데미지 증가 시 감소
        health -= otherDamage * 10 / (10 + damage); 

        // 체력이 0 이하 && 아직 죽지 않았다면 사망 처리 실행
        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    // 사망 처리
    public virtual void Die()
    {
        // onDeath 이벤트에 등록된 메서드가 있다면 실행
        // ShipEntity를 계승 받을 다른 클래스(플레이어, 적 등)에서 onDeath 이벤트에 기능을 추가할거임
        if (onDeath != null)
        {
            onDeath();
        }

        // 사망 상태를 참으로 변경
        dead = true;
    }






}
