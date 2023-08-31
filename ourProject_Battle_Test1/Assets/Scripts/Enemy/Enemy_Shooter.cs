using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 원하는 Shooter 오브젝트를 쏘거나 재장전
public class Enemy_Shooter : MonoBehaviour
{
    public Shooter shooter; // 필요한 Shooter 불러오기

    public float reloadInterval; // 재장전 시기간의 시간 간격
    public float lastReloadTime; // 마지막 장전 시점
    public bool ShootPlayer;

    private void OnEnable()
    {
        // 재장전 시기간의 시간 간격 초기화
        reloadInterval = 10f;
        // 마지막 장전 시점 초기화
        lastReloadTime = 0;
        ShootPlayer = false;
        shooter.magCapacity = 10;
        shooter.projectilesPerFire = 1;
        shooter.objectRigidbody = GetComponent<Rigidbody2D>();
        shooter.bulletType = 1;
        shooter.recoil = 10;
        shooter.timeBetFire = 1.0f;
        shooter.timeBetProjectiles = 0.1f;
        shooter.reloadTime = 1f;
    }

    // 입력을 감지하고 총 발사하거나 재장전
    private void FixedUpdate()
    {
        ShootPlayer = false;
        if (ShootPlayer == true)
        {
            shooter.Fire();

            // 마지막 장전 시간으로 부터의 시간이 장전주기보다 길고 탄창의 탄수가 최대 탄수보다 적을 때
            // 수동 재장전도 고려중이지만 아직 못 만듬
            if (Time.time - lastReloadTime >= reloadInterval && shooter.magAmmo != shooter.magCapacity)
            {
                // 장전을 하는 Reload 매서드 실행
                shooter.Reload();
                // 마지막 장전 시간을 현재로 설정
                lastReloadTime = Time.time;
            }
        }
    }

}