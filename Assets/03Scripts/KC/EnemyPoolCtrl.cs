using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolCtrl : MonoBehaviour
{
    public GameManager GameManager;

    public GameObject[] EnemyPools = new GameObject[30];

    public GameObject EnemyPool_present;

    //private GameObject EnemyManager;
    public ObjectPoolManager ObjectPoolManager;

    //private bool SpawnApprove; //생성해도 된다는 승인 변수

    //
    private int Spawm_Num;
    private int Stage_num;
    
    void Start()
    {
        Spawm_Num = 0;
        Stage_num = 0;

        GameManager = GameManager.instance;// GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        //EnemyManager = GameObject.FindGameObjectWithTag("EnemyManager");
        ObjectPoolManager = ObjectPoolManager.instance;
        EnemyPool_present = EnemyPools[Spawm_Num];
    }

    public void NextSpawn()
    {
        Spawm_Num += 1;

        // 몬스터 풀 1개 당 15번 진행 -> 다음 몬스터 풀로 변경
        if (Spawm_Num % 15 == 0)
        {
            Stage_num += 1;
            EnemyPool_change();
        }

        StartCoroutine(EnemySpawnStart());
    }

    //  다음 몬스터 풀로 변경
    void EnemyPool_change()
    {
        EnemyPool_present = EnemyPools[Stage_num];
    }
    public void EnemyPool_change_Cheat()
    {
        //Debug.Log("aaaa");
        //Stage_num += 1;
        //Spawm_Num = 14;
        //EnemyPool_present = EnemyPools[Stage_num];
    }

    IEnumerator EnemySpawnStart()
    {
        int typeNum = EnemyPool_present.GetComponent<EnemyPool>().Enemy.Length;
        for (int a = 0; a < typeNum; a++)
        {
            for (int i = 0; i < EnemyPool_present.GetComponent<EnemyPool>().SpawnValue[a]; i++)
            {
                if (GameManager.enemyCount <= GameManager.enemyCount_Max)
                {
                    GameObject Enemy = Instantiate(EnemyPool_present.GetComponent<EnemyPool>().Enemy[a], 
                                                   GetRandomPosition(180f), Quaternion.identity);
                    Enemy.transform.SetParent(ObjectPoolManager.transform, true);
                    GameManager.enemyCount += 1;
                }
            }
        }

        yield return new WaitForSeconds(2.0f); //   2초씩 15번 = 30초 즉, 30초당 몬스터 풀이 변경됨.
        NextSpawn();
    }

    //스폰할 위치 결정
    public Vector3 GetRandomPosition(float range)
    {
        float radius = range;
        //스폰할 위치 기준이 어디?
        //플레이어 주위 원 반경 밖에서 랜덤 스폰할 거
        //근데 스포너는 캐릭터를 따라다니고 있음 -> 캐릭터 위치 = 스포너 위치
        //즉 스포너 위치를 캐릭터 위치 대신 써도 된다.
        Vector3 SpawnerPosition = transform.position;

        //랜덤 위치를 정하려면 원 반지름 길이만 알면 되는게 아니라
        //어디를 기준으로 반지름 길이 만큼 밖에서 스폰할것인지?
        //스포너의 현 위치를 기준으로 반지름 만큼 떨어진 위치에서 랜덤 생성
        float anchorPosX = SpawnerPosition.x;
        float anchorPosY = SpawnerPosition.y;

        //랜덤 생성 위치의 수평 지점 계산
        //anchorPosX에서 반지름 만큼 빼거나 더한 값이
        //랜덤스폰할 좌표의 x좌표
        float x = Random.Range(-radius + anchorPosX, radius + anchorPosX);

        //랜덤 생성 위치의 수직 지점 계산
        //Mathf.Sqrt : 제곱근 반환
        //Mathf.Pow(A,B)  : A의 B승 반환
        float y_b = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(x - anchorPosX, 2));

        //y_b에 곱할건데 뭘 곱하냐 -1 또는 1을 곱할거
        //Random.Range(0, 2) -> 0 또는 1이 나오게 함.
        //삼항연산자는 Random.Range(0, 2)통해 나온 값이 0이냐? 라고 묻는것.
        //0이 나오면 참이므로 -1을 y_b에 곱하는 것이고
        //1이 나오면 거짓이므로 1을 y_b에 곱하는 것.
        //왜 이렇게 하느냐?
        //몬스터가 위에서만 나오는 법이 없으므로 아래에서도 나오게 하기 위해서
        y_b *= Random.Range(0, 2) == 0 ? -1 : 1;

        //랜덤스폰할 좌표의 y좌표
        float y = y_b + anchorPosY;

        Vector3 randomPosition = new Vector3(x, y*0.77f, 0);

        return randomPosition;
    }

}
