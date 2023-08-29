using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    // ���� ���¸� ǥ���ϴµ� ����� Ÿ���� �����Ѵ�
    public enum State
    {
        Ready, // �߻� �غ��
        Empty, // źâ�� ��
        Reloading // ������ ��
    }

    public State state { get; private set; } // ���� ���� ����

    public Transform fireTransform; // ����ü�� �߻�� ��ġ
    public Rigidbody2D objectRigidbody; // �÷��̾���  Rigidbody

    public int magCapacity; // źâ �뷮
    public int magAmmo; // ���� źâ�� �����ִ� ź��
    public float recoil; // �߻�� �ݵ�
    public int bulletType;

    public float timeBetFire; // ����ü �߻� ����
    private float lastFireTime; // ���� ���������� �߻��� ����
    public int projectilesPerFire; // �ѹ� Ŭ���� �߻��ϴ� ����ü ��
    public float timeBetProjectiles; // �ѹ� Ŭ���� �߻�Ǵ� ����ü ���� �ð� ����
    public float reloadTime; // ������ �ҿ� �ð�


    private void OnEnable()
    {
        // ���� źâ�� ����ä���
        magAmmo = magCapacity;
        // ���� ���� ���¸� ���� �� �غ� �� ���·� ����
        state = State.Ready;
        // ���������� ���� �� ������ �ʱ�ȭ
        lastFireTime = 0;
    }

    private void FixedUpdate()
    {
        // ���� ���� ��ġ�� ���������� ������Ʈ
        fireTransform = gameObject.transform;
    }

    // �߻� �õ�
    public void Fire()
    {
        // ���� ���°� �߻� ������ ����
        // && ������ �� �߻� �������� timeBetFire �̻��� �ð��� ����
        if (state == State.Ready
            && Time.time >= lastFireTime + timeBetFire)
        {
            // ������ �� �߻� ������ ����
            lastFireTime = Time.time;
            // ���� �߻� ó�� ����
            Shot();
          
        }
    }

    // ���� �߻� ó��
    private void Shot()
    {      
        // �߻���� �ڷ�ƾ ����
        StartCoroutine("ShotLogic");

        // ���� źȯ�� ���� -1
        magAmmo--;
        
        if (magAmmo <= 0)
        {
            // źâ�� ���� ź���� ���ٸ�, ���� ���� ���¸� Empty���� ����
            state = State.Empty;
            
        }
    }

    // �ѹ� Ŭ���� ����ü �߻� ������ �߻簣�� ������ �����ϴ� ���
    IEnumerator ShotLogic()
    {        
            // �ѹ��� Ŭ���� �߻��ϴ� ����ü�� ��ŭ for�� ���� �ݺ�
            for (int i = 0; i < projectilesPerFire; i++)
            {               
                // ���� �Ѿ� �߻� �޼��� ShootProjectiles�� Ŭ���� �߻� �ӵ����� �۵�
                Invoke("ShootProjectiles", timeBetProjectiles * i);
                
            }
            
            // ����ü �߻� ���ݸ�ŭ ���
            yield return new WaitForSeconds(timeBetFire);         
    }

    // �ʿ��� ����ü�� �����ؼ� �߻�
    private void ShootProjectiles()
    {
        // Ǯ���� ����ü�� �ҷ��� �߻�� ��ġ�� ����
        GameManager.instance.poolManager.Get(bulletType, fireTransform);
        //����ü�� �߻��� ��ġ �ݴ� �������� �÷��̾�� �ݵ��� ��
        objectRigidbody.AddForce(-fireTransform.up.normalized * recoil);
    }


    // ������
    public void Reload()
    {
        // �̹� ������ ���̰ų� źâ�� ź���� �̹� ������ ��� ������ �Ҽ� ����
        if (state != State.Reloading && magAmmo < magCapacity)
        {
            // ������ ó�� ����
            StartCoroutine(ReloadRoutine());
        }
    }

    // ���� ������ ó���� ����
    private IEnumerator ReloadRoutine()
    {
        // ���� ���¸� ������ �� ���·� ��ȯ
        state = State.Reloading;

        // ������ �ҿ� �ð� ��ŭ ó���� ����
        yield return new WaitForSeconds(reloadTime);

        //źâ�� ź���� ä���.
        magAmmo = magCapacity;

        // ���� ���� ���¸� �߻� �غ�� ���·� ����
        state = State.Ready;
    }
}