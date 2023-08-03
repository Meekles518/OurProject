using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bullet : MonoBehaviour, IOnDamage
{
    private Rigidbody2D rb2; // 총알의 리지드바디
    public GameObject player; // 이 총알을 발사하는 주체 플레이어
    private bool dead; // 이 총알의 활성화 여부를 확인해줄 변수

    public float damage; // 총알의 데미지
    public float health; // 총알의 체력
    public float speed; // 총알의 속도

    private Vector3 moveDirection3; // 총알의 이동방향 (Vector3)
    private Vector2 moveDirection2; // 총알의 이동방향 (Vector2)
    private Vector2 bulletPosition; // 현재 총알의 위치
    public float spreadRange; // 탄퍼짐 정도
    private float spread; // 최종 탄퍼짐

    private void Awake()
    {
        // 현재 오브젝트의 리지드바디를 가져옴
        rb2 = gameObject.GetComponent<Rigidbody2D>();       
        // 필요한 변수들의 초기화
        damage = 10f;
        health = 100f;
        speed = 10f;
        spreadRange = 5f;
        // 최종 탄퍼짐을 탄퍼짐 정도 사이에서 랜덤하게 결정
        spread = Random.Range(-spreadRange, spreadRange);
    }

    // 풀매니저에서 비활성화된 총알이 활성화 될때 마다 작동할 매서드
    private void OnEnable()
    {
        // 총알의 비활성화 여부를 거짓으로 바꿈
        dead = false;
        // 총알을 발사 할 주체인 플레이어를 찾음
        player = GameObject.Find("Player");
        // 총알의 이동방향을 플레이어가 향하는 방향으로 설정
        moveDirection3 = Quaternion.AngleAxis(spread, new Vector3(0, 0, 1)) * player.transform.up;
        // 계산에 필요한 Vector2 값으로 위의 방향을 변경해줌
        moveDirection2 = (Vector2)moveDirection3;
        // 총알의 현재 위치를 계산
        bulletPosition = new Vector2(transform.position.x, transform.position.y);
        // 총알이 계속 남아있지 않도록 하는 코루틴 Disable을 실행
        StartCoroutine(Disable());
    }

    // 총알에 velocity를 부여해줌
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
        //임의로 식을 설정해봄, 받는 데미지는 상대 데미지에 정비례, 내 데미지 증가 시 감소
        health -= otherDamage * 100 / (100 + damage);

        // 체력이 0 이하 && 아직 죽지 않았다면 사망 처리 실행
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
