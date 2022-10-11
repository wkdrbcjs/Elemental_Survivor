using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public GameManager GameManager;
    public static ObjectPoolManager instance;
    public List<GameObject> enemyObjs;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        GameManager = GameManager.instance;
        StartCoroutine(ObjectPooling());
    }

    //void Update(){ }

    IEnumerator ObjectPooling()
    {
        if (enemyObjs.Count > 0)
        {
            for (int i = 0; i < enemyObjs.Count; i++)
            {
                if (GameManager.enemyCount <= GameManager.enemyCount_Max)
                {
                    //  객체 활성화
                    enemyObjs[i].SetActive(true);

                    // 위치 설정
                    enemyObjs[i].transform.position = enemyObjs[i].GetComponent<EnemyCtrl>().GetRandomPosition(180f);

                    //  리스트에서 제외 시키기
                    enemyObjs.Remove(enemyObjs[0]);

                    //  게임매니저에 몬스터 수 변수 증가
                    GameManager.enemyCount += 1;

                }
                yield return new WaitForSeconds(0.1f);
            }
        }

        yield return new WaitForSeconds(0.2f);
        StartCoroutine(ObjectPooling());
    }
}
