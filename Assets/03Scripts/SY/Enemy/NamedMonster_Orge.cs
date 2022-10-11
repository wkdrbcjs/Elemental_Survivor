using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamedMonster_Orge : NamedMonster
{
    void Start()
    {
        base.Start();
    }

    // 오우거 특수 공격 - 고블린 무리 스포너
    protected override IEnumerator NamedMonster_SpecialPattern()
    {
        if (SelectingObj == null)
        {
            SelectingObj = Instantiate(attackProjectile_1, TraceTarget.transform.position, Quaternion.identity);
        }
        //  고블린 스포너 객체의 위치 변경 및 고블린들 활성화
        if (!isDead)
        {
            SelectingObj.transform.position = GetRandomPosition(150f);
            yield return new WaitForSeconds(0.1f);
            SelectingObj.GetComponent<TileSpwner>().GoblinActiveTrue(true);

            //  고블린 활성화 되어 있는 시간 7초
            yield return new WaitForSeconds(7f);
            SelectingObj.GetComponent<TileSpwner>().GoblinActiveTrue(false);

            for (int i = 0; i < SelectingObj.GetComponent<TileSpwner>().TileCount; i++)
            {
                //  고블린 객체들은 비활성화 상태이기 때문에 GetComponentsInChildren로 정보를 가져올 수 없음.
                //  그 이유는 ()안의 bool 인자값이 false가 기본값으로 설정되어 있기 때문.
                //  그래서 true를 인자값으로 주면 비활성화된 자식객체의 정보도 가져올 수 있음.
                SelectingObj.GetComponentsInChildren<EnemyMonster_Goblin>(true)[i].GoblinNonActive();
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(1f);
        }
    }
    // 오우거 기본 공격 - 스턴 장판
    protected override IEnumerator NamedMonster_NormalAttack()
    {
        if (!isDead)
        {
            Instantiate(attackProjectile, TraceTarget.transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(1f);
    }

}
