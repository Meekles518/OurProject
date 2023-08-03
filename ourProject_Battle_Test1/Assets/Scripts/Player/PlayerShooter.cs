using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���ϴ� Shooter ������Ʈ�� ��ų� ������
public class PlayerShooter : MonoBehaviour
{
    public Shooter shooter; // �ʿ��� Shooter �ҷ�����
    private PlayerInput playerInput; // PlayerInput�� �ҷ���

    public float reloadInterval; // ������ �ñⰣ�� �ð� ����
    public float lastReloadTime; // ������ ���� ����

    private void Awake()
    {
        // ����� ������Ʈ���� ��������
        playerInput = GetComponent<PlayerInput>();
        // ������ �ñⰣ�� �ð� ���� �ʱ�ȭ
        reloadInterval = 10f;
        // ������ ���� ���� �ʱ�ȭ
        lastReloadTime = 0;
    }

    // �Է��� �����ϰ� �� �߻��ϰų� ������
    private void FixedUpdate()
    {
        // �÷��̾��� �Է��� fire���
        if (playerInput.fire)
        {
            // �߻縦�ϴ� Fire �ż��� ����
            shooter.Fire();
           
        }
        // ������ ���� �ð����� ������ �ð��� �����ֱ⺸�� ��� źâ�� ź���� �ִ� ź������ ���� ��
        // ���� �������� ����������� ���� �� ����
        else if (Time.time - lastReloadTime >= reloadInterval && shooter.magAmmo != shooter.magCapacity)
        {
                // ������ �ϴ� Reload �ż��� ����
                shooter.Reload();
                // ������ ���� �ð��� ����� ����
                lastReloadTime = Time.time;           
        }
       
    }

}