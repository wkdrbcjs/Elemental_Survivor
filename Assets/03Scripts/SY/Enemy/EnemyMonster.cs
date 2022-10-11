using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMonster : EnemyCtrl
{
    // 오브젝트 풀링 후 start가 호출 되지 않기 때문에 OnEnable 사용
    private void OnEnable()
    {
        if (isObjectpool)
        {
            TraceTarget = PlayerCtrl.playerInstance;
            isStateBattle = true;
            StartCoroutine(Ability_Cool());
        }
    }

    protected override void Start()
    {
        base.Start();
        isStateBattle = true;
        StartCoroutine(Ability_Cool());
    }

    new void FixedUpdate()
    {
        Trace();
        base.FixedUpdate();
    }

    #region Ability_Cool
    protected IEnumerator Ability_Cool()
    {
        Vector2 m_distance = transform.position - TraceTarget.transform.position;
        float m_x = m_distance.x;
        float m_y = m_distance.y;
        if (m_x < 0)
        {
            m_x *= -1;
        }
        if (m_y < 0)
        {
            m_y *= -1;
        }
        if (m_distance.magnitude <= attackRange)
        {
            attackCoolTime -= 1;
            if (attackCoolTime <= 0 && !isDead && !isFreezed)
            {
                switch (EnemyID)
                {
                    case 0:
                        break;

                    case 1:
                        break;

                    case 2: //골렘
                        StartCoroutine(Attack_Charge());
                        break;

                    case 3: // spiked 슬라임, 임프, 셰이드 - 투사체 1개
                        StartCoroutine(Attack_ProjectileSolo());
                        break;

                    case 4: // tentacle 슬라임 - 투사체 3개
                        StartCoroutine(Attack_ProjectileThree());
                        break;

                    case 5:
                        break;

                }
                attackCoolTime = CoolTime;
            }
        }

        //  플레이어로부터 너무 멀어지면 새로운 랜덤 위치로 이동
        else if (m_x >= 190 || m_y >= 190)
        {
            transform.position = GetRandomPosition(170f);
        }
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(Ability_Cool());
    }
    #endregion



    #region Enemy
    // 골렘
    IEnumerator Attack_Charge()
    {
        movementSpeed = 0.0f;
        isSuperArmor = true;
        anim.SetTrigger("Ready");

        yield return new WaitForSeconds(0.7f);

        toPcVec = new Vector3(TraceTarget.transform.position.x - transform.position.x, TraceTarget.transform.position.y - transform.position.y, 0);

        yield return new WaitForSeconds(0.3f);

        isAttack = true;
        anim.SetTrigger("Attack");
        anim.SetBool("isDuringAnim", true);

        yield return new WaitForSeconds(0.4f);

        isSuperArmor = false;
        isAttack = false;
        movementSpeed = baseMovementSpeed;
    }

    // spiked 슬라임, 임프, 셰이드
    IEnumerator Attack_ProjectileSolo()
    {
        movementSpeed = 0.0f;

        isSuperArmor = true;

        yield return new WaitForSeconds(0.2f);
        attackCoolTime = 5;
        anim.SetTrigger("Attack");
        anim.SetBool("isDuringAnim", true);

        yield return new WaitForSeconds(0.9f);
        toPcVec = new Vector3
            (TraceTarget.transform.position.x - transform.position.x,
            TraceTarget.transform.position.y - transform.position.y, 0);

        Quaternion angleAxis1 = Quaternion.Euler(0, 0, Mathf.Atan2(toPcVec.normalized.y, toPcVec.normalized.x) * Mathf.Rad2Deg);
        if (!isDead)
        {
            Instantiate(attackProjectile, transform.position, angleAxis1);
        }

        yield return new WaitForSeconds(0.1f);
        isSuperArmor = false;
        movementSpeed = baseMovementSpeed;
    }

    //  Tentacle 슬라임
    IEnumerator Attack_ProjectileThree()
    {
        movementSpeed = 0.0f;

        isSuperArmor = true;

        yield return new WaitForSeconds(0.2f);
        attackCoolTime = 5;
        anim.SetTrigger("Attack");
        anim.SetBool("isDuringAnim", true);

        yield return new WaitForSeconds(0.9f);
        toPcVec = new Vector3
            (TraceTarget.transform.position.x - transform.position.x,
            TraceTarget.transform.position.y - transform.position.y, 0);


        for (int i = -1; i < 2; i++)
        {
            float plusAngle = i * 15;
            Quaternion angleAxis = Quaternion.Euler(0, 0, Mathf.Atan2(toPcVec.normalized.y, toPcVec.normalized.x) * Mathf.Rad2Deg + plusAngle);

            if (!isDead)
            {
                Instantiate(attackProjectile, transform.position, angleAxis);
            }
        }

        yield return new WaitForSeconds(0.1f);
        isSuperArmor = false;
        movementSpeed = baseMovementSpeed;
    }



    #endregion


}
