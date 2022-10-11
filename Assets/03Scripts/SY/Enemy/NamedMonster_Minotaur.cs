using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamedMonster_Minotaur : NamedMonster
{
    void Start()
    {
        base.Start();
    }

    // 미노타우로스 일반 공격
    protected override IEnumerator NamedMonster_NormalAttack()
    {
        movementSpeed = 0f;
        yield return new WaitForSeconds(0.1f);
        attackCoolTime = CoolTime;
        anim.SetBool("Move", false);
        anim.SetBool("isDuringAnim", true);
        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(0.7f);
        
        if (!isDead)
        {
            for (int i = 0; i < 3; i++)
            {
                toPcVec = new Vector3
                    (TraceTarget.transform.position.x - transform.position.x,
                    TraceTarget.transform.position.y - transform.position.y, 0);

                Quaternion angleAxis1 = Quaternion.Euler(0, 0, Mathf.Atan2(toPcVec.normalized.y, toPcVec.normalized.x) * Mathf.Rad2Deg);
                //  TileSpawnPos는 미노타우르스 객체 자식으로 있는 투사체의 생성위치 객체임.
                Instantiate(attackProjectile, TileSpawnPos.transform.position, angleAxis1);
                yield return new WaitForSeconds(0.2f);
            }
        }

        yield return new WaitForSeconds(0f);
        if (isMoveAble) //  플레이어 추격 가능한 상태
        {
            anim.SetBool("Move", true);
        }
        else  
        {}
    }

    // 미노타우로스 특수 공격
    protected override IEnumerator NamedMonster_SpecialPattern()
    {
        if (SelectingObj == null)
        {
            SelectingObj = Instantiate(attackProjectile_1, TraceTarget.transform.position, Quaternion.identity);
        }
        //  위치 옮겨주고 활성화 해주면 
        //  벽 객체 스스로 일정 시간후 비활성화 될 것
        if (!isDead)
        {
            SelectingObj.transform.position = TraceTarget.transform.position;
            SelectingObj.SetActive(true);
        }
        yield return new WaitForSeconds(0f);
    }

}
