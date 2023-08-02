using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �־��� Gun ������Ʈ�� ��ų� ������
public class PlayerShooter : MonoBehaviour
{
    public Shooter shooter; // ����� ��
    private PlayerInput playerInput; // �÷��̾��� �Է�

    public float reloadInterval; // ������ �ҿ� �ð�
    public float lastReloadTime; // ������ ���� ����

    private void Awake()
    {
        // ����� ������Ʈ���� ��������
        playerInput = GetComponent<PlayerInput>();
        reloadInterval = 10f;
        lastReloadTime = 0;
    }

    private void Update()
    {
        // �Է��� �����ϰ� �� �߻��ϰų� ������
        if (playerInput.fire)
        {
            // �߻� �Է� ������ �� �߻�
            shooter.Fire();
           
        }
        else if (Time.time - lastReloadTime >= reloadInterval && shooter.magAmmo != shooter.magCapacity)
        {
           
                shooter.Reload();
                lastReloadTime = Time.time;
            
        }
       
        // ���� ź�� UI�� ����
        //UpdateUI();
    }

    // ź�� UI ����
    /*private void UpdateUI()
    {
        if (gun != null && UIManager.instance != null)
        {
            // UI �Ŵ����� ź�� �ؽ�Ʈ�� źâ�� ź��� ���� ��ü ź���� ǥ��
            UIManager.instance.UpdateAmmoText(gun.magAmmo, gun.ammoRemain);
        }
    }*/

  
}