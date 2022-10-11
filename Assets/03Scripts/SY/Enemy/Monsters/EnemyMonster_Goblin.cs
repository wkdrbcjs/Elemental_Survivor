using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMonster_Goblin : EnemyMonster
{   
    public bool GoblinMoveAble = false;
    protected Vector3 originPos;

    void Start()
    {
        base.Start();
        isSuperArmor = true;
    }

    //  초기화 후 다시 활성화 되었을 떄
    private void OnEnable()
    {
        //  처음 생성 위치 기억
        //originPos = transform.position;
        //  타겟 객체 정보를 잊어버렸을 수도 있으므로 다시 재할당
        TraceTarget = PlayerCtrl.playerInstance;
        if (TraceTarget == null)
        {
            TraceTarget = GameObject.FindGameObjectWithTag("Player");
        }

        //  객체가 바라볼 방향 갱신
        if (TraceTarget.transform.position.x > this.gameObject.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        GoblinMoveAble = true;
        int childCount = transform.childCount;  // 자식으로 생성되는 폰트들 삭제 위함.    
        for (int i = 2; i < childCount; i++)    // 혹시 모를 지워지지 않은 폰트 삭제
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    new void FixedUpdate()
    {
        GoblinMove();
        base.FixedUpdate();
    }

    // 고블린 이동
    public void GoblinMove()
    {
        toPcVec = new Vector3
                (TraceTarget.transform.position.x - transform.parent.position.x,
                TraceTarget.transform.position.y - transform.parent.position.y,
                0);

        if (GoblinMoveAble)
        {
            if (!isDead)
            {
                transform.position += toPcVec.normalized * movementSpeed * Time.deltaTime;
            }
        }
    }

    //  고블린 초기화
    public void GoblinNonActive()
    {
        Destroy(font);
        SprRend.color = new Color(255, 255, 255, 255);
        HP = maxHP;
        isDead = false;
        anim.SetBool("isDead", false);
        GoblinMoveAble = false;
        movementSpeed = baseMovementSpeed;
        BoxColliderOnOff(true);
        gameObject.SetActive(false);
    }
}
