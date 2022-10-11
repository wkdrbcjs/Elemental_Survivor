using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HailStorm : Projectile
{
    
    private PlayerCtrl playerCtrl;

    int Num;


    public float duration;
    public float cooltime;

    public GameObject[] ProjectileImage = new GameObject[20];
    public GameObject ProjectileEnd;

    public float lifetime = 4.0f;
    public float freezeTime;
    public bool activateMassiveEnergy = false;
    public float damageCycle = 0.5f;
    public float freezeChance;
    public bool activateExtraAttack = false;
    // Start is called before the first frame update
    void Start()
    {
        playerCtrl = PlayerCtrl.playerInstance.GetComponent<PlayerCtrl>();

        //�� ����//
        defaultInit();

        duration += baseDuration;
        cooltime += baseCooltime;

        // ���� //
        if (activateMassiveEnergy)
        {
            projectileScale *= 2;
        }
        transform.localScale = new Vector3(projectileScale, projectileScale, 1);

        
        System.Array.Resize(ref monster, 1000);

        for (int i = 0; i < 20; i++)
        {
            if (ProjectileImage[i] != null)
            {
                //ProjectileImage[i].
                
                StartCoroutine(ImageSet(ProjectileImage[i]));
            }
        }
        Num = (int)lifetime -1;
        StartCoroutine(_SM.SoundPlayLoop(_SM.Water_WaterClass, 0.1f, Num * 2 -1, 0.75f));
        StartCoroutine(StartDuration(lifetime));
    }

    // Update is called once per frame

    IEnumerator StartDuration(float lifetime)
    {
        
        yield return new WaitForSeconds(lifetime);

        if (activateExtraAttack)
        {
            for (int i = 0; i < 10; i++)
            {
                Instantiate(ProjectileEnd, this.transform.position, Quaternion.identity);
                _SM.SoundPlay(_SM.Water_WaterClassAdd);
                yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
            }
            //var instance = 

                //instance.GetComponent<HailStorm>().activateMassiveEnergy = activateMassiveEnergy;
  
        }
        Destroy(this.gameObject);

    }

    IEnumerator ImageSet(GameObject projImg)
    {
        yield return new WaitForSeconds(Random.Range(0f,2f));
        projImg.SetActive(true);
        projImg.GetComponent<Animator>().speed = (1.5f - damageCycle);
    }
 
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (hit == false)
        {
            hit = true;
            if (collision.gameObject.CompareTag("Enemy"))
            {
                if (penetration > 0)
                {
                    int index = System.Array.IndexOf(monster, collision.gameObject);//���� �迭�� �ǰݵ� ���Ͱ� ����ִ� �� �˻�
                    if (index == -1)//�迭�� �ش� ���Ͱ� ���� ��
                    {
                        monster[monsterSize] = collision.gameObject; //�迭�� �ش� ���͸� ���� ����
                        monsterSize += 1;

                        StartCoroutine(TargetActive(monsterSize));
                        Vector2 knockBackVec = new Vector2(transform.position.x - collision.transform.position.x, transform.position.y - collision.transform.position.y);


                        //���� ��ũ��Ʈ�� �������� �˹�Ÿ��� �����ϸ� ���� �ǰ�ó�� ����
                        collision.gameObject.GetComponent<EnemyCtrl>().Hit(this.gameObject, (int)attackDamage, knockBack);
                        if (Random.Range(0.0f,1.0f)<(0.1f))
                        {
                            collision.gameObject.GetComponent<EnemyCtrl>().StartCoroutine(collision.gameObject.GetComponent<EnemyCtrl>().MonsterFreeze(freezeTime));
                        }
                    }
                }
            }
            hit = false;
        }
    }
    IEnumerator TargetActive(int a) //�ǰ� ������ �����ϴ� �Լ�
    {
        yield return new WaitForSeconds(damageCycle);
        monster[a - 1] = null; //���� �迭���� �ش� ���͸� ������ �ٽ� �ǰݵ� �� �ְ� ����
    }
}
