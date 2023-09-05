using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 현재로서는 일부 스크립트를 인스턴스 참조로 쉽게 찾는 역할만 함
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
