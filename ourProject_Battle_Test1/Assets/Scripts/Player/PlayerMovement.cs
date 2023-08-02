using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

// 플레이어 우주선의 이동 및 회전 등 움직임을 제어
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed; // 앞뒤 움직임의 속도
    public float rotateSpeed; // 좌우 회전 속도

    //private Animator playerAnimator; // 플레이어 캐릭터의 애니메이터
    private PlayerInput playerInput; // 플레이어 입력을 알려주는 컴포넌트
    private Rigidbody2D playerRigidbody; // 플레이어 캐릭터의 리지드바디

    private Vector2 moveDirection; // 실제로 이동해야 하는 방향
    private Vector2 rotateDirection; // 내 우주선이 회전해야하는 방향
    private Vector2 mousePosition; // 월드맵 상에서의 현재 마우스 위치
    private float actualRotate; // 현재 각도에서 rotateDirection을 만족하기 위해 회전해야하는 각도(도)

    private void Awake()
    {
        // 사용할 컴포넌트들의 참조를 가져오기
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        //playerAnimator = GetComponent<Animator>();

        moveSpeed = 5f;
        rotateSpeed = 100f;
    }

    // FixedUpdate는 물리 갱신 주기에 맞춰 실행됨
    private void FixedUpdate()
    {
        // 회전 실행
        Rotate();
        // 움직임 실행
        Move();

        // 입력값에 따라 애니메이터의 Move 파라미터 값을 변경
        //playerAnimator.SetFloat("Move", playerInput.move);
    }

    // 입력값에 따라 우주선을 움직임
    private void Move()
    {
        // 가로축, 세로축 입력값을 통해 moveDirection 구함
        moveDirection = new Vector2(playerInput.moveHorizontal, playerInput.moveVertical);
        // 그 방향의 단위벡터 * 이동속도만큼의 addForce를 해줌
        playerRigidbody.AddForce(moveDirection.normalized * moveSpeed);
     
    }

    // 마우스 방향으로 우주선을 회전
    private void Rotate()
    {
        // mousePosition에 관한 입력 감지
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 마우스 좌표에서 자신의 좌표 빼서 벡터값 구하기
        rotateDirection = mousePosition - (Vector2) transform.position;

        // 회전해야 하는 각도를 단위 '도'로 구함
        actualRotate = Quaternion.FromToRotation((Vector2) transform.up, rotateDirection).eulerAngles.z;


        // 회전해야 하는 각도의 값에 따라 회전 방향을 결정해줌.
        if (actualRotate < 1 || actualRotate > 359)
        {
            // 떨림을 방지하기 위해 오차를 1도 넣음
            return;
        }

        else if (actualRotate > 180)
        {
            transform.Rotate(0, 0, -Time.deltaTime * rotateSpeed, Space.Self);
        }

        else if (actualRotate < 180)
        {
            transform.Rotate(0, 0, Time.deltaTime * rotateSpeed, Space.Self);
        }
        
    }

   
}
