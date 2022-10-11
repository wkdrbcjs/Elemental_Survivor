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
        temp = Instantiate(CurrentSpanwer, this.transform.position, Quaternion.identity); //현재위치에 타임라인을 temp라는 게임오브젝트로 만들어서
        temp.transform.SetParent(this.transform, false);                                   // temp의 위치를 이 객체의 하위로 생성함.
        Spawn = false;

        //게임매니저에게 결정된 타임라인이 무엇인지 알려줌.
        gameManager.RecieveSpawner(temp);
    }

    //게임매니저가 호출해주면 
    //속성값 바뀌고 TimeLine이 결정됨.
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
