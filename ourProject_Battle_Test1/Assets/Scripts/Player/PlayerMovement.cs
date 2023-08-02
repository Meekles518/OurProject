using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

// �÷��̾� ���ּ��� �̵� �� ȸ�� �� �������� ����
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed; // �յ� �������� �ӵ�
    public float rotateSpeed; // �¿� ȸ�� �ӵ�

    //private Animator playerAnimator; // �÷��̾� ĳ������ �ִϸ�����
    private PlayerInput playerInput; // �÷��̾� �Է��� �˷��ִ� ������Ʈ
    private Rigidbody2D playerRigidbody; // �÷��̾� ĳ������ ������ٵ�

    private Vector2 moveDirection; // ������ �̵��ؾ� �ϴ� ����
    private Vector2 rotateDirection; // �� ���ּ��� ȸ���ؾ��ϴ� ����
    private Vector2 mousePosition; // ����� �󿡼��� ���� ���콺 ��ġ
    private float actualRotate; // ���� �������� rotateDirection�� �����ϱ� ���� ȸ���ؾ��ϴ� ����(��)

    private void Awake()
    {
        // ����� ������Ʈ���� ������ ��������
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        //playerAnimator = GetComponent<Animator>();

        moveSpeed = 5f;
        rotateSpeed = 100f;
    }

    // FixedUpdate�� ���� ���� �ֱ⿡ ���� �����
    private void FixedUpdate()
    {
        // ȸ�� ����
        Rotate();
        // ������ ����
        Move();

        // �Է°��� ���� �ִϸ������� Move �Ķ���� ���� ����
        //playerAnimator.SetFloat("Move", playerInput.move);
    }

    // �Է°��� ���� ���ּ��� ������
    private void Move()
    {
        // ������, ������ �Է°��� ���� moveDirection ����
        moveDirection = new Vector2(playerInput.moveHorizontal, playerInput.moveVertical);
        // �� ������ �������� * �̵��ӵ���ŭ�� addForce�� ����
        playerRigidbody.AddForce(moveDirection.normalized * moveSpeed);
     
    }

    // ���콺 �������� ���ּ��� ȸ��
    private void Rotate()
    {
        // mousePosition�� ���� �Է� ����
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // ���콺 ��ǥ���� �ڽ��� ��ǥ ���� ���Ͱ� ���ϱ�
        rotateDirection = mousePosition - (Vector2) transform.position;

        // ȸ���ؾ� �ϴ� ������ ���� '��'�� ����
        actualRotate = Quaternion.FromToRotation((Vector2) transform.up, rotateDirection).eulerAngles.z;


        // ȸ���ؾ� �ϴ� ������ ���� ���� ȸ�� ������ ��������.
        if (actualRotate < 1 || actualRotate > 359)
        {
            // ������ �����ϱ� ���� ������ 1�� ����
            return;
        }

        else if (actualRotate > 180)
        {
            transform.Rotate(0, 0, -Time.deltaTime * rotateSpeed, Space.Self);
        }

        else if (actualRotate < 180)
        {
            transform.Rotate(0, 0, Time.deltaTime * rotateSpeed, Space.Self);
        }
        
    }

   
}
