using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{

    public GameObject player;

    private Rigidbody2D enemyRigidbody; // 플레이어의 리지드바디

    public float moveSpeed; // 이동 속도
    public float rotateSpeed; // 회전 속도

    private Vector2 moveDirection; // 우주선이 이동해야하는 방향
    private Vector2 rotateDirection; // 우주선이 회전해야하는 방향
    private float actualRotate; // 현재 각도에서 rotateDirection을 만족하기 위해 회전해야하는 각도(도)
    public float x_Random;
    private float y_Random;
    private float xrandomRange = 2f;
    private float yrandomRange = 2f;
    private float currTime;

    private void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        moveSpeed = 5f;
        rotateSpeed = 100f;
        player = GameObject.Find("Player");

    }


    private void Update()
    {

        // 움직임 실행
        Move();
        // 회전 실행
        Rotate();
        currTime += Time.deltaTime;
        if(currTime >3)
        {
            x_Random = Random.Range(-xrandomRange, xrandomRange);
            y_Random = Random.Range(-yrandomRange, yrandomRange);
            currTime = 0;
        }
        
        


    }



    private void Move()
    {
        if (Vector2.Distance(transform.position, player.transform.position) > 10f)
        {
            // 가로축, 세로축 입력값을 통해 moveDirection 구함
            moveDirection = new Vector2(player.transform.position.x - transform.position.x + x_Random, player.transform.position.y - transform.position.y + y_Random);
            enemyRigidbody.velocity = moveDirection.normalized * moveSpeed;
        }
        else
        {
            enemyRigidbody.velocity = new Vector2(0, 0);
        }
    }

    // 마우스 방향으로 우주선을 회전
    private void Rotate()
    {

        // 마우스 좌표에서 자신의 좌표 빼서 벡터값 구하기
        rotateDirection = (player.transform.position - transform.position) + new Vector3(x_Random, y_Random, 0);

        // 회전해야 하는 각도를 단위 '도'로 구함
        actualRotate = Quaternion.FromToRotation((Vector2)transform.up, rotateDirection).eulerAngles.z;


        // 회전해야 하는 각도의 값에 따라 회전 방향을 결정해줌.
        if (actualRotate < 1 || actualRotate > 359)
        {
            // 떨림을 방지하기 위해 오차를 1도 넣음
            return;
        }

        // 시계방향으로 회전이 더 가까울 때 시계방향으로 회전
        else if (actualRotate > 180)
        {
            transform.Rotate(0, 0, -Time.deltaTime * rotateSpeed, Space.Self);
        }

        // 반시계방향으로 회전이 더 가까울 때 반시계방향으로 회전
        else if (actualRotate < 180)
        {
            transform.Rotate(0, 0, Time.deltaTime * rotateSpeed, Space.Self);
        }

    }



}