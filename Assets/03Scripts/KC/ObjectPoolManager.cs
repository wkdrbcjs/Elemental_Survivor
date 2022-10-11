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
                    //  ��ü Ȱ��ȭ
                    enemyObjs[i].SetActive(true);

                    // ��ġ ����
                    enemyObjs[i].transform.position = enemyObjs[i].GetComponent<EnemyCtrl>().GetRandomPosition(180f);

                    //  ����Ʈ���� ���� ��Ű��
                    enemyObjs.Remove(enemyObjs[0]);

                    //  ���ӸŴ����� ���� �� ���� ����
                    GameManager.enemyCount += 1;

                }
                yield return new WaitForSeconds(0.1f);
            }
        }

        yield return new WaitForSeconds(0.2f);
        StartCoroutine(ObjectPooling());
    }
}
