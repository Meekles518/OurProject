using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 원하는 Shooter 오브젝트를 쏘거나 재장전
public class PlayerShooter : MonoBehaviour
{
    public Shooter shooter; // 필요한 Shooter 불러오기
    private PlayerInput playerInput; // PlayerInput을 불러옴

    public float reloadInterval; // 재장전 시기간의 시간 간격
    public float lastReloadTime; // 마지막 장전 시점

    private void Awake()
    {
        // 사용할 컴포넌트들을 가져오기
        playerInput = GetComponent<PlayerInput>();
        // 재장전 시기간의 시간 간격 초기화
        reloadInterval = 10f;
        // 마지막 장전 시점 초기화
        lastReloadTime = 0;
    }

    // 입력을 감지하고 총 발사하거나 재장전
    private void FixedUpdate()
    {
        // 플레이어의 입력이 fire라면
        if (playerInput.fire)
        {
            // 발사를하는 Fire 매서드 실행
            shooter.Fire();
           
        }
        // 마지막 장전 시간으로 부터의 시간이 장전주기보다 길고 탄창의 탄수가 최대 탄수보다 적을 때
        // 수동 재장전도 고려중이지만 아직 못 만듬
        else if (Time.time - lastReloadTime >= reloadInterval && shooter.magAmmo != shooter.magCapacity)
        {
                // 장전을 하는 Reload 매서드 실행
                shooter.Reload();
                // 마지막 장전 시간을 현재로 설정
                lastReloadTime = Time.time;           
        }
       
    }

}