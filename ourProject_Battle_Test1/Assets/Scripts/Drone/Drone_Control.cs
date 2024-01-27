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
    //public float smallAgrro; // 작은 어그로 범위
    //public float largeAgrro; // 큰 어그로 범위
    public float timer; // 타이머 변수
    public PlayerInput playerInput; // PlayerInput을 가져옴
    public bool isSpread; //산개 여부를 확인하는 bool 변수

    public Transform droneTarget; //  드론의 Target의 Transform을 저장할 변수
    public Vector2 TargetPosition; //  Target의 Position을 나타낼 vector2 변수
    public RaycastHit2D[] Targets;  // CirclecastAll로 가져오는 모든 오브젝트들을 저장할 배열
    public LayerMask Target_layer;  // 검색을 시행할 layer(Enemy, 기체에만 해당)
    public Transform Nearest_enemy; // 캐스트한 오브젝트 중 가장 가까운 Enemy 오브젝트
    public float Scan_range;    //검색 범위를 저장할 float 변수


    public enum STATE
    {
        IDLE, // 초기 상태
        SPREAD, //산개 상태
        ENGAGE,  //교전 상태
        FOLLOW  //플레이어 추적 상태
    };

    public STATE statename; // STATE 변수 (Drone_Movement 제어)

    public void OnEnable()
    {
    
        // 플레이어 오브젝트를 찾아 트랜스폼 할당
        player = GameObject.Find("Player").transform;
        // PlayerInput을 가져옴
        playerInput = GetComponent<PlayerInput>();
        // 자신의 Transform 컴포넌트 가져옴
        selfposition = GetComponent<Transform>();
        //스캔 범위 설정
        Scan_range = 10f;

        // 에러가 나지않게 초기값들 설정
        statename = STATE.IDLE;
        timer = 100;
        isSpread = false;
        droneTarget = null;
        Nearest_enemy = null;
 
    }//OnEnable


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

        //드론의 Target이 설정되있지 않다면
        if (droneTarget == null)
        {
            Find(); //드론 주변의 적을 탐색하는 함수
        }

        //드론의 Target이 정해졌다면
        else
        {
            TargetPosition = droneTarget.position;
        }


    }//FixedUpdate

    //드론 주변의 적을 탐색하는 함수
    public void Find()
    {
        // 검색 레이어를 Enemy로 설정
        Target_layer = LayerMask.GetMask("Enemy");
        //근처의 모든 Enemy 오브젝트를 검색
        Targets = Physics2D.CircleCastAll(transform.position, Scan_range, Vector2.zero, 0, Target_layer);
        //가장 가까운 Enemy 오브젝트를 Target으로 설정
        Nearest_enemy = Nearest();
        if (Nearest_enemy != null)
        {
            droneTarget = Nearest_enemy;
        }//if

    }//Find


    //가장 가까운 오브젝트의 Transform를 return 해줄 함수
    public Transform Nearest()
    {
        //가장 가까운 Target을 돌려줄 변수
        Transform Result = null;

        //각 Target들 중 최소 거리를 저장할 변수
        float distance = Scan_range * 2;

        //Targets 배열에 들어있는 모든 원소에 foreach로 접근
        foreach(RaycastHit2D Target in Targets)
        {
            //드론의 좌표와, Target의 좌표 가져오기
            Vector2 Drone_pos = transform.position;
            Vector2 Target_pos = Target.transform.position;

            //Distance 함수를 통해 드론과 Target의 거리 계산
            float Cur_distance = Vector2.Distance(Drone_pos, Target_pos);

            //현재 foreach문의 Target과의 거리(Cur_distance)가, 저장되어 있는 distance보다 짧으면
            //distance와 Result의 값을 새로 초기화
            if (Cur_distance < distance)
            {
                distance = Cur_distance;
                Result = Target.transform;
            }

           

        }//foreach

        

        return Result;

    }//Nearest


}