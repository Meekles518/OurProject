using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����μ��� �Ϻ� ��ũ��Ʈ�� �ν��Ͻ� ������ ���� ã�� ���Ҹ� ��
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PoolManager poolManager;
    public GameObject player;
    public PlayerInput playerInput;
    public bool isDefensiveEngage;
    public bool isNotOppEngage;


    void Awake()
    {

        instance = this;
        isDefensiveEngage = false;
        isNotOppEngage = true;


    }
}
