using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

// 
public class Drone_Control : MonoBehaviour
{
    public Transform player; // 플레이어 트랜스폼
    public Transform selfposition; // 드론 트랜스폼
    public float smallAgrro; // 작은 어그로 범위
    public float largeAgrro; // 큰 어그로 범위
    public float timer; // 타이머 변수
    private PlayerInput playerInput; // PlayerInput을 가져옴
    public bool isSpread;

    public enum STATE
    {
        IDLE, // 초기 상태
        SPREAD,
        ENGAGE
    };

    public STATE statename; // STATE 변수 (Drone_Movement 제어)

    public void OnEnable()
    {
        // 플레이어 오브젝트를 찾아 트랜스폼 할당
        player = GameObject.Find("Player").transform;
        // PlayerInput을 가져옴
        playerInput = GetComponent<PlayerInput>();
        // 자기 위치 초기화
        selfposition = GetComponent<Transform>();
        // 어그로 범위 설정 (이거를 지우면 유니티 인스펙터에서 실시간으로 개별 전대별 설정가능)
        smallAgrro = 15f;
        largeAgrro = 25f;
        // 에러가 나지않게 초기값들 설정
        statename = STATE.IDLE;
        timer = 100;
        isSpread = false;
 
    }


    public void FixedUpdate()
    {
        // 자기 위치 갱신
        selfposition = GetComponent<Transform>();
        if (playerInput.special)
        {
            isSpread = true;
        }
        else
        {
            isSpread = false;
        }

    }
}