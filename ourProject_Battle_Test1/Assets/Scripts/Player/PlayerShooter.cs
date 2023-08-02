using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 주어진 Gun 오브젝트를 쏘거나 재장전
public class PlayerShooter : MonoBehaviour
{
    public Shooter shooter; // 사용할 총
    private PlayerInput playerInput; // 플레이어의 입력

    public float reloadInterval; // 재장전 소요 시간
    public float lastReloadTime; // 직전의 장전 시점

    private void Awake()
    {
        // 사용할 컴포넌트들을 가져오기
        playerInput = GetComponent<PlayerInput>();
        reloadInterval = 10f;
        lastReloadTime = 0;
    }

    private void Update()
    {
        // 입력을 감지하고 총 발사하거나 재장전
        if (playerInput.fire)
        {
            // 발사 입력 감지시 총 발사
            shooter.Fire();
           
        }
        else if (Time.time - lastReloadTime >= reloadInterval && shooter.magAmmo != shooter.magCapacity)
        {
           
                shooter.Reload();
                lastReloadTime = Time.time;
            
        }
       
        // 남은 탄약 UI를 갱신
        //UpdateUI();
    }

    // 탄약 UI 갱신
    /*private void UpdateUI()
    {
        if (gun != null && UIManager.instance != null)
        {
            // UI 매니저의 탄약 텍스트에 탄창의 탄약과 남은 전체 탄약을 표시
            UIManager.instance.UpdateAmmoText(gun.magAmmo, gun.ammoRemain);
        }
    }*/

  
}