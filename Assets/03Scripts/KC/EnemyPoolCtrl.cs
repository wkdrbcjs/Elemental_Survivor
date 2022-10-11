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

    //private bool SpawnApprove; //�����ص� �ȴٴ� ���� ����

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

        // ���� Ǯ 1�� �� 15�� ���� -> ���� ���� Ǯ�� ����
        if (Spawm_Num % 15 == 0)
        {
            Stage_num += 1;
            EnemyPool_change();
        }

        StartCoroutine(EnemySpawnStart());
    }

    //  ���� ���� Ǯ�� ����
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

        yield return new WaitForSeconds(2.0f); //   2�ʾ� 15�� = 30�� ��, 30�ʴ� ���� Ǯ�� �����.
        NextSpawn();
    }

    //������ ��ġ ����
    public Vector3 GetRandomPosition(float range)
    {
        float radius = range;
        //������ ��ġ ������ ���?
        //�÷��̾� ���� �� �ݰ� �ۿ��� ���� ������ ��
        //�ٵ� �����ʴ� ĳ���͸� ����ٴϰ� ���� -> ĳ���� ��ġ = ������ ��ġ
        //�� ������ ��ġ�� ĳ���� ��ġ ��� �ᵵ �ȴ�.
        Vector3 SpawnerPosition = transform.position;

        //���� ��ġ�� ���Ϸ��� �� ������ ���̸� �˸� �Ǵ°� �ƴ϶�
        //��� �������� ������ ���� ��ŭ �ۿ��� �����Ұ�����?
        //�������� �� ��ġ�� �������� ������ ��ŭ ������ ��ġ���� ���� ����
        float anchorPosX = SpawnerPosition.x;
        float anchorPosY = SpawnerPosition.y;

        //���� ���� ��ġ�� ���� ���� ���
        //anchorPosX���� ������ ��ŭ ���ų� ���� ����
        //���������� ��ǥ�� x��ǥ
        float x = Random.Range(-radius + anchorPosX, radius + anchorPosX);

        //���� ���� ��ġ�� ���� ���� ���
        //Mathf.Sqrt : ������ ��ȯ
        //Mathf.Pow(A,B)  : A�� B�� ��ȯ
        float y_b = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(x - anchorPosX, 2));

        //y_b�� ���Ұǵ� �� ���ϳ� -1 �Ǵ� 1�� ���Ұ�
        //Random.Range(0, 2) -> 0 �Ǵ� 1�� ������ ��.
        //���׿����ڴ� Random.Range(0, 2)���� ���� ���� 0�̳�? ��� ���°�.
        //0�� ������ ���̹Ƿ� -1�� y_b�� ���ϴ� ���̰�
        //1�� ������ �����̹Ƿ� 1�� y_b�� ���ϴ� ��.
        //�� �̷��� �ϴ���?
        //���Ͱ� �������� ������ ���� �����Ƿ� �Ʒ������� ������ �ϱ� ���ؼ�
        y_b *= Random.Range(0, 2) == 0 ? -1 : 1;

        //���������� ��ǥ�� y��ǥ
        float y = y_b + anchorPosY;

        Vector3 randomPosition = new Vector3(x, y*0.77f, 0);

        return randomPosition;
    }

}
