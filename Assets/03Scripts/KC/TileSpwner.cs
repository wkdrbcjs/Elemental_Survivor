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
    IEnumerator TileSpawn1()        //  미노타우르스 투사체
    {
        //  원형으로 SpawnTile을 배치`
        for (int i = 0; i < TileCount; i++)
        {
            float plusAngle = i * 360f / TileCount;

            Quaternion angleAxis = Quaternion.Euler(
                0, 0, 0 * Mathf.Rad2Deg + plusAngle);

            GameObject tile = Instantiate(SpawnTile, transform.position, angleAxis);

            //  SetParent - true로 설정 -> 부모가 될 객체의 transform에 영향 안 받음.
            //  SetParent - false 설정  -> 부모가 될 객체의 transform에 영향 받음.
            tile.transform.SetParent(transform, true);
        }
        yield return new WaitForSeconds(0.0f);
    }
    #endregion

    #region PhantomKnightAttack_ProjectTile
    IEnumerator TileSpawn2()    //  팬텀나이트 투사체
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
        //  고블린
        //  오우거가 이 고블린 스포너를 생성하면
        //  이 고블린 스포너에서는 고블린들을 생성해줌.
        //  첫 번째때는 생성만 하고
        //  그 다음 공격 타임에서는 GoblinActiveTrue함수를 호출해줄 것임

        //  고블린 생성
        /*for (int i = 0; i < TileCount; i++)
        {
            GameObject tile = Instantiate(SpawnTile, transform.position, Quaternion.identity);
            tile.transform.SetParent(transform, true);
            tile.GetComponent<EnemyMonster_Goblin>().GoblinMoveAble = false;
            goblinList.Add(tile);
        }*/
        yield return new WaitForSeconds(0.0f);
    }

    //  NamedMonster_Orge.cs에서 호출
    public void GoblinActiveTrue(bool TorF)
    {
        //  30마리의 고블린의 위치를 재조정
        for (int i = 0; i < TileCount; i++) 
        {
            if(goblinList[i]!=null)
            {
                goblinList[i].transform.position = GetRandomPosition();
            }
        }
        //  고블린 활성화
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
        Vector3 SpawnerPosition = transform.position; //고블린 스포너의 포지션

        float anchorPosX = SpawnerPosition.x;
        float anchorPosY = SpawnerPosition.y;

        float x = Random.Range(-radius + anchorPosX, radius + anchorPosX);
        float y = Random.Range(-radius + anchorPosY, radius + anchorPosY);

        Vector3 randomPosition = new Vector3(x, y, 0);

        return randomPosition;
    }
    #endregion

}
