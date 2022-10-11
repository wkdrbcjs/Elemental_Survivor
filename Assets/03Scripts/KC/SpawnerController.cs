using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject Spawner;
    //public GameObject Spawner_B;
    private int selectNum;

    public GameObject CurrentSpanwer;
    public GameObject temp;

    private bool Spawn;

    private void Awake()
    {
        Spawn = false;
    }
    void Start()
    {
        gameManager = GameManager.instance;// GameObject.FindGameObjectWithTag("GameManager");
        selectNum = -1;
    }

    void Update()
    {
        if (Spawn == true)
        {
            WhatisCurrentSpawner();
        }
    }

    void WhatisCurrentSpawner()
    {
        temp = Instantiate(CurrentSpanwer, this.transform.position, Quaternion.identity); //������ġ�� Ÿ�Ӷ����� temp��� ���ӿ�����Ʈ�� ����
        temp.transform.SetParent(this.transform, false);                                   // temp�� ��ġ�� �� ��ü�� ������ ������.
        Spawn = false;

        //���ӸŴ������� ������ Ÿ�Ӷ����� �������� �˷���.
        gameManager.RecieveSpawner(temp);
    }

    //���ӸŴ����� ȣ�����ָ� 
    //�Ӽ��� �ٲ�� TimeLine�� ������.
    public void SelectSpawner()
    {
        CurrentSpanwer = Spawner;
        //selectNum = Random.Range(0, 2); //0 or 1

        //if (selectNum == 0)
        //{ CurrentSpanwer = Spawner_A; }
        //else //selectNum=1
        //{ CurrentSpanwer = Spawner_B; }

        Spawn = true;
    }
}
