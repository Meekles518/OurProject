using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    //자동 원거리 공격 구현

    //자동 공격의 적 탐색 범위
    public float Scan_range;

    //CircleCastAll 함수에서, 탐색할 Layer을 저장할 변수
    public LayerMask Target_layer;

    public Collider2D Target;

    private Rigidbody2D enemyRigidbody; // 플레이어의 리지드바디

    public float moveSpeed; // 이동 속도
    public float rotateSpeed; // 회전 속도

    private Vector2 moveDirection; // 우주선이 이동해야하는 방향
    private Vector2 rotateDirection; // 우주선이 회전해야하는 방향
    private float actualRotate; // 현재 각도에서 rotateDirection을 만족하기 위해 회전해야하는 각도(도)

    private void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        Scan_range = 30;
        moveSpeed = 5f;
        rotateSpeed = 100f;

    }


    private void Update()
    {

        //원의 범위로 주변을 Cast하는 함수(스캔 위치, 원의 반지름, 스캔 방향, 스캔 거리, Layermask)
        //주변에서 Scan도중, 목표 대상이 잡히면 Targets List에 넣기?
        Target = Physics2D.OverlapCircle(transform.position, Scan_range, Target_layer);
        // 움직임 실행

        // 움직임 실행
        Move();
        // 회전 실행
        Rotate();

        //move towards the player
        /*if (Vector2.Distance(transform.position, Target.transform.position) > 4f)
        {//move if distance from target is greater than 1
            //transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0, 0));
            moveDirection = new Vector2(Target.transform.position.x - transform.position.x, Target.transform.position.y - transform.position.y);
            enemyRigidbody.AddForce(moveDirection.normalized * moveSpeed);
        }*/
  


    }

    private void Move()
    {
        if (Vector2.Distance(transform.position, Target.transform.position) > 10f)
        {
            // 가로축, 세로축 입력값을 통해 moveDirection 구함
            moveDirection = new Vector2(Target.transform.position.x - transform.position.x, Target.transform.position.y - transform.position.y);
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
        rotateDirection = Target.transform.position - transform.position;

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