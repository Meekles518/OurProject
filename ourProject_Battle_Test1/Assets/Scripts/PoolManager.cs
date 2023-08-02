using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{

    //Prefab을 보관할  변수
    public GameObject[] Prefabs;

    //Pool 을 담을 List변수
    List<GameObject>[] Pools;

    //Pools list 초기화
    private void Awake()
    {
        // Pools에 담을 프리팹의 개수를 배열길이로 설정
        Pools = new List<GameObject>[Prefabs.Length];

        // 각 프리팹의 종류를 List<GameObject>로 불러옴
        for (int i = 0; i < Pools.Length; i++)
        {

            Pools[i] = new List<GameObject>();

        }
    }

    //GameObject를 return해주는 함수 
    public GameObject Get(int idx, Transform fireTransform)
    {
        // Select 선언, 초기화
        GameObject Select = null;

        //Pools[idx]에 대해서 E로 접근
        foreach (GameObject E in Pools[idx])
        {

            //E가 비활설화 되어있는 경우
            if (!E.activeSelf)
            {

                //E를 SetActive로 활성화
                Select = E;
                Select.transform.position = fireTransform.position;
                Select.SetActive(true);
                break;
            }
        }


        //Select의 값이 없을 경우
        if (!Select)
        {

            //Instantiate로 Prefabs[idx]에 있는 원소를 생성하고,
            Select = Instantiate(Prefabs[idx], fireTransform.position, fireTransform.rotation);

            //Pools에 Add해주기
            Pools[idx].Add(Select);

        }

        return Select;

    }




}