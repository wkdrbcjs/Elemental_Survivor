using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpwner : MonoBehaviour
{
    public GameObject TraceTarget;
    public GameObject SpawnTile;
    public int TileType;
    public int TileCount;

    public List<GameObject> goblinList;
   
    void Start()
    {
        TraceTarget = PlayerCtrl.playerInstance;
        if (TraceTarget == null)
        {
            TraceTarget = GameObject.FindGameObjectWithTag("Player");
        }


        if (TileType == 1)  //  Minotaur
        {
            StartCoroutine(TileSpawn1());
        }
        if (TileType == 2) //   PhantomKnight
        {
            StartCoroutine(TileSpawn2());
        }
        if(TileType == 4)   //  Ogre
        {
            //StartCoroutine(TileSpawn4());
        }
    }
    void Update()
    {
        if (TileType == 2)
        {
            transform.position = TraceTarget.transform.position;
        }
    }
    #region MinotaurAttack_ProjectTile
    IEnumerator TileSpawn1()        //  �̳�Ÿ�츣�� ����ü
    {
        //  �������� SpawnTile�� ��ġ`
        for (int i = 0; i < TileCount; i++)
        {
            float plusAngle = i * 360f / TileCount;

            Quaternion angleAxis = Quaternion.Euler(
                0, 0, 0 * Mathf.Rad2Deg + plusAngle);

            GameObject tile = Instantiate(SpawnTile, transform.position, angleAxis);

            //  SetParent - true�� ���� -> �θ� �� ��ü�� transform�� ���� �� ����.
            //  SetParent - false ����  -> �θ� �� ��ü�� transform�� ���� ����.
            tile.transform.SetParent(transform, true);
        }
        yield return new WaitForSeconds(0.0f);
    }
    #endregion

    #region PhantomKnightAttack_ProjectTile
    IEnumerator TileSpawn2()    //  ���ҳ���Ʈ ����ü
    {
        GameObject[] tile = new GameObject[TileCount];

        for (int i = 0; i < TileCount; i++)
        {
            float plusAngle = i * 360f / TileCount;

            Quaternion angleAxis = Quaternion.Euler(
                0, 0, 0 * Mathf.Rad2Deg + plusAngle);

            Vector3 originalRotationInVector3 = angleAxis.eulerAngles;
            tile[i] = Instantiate(SpawnTile, transform.position, angleAxis, transform);
            tile[i].transform.position = transform.position + tile[i].transform.TransformDirection(Vector2.right) * 65;
            tile[i].transform.rotation = Quaternion.Euler(0, 0, 0 * Mathf.Rad2Deg + plusAngle + 180); ;
            
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.3f);

        for (int i = 0; i < TileCount; i++)
        {
            tile[i].GetComponent<EnemyProjectile>().On = true;
            tile[i].transform.parent = null;
        }
        yield return new WaitForSeconds(0.0f);
    }
    #endregion

    #region OgreAttack_GoblinDummy
    IEnumerator TileSpawn4()
    {
        //  ���
        //  ����Ű� �� ��� �����ʸ� �����ϸ�
        //  �� ��� �����ʿ����� ������� ��������.
        //  ù ��°���� ������ �ϰ�
        //  �� ���� ���� Ÿ�ӿ����� GoblinActiveTrue�Լ��� ȣ������ ����

        //  ��� ����
        /*for (int i = 0; i < TileCount; i++)
        {
            GameObject tile = Instantiate(SpawnTile, transform.position, Quaternion.identity);
            tile.transform.SetParent(transform, true);
            tile.GetComponent<EnemyMonster_Goblin>().GoblinMoveAble = false;
            goblinList.Add(tile);
        }*/
        yield return new WaitForSeconds(0.0f);
    }

    //  NamedMonster_Orge.cs���� ȣ��
    public void GoblinActiveTrue(bool TorF)
    {
        //  30������ ����� ��ġ�� ������
        for (int i = 0; i < TileCount; i++) 
        {
            if(goblinList[i]!=null)
            {
                goblinList[i].transform.position = GetRandomPosition();
            }
        }
        //  ��� Ȱ��ȭ
        if (TorF) //TorF==true
        {
            for (int k = 0; k < TileCount; k++)
            {
                goblinList[k].SetActive(TorF);
            }
        }
        else // TorF = false;
        {
            for (int k = 0; k < TileCount; k++)
            {
                goblinList[k].SetActive(TorF);
            }
        }
    }
    #endregion

    #region GetRandomPosition
    public Vector3 GetRandomPosition()
    {
        float radius = 30f;
        Vector3 SpawnerPosition = transform.position; //��� �������� ������

        float anchorPosX = SpawnerPosition.x;
        float anchorPosY = SpawnerPosition.y;

        float x = Random.Range(-radius + anchorPosX, radius + anchorPosX);
        float y = Random.Range(-radius + anchorPosY, radius + anchorPosY);

        Vector3 randomPosition = new Vector3(x, y, 0);

        return randomPosition;
    }
    #endregion

}
