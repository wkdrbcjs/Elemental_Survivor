using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMonster_Mimic : EnemyMonster
{

    //  미믹 변신
    IEnumerator MimicTrans()
    {
        anim.SetBool("Trans", true);
        yield return new WaitForSeconds(0.2f);
        transform.tag = "Enemy";
        anim.SetBool("Trans", false);

        yield return new WaitForSeconds(0.5f);
        //  변신 했으니깐 꺼져 있던 박스 콜라이더 켜주기
        BoxColliderOnOff(true);

        //  변신했으니깐 이동하기 위해 이동속도 변수 초기화
        movementSpeed = baseMovementSpeed;
    }

    protected override void distanceCheck()
    {
        if (isTrans == false)
        {
            //  미믹은 골렘과 다르게 플레이어의 실시간 위치를 추적해서 일정거리 내에 들어오면 변신함.
            toPcVec = new Vector3
            (TraceTarget.transform.position.x - transform.position.x,
            TraceTarget.transform.position.y - transform.position.y,
            0);

            if (Vector3.Distance(transform.position, TraceTarget.transform.position) <= attackRange)
            {
                StartCoroutine(MimicTrans());
                isTrans = true;
            }
        }
    }


    protected override void CheckDropRate()
    {
        Instantiate(MonsterManager.instance.ItemChestOBJ, transform.position, transform.rotation);
    }
}
