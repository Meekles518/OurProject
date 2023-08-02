using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{

    //Prefab�� ������  ����
    public GameObject[] Prefabs;

    //Pool �� ���� List����
    List<GameObject>[] Pools;

    //Pools list �ʱ�ȭ
    private void Awake()
    {
        // Pools�� ���� �������� ������ �迭���̷� ����
        Pools = new List<GameObject>[Prefabs.Length];

        // �� �������� ������ List<GameObject>�� �ҷ���
        for (int i = 0; i < Pools.Length; i++)
        {

            Pools[i] = new List<GameObject>();

        }
    }

    //GameObject�� return���ִ� �Լ� 
    public GameObject Get(int idx, Transform fireTransform)
    {
        // Select ����, �ʱ�ȭ
        GameObject Select = null;

        //Pools[idx]�� ���ؼ� E�� ����
        foreach (GameObject E in Pools[idx])
        {

            //E�� ��Ȱ��ȭ �Ǿ��ִ� ���
            if (!E.activeSelf)
            {

                //E�� SetActive�� Ȱ��ȭ
                Select = E;
                Select.transform.position = fireTransform.position;
                Select.SetActive(true);
                break;
            }
        }


        //Select�� ���� ���� ���
        if (!Select)
        {

            //Instantiate�� Prefabs[idx]�� �ִ� ���Ҹ� �����ϰ�,
            Select = Instantiate(Prefabs[idx], fireTransform.position, fireTransform.rotation);

            //Pools�� Add���ֱ�
            Pools[idx].Add(Select);

        }

        return Select;

    }




}