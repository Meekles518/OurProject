using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾� ĳ���͸� �����ϱ� ���� ����� �Է��� ����
// ������ �Է°��� �ٸ� ������Ʈ���� ����� �� �ֵ��� ����
// �Ʒ��� ������ ���� ��Ĵ�� ���ϴ� �Էµ��� ��� �߰� ����
public class PlayerInput : MonoBehaviour
{
    // Input ���ÿ� �ִ� �����ϴ� string�� �°� Name������ �Ҵ�
    private string moveVerticalName = "Vertical"; // ���Ʒ� �������� ���� �Է��� �̸�
    private string moveHorizontalName = "Horizontal"; // �¿� �������� ���� �Է��� �̸�
    private string fireButtonName = "Fire1"; // �߻縦 ���� �Է� ��ư �̸�
    private string specialButtonName = "Fire2"; // Ư�� ������ ���� �Է� ��ư �̸�
    private string reloadButtonName = "Reload"; // �������� ���� �Է� ��ư �̸�
    private string missileButtonName = "Missile";

    // �� �Ҵ��� ���ο����� ����
    public float moveVertical { get; private set; } // ������ ������ �Է°�
    public float moveHorizontal { get; private set; } // ������ ȸ�� �Է°�
    public bool fire { get; private set; } // ������ �߻� �Է°�
    public bool special { get; private set; } // ������ Ư�� ���� �Է°�
    public bool reload { get; private set; } // ������ ������ �Է°�
    public bool missile { get; private set; }

    // �������� ����� �Է��� ����
    private void Update()
    {
        // ���ӿ��� ���¿����� ����� �Է��� �������� �ʴ´�
        /*if (GameManager.instance != null
            && GameManager.instance.isGameover)
        {
            moveVertical = 0;
            moveHorizontal = 0;  
            fire = false;
            special = false;
            reload = false;
            return;
        }*/

        // moveVertical�� ���� �Է� ����
        moveVertical = Input.GetAxis(moveVerticalName);
        // moveHorizontal�� ���� �Է� ����
        moveHorizontal = Input.GetAxis(moveHorizontalName);
        // fire�� ���� �Է� ����
        fire = Input.GetButton(fireButtonName);
        //special�� ���� �Է� ����
        special = Input.GetButton(specialButtonName);
        // reload�� ���� �Է� ����
        reload = Input.GetButtonDown(reloadButtonName);
        missile = Input.GetButtonDown(missileButtonName);
        
    }
}
