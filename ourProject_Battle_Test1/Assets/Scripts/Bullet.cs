using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bullet : MonoBehaviour, IOnDamage
{
    private Rigidbody2D rb2;
    public GameObject shooter;
    private bool dead;

    public float damage;
    public float health;
    public float speed;

    private Vector3 moveDirection3;
    private Vector2 moveDirection2;
    private float spread;
    public float spreadRange;

    private void Awake()
    {
        rb2 = gameObject.GetComponent<Rigidbody2D>();
        
        damage = 10f;
        health = 100f;
        speed = 10f;
        spreadRange = 5f;
        spread = Random.Range(-spreadRange, spreadRange);




    }

    private void OnEnable()
    {
        dead = false;
        shooter = GameObject.Find("Player");
        moveDirection3 = Quaternion.AngleAxis(spread, new Vector3(0, 0, 1)) * shooter.transform.up;
        moveDirection2 = (Vector2)moveDirection3;
        
        
        //StartCoroutine(Disable());
    }

    private void FixedUpdate()
    {
        rb2.velocity = moveDirection2.normalized * speed;
       
    }

    /*private IEnumerator Disable()
    {
        while (!dead)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);

            Debug.Log("collide");
            for (int i = 0; i < colliders.Length; i++)
            {
                
                if (colliders[i].tag == "Player")
                {
                    Debug.Log("inrange");
                    yield return new WaitForSeconds(0.5f);                 
                }
            }
            gameObject.SetActive(false);
            dead = true;
            Debug.Log(dead);
            break;
           
        }


    }*/

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

    private IEnumerator OnTriggerStay2D(Collider2D other)
    {
        if (other.tag != "Player")
        {
            IOnDamage onDamage = other.GetComponent<IOnDamage>();
            if (onDamage != null)
            {
                onDamage.onDamage(damage);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }



}
