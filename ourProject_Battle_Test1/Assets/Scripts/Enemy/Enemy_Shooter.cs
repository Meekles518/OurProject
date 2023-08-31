using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���ϴ� Shooter ������Ʈ�� ��ų� ������
public class Enemy_Shooter : MonoBehaviour
{
    public Shooter shooter; // �ʿ��� Shooter �ҷ�����

    public float reloadInterval; // ������ �ñⰣ�� �ð� ����
    public float lastReloadTime; // ������ ���� ����
    public bool ShootPlayer;

    private void OnEnable()
    {
        // ������ �ñⰣ�� �ð� ���� �ʱ�ȭ
        reloadInterval = 10f;
        // ������ ���� ���� �ʱ�ȭ
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

    // �Է��� �����ϰ� �� �߻��ϰų� ������
    private void FixedUpdate()
    {
        ShootPlayer = false;
        if (ShootPlayer == true)
        {
            shooter.Fire();

            // ������ ���� �ð����� ������ �ð��� �����ֱ⺸�� ��� źâ�� ź���� �ִ� ź������ ���� ��
            // ���� �������� ����������� ���� �� ����
            if (Time.time - lastReloadTime >= reloadInterval && shooter.magAmmo != shooter.magCapacity)
            {
                // ������ �ϴ� Reload �ż��� ����
                shooter.Reload();
                // ������ ���� �ð��� ����� ����
                lastReloadTime = Time.time;
            }
        }
    }

}