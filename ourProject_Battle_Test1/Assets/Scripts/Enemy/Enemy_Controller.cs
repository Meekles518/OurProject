using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    // ���� ���¸� ǥ���ϴµ� ����� Ÿ���� �����Ѵ�
    public enum State
    {
        Wait,
        Engage,
        Hunt
    }

    public State state { get; private set; } // ���� ���� ����

    public Transform SpawnPoint; // ����ü�� �߻�� ��ġ
    public Rigidbody2D EnemyRigidbody; // �÷��̾���  Rigidbody

    private void OnEnable()
    {

    }

    private void FixedUpdate()
    {

    }

}