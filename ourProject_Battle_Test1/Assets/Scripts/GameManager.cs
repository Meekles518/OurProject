using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PoolManager poolManager;
    public GameObject player;
    public PlayerInput playerInput;

    void Awake()
    {

        instance = this;
    }
}
