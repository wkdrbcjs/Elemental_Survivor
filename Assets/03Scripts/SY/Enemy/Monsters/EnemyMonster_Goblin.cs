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

    //  �ʱ�ȭ �� �ٽ� Ȱ��ȭ �Ǿ��� ��
    private void OnEnable()
    {
        //  ó�� ���� ��ġ ���
        //originPos = transform.position;
        //  Ÿ�� ��ü ������ �ؾ������ ���� �����Ƿ� �ٽ� ���Ҵ�
        TraceTarget = PlayerCtrl.playerInstance;
        if (TraceTarget == null)
        {
            TraceTarget = GameObject.FindGameObjectWithTag("Player");
        }

        //  ��ü�� �ٶ� ���� ����
        if (TraceTarget.transform.position.x > this.gameObject.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        GoblinMoveAble = true;
        int childCount = transform.childCount;  // �ڽ����� �����Ǵ� ��Ʈ�� ���� ����.    
        for (int i = 2; i < childCount; i++)    // Ȥ�� �� �������� ���� ��Ʈ ����
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    new void FixedUpdate()
    {
        GoblinMove();
        base.FixedUpdate();
    }

    // ��� �̵�
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

    //  ��� �ʱ�ȭ
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
