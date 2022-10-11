using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HailStormEnd : MonoBehaviour
{
    private PlayerCtrl playerObject;

    private GameObject[] monster;
    int a = 0;
    bool hit;


    //����ü �⺻ ����//
    public int baseAttackDamage;
    public float baseKnockBack;
    public int basePenetration;
    public float baseProjectileScale;
    public float baseProjectileSpeed;
    public float baseDuration;
    public float baseCooltime;
    //����ü ���� ����//
    public int attackDamage;
    public float knockBack;
    public int penetration;
    public float projectileSpeed;
    public float projectileScale;
    public float duration;
    public float cooltime;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = PlayerCtrl.playerInstance.GetComponent<PlayerCtrl>();

        //�� ����//
        attackDamage = baseAttackDamage * (100 + PlayerStatus.instance.getAttackDamage()) / 100;
        knockBack = baseKnockBack * PlayerStatus.instance.getKnockBack();
        penetration = basePenetration;// + PC.penetration;
        projectileSpeed = baseProjectileSpeed;// * PC.projectileSpeed;
        projectileScale = baseProjectileScale;// * PC.projectileScale;
        duration = baseDuration;
        cooltime = baseCooltime;
        // ���� //
        transform.localScale = new Vector3(projectileScale, projectileScale, 1);

        System.Array.Resize(ref monster, 1000);

        transform.position += new Vector3(Random.Range(-50, 50), Random.Range(-40, 40), 0);
    }

    // Update is called once per frame

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (hit == false)
        {
            hit = true;
            if (collision.gameObject.CompareTag("Enemy"))
            {
                int index = System.Array.IndexOf(monster, collision.gameObject);
                if (index == -1)
                {
                    monster[a] = collision.gameObject;
                    a += 1;

                    //���� ��ũ��Ʈ�� �������� �˹�Ÿ��� �����ϸ� ���� �ǰ�ó�� ����//
                    collision.gameObject.GetComponent<EnemyCtrl>().Hit(this.gameObject, attackDamage, knockBack);
                    collision.gameObject.GetComponent<EnemyCtrl>().StartCoroutine(collision.gameObject.GetComponent<EnemyCtrl>().MonsterFreeze(2.0f));
                }
            }
            hit = false;
        }
    }
    public IEnumerator ColisionOn()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        GetComponent<BoxCollider2D>().enabled = false;
    }
    void DestroyEffect()
    {
        Destroy(this.gameObject);
    }
}
