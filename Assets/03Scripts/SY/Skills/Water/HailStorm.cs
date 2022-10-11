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

        //값 세팅//
        defaultInit();

        duration += baseDuration;
        cooltime += baseCooltime;

        // 적용 //
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
                    int index = System.Array.IndexOf(monster, collision.gameObject);//몬스터 배열에 피격된 몬스터가 들어있는 지 검사
                    if (index == -1)//배열에 해당 몬스터가 없을 때
                    {
                        monster[monsterSize] = collision.gameObject; //배열에 해당 몬스터를 집어 넣음
                        monsterSize += 1;

                        StartCoroutine(TargetActive(monsterSize));
                        Vector2 knockBackVec = new Vector2(transform.position.x - collision.transform.position.x, transform.position.y - collision.transform.position.y);


                        //몬스터 스크립트에 데미지와 넉백거리를 전달하며 몬스터 피격처리 실행
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
    IEnumerator TargetActive(int a) //피격 간격을 설정하는 함수
    {
        yield return new WaitForSeconds(damageCycle);
        monster[a - 1] = null; //몬스터 배열에서 해당 몬스터를 지워서 다시 피격될 수 있게 설정
    }
}
