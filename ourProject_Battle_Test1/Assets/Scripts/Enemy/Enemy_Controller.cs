using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    // 총의 상태를 표현하는데 사용할 타입을 선언한다
    public enum State
    {
        Wait,
        Engage,
        Hunt
    }

    public State state { get; private set; } // 현재 총의 상태

    public Transform SpawnPoint; // 투사체가 발사될 위치
    public Rigidbody2D EnemyRigidbody; // 플레이어의  Rigidbody

    private void OnEnable()
    {

    }

    private void FixedUpdate()
    {

    }

}