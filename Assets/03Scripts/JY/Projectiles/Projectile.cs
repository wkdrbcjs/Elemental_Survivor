using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public SkillManagement _skillManagement;
    public SoundManager _SM;
    //public ItemInfoSet ItemInfoSet.instance;
    public int ElementClass = 0; //0�븻 1�� 2�� 3�ٶ� 4�� 5����
    //����ü �Ӽ� ����//
    public int ProjectileClass = 0; //0�븻 1�� 2�� 3�ٶ� 4�� 5����
    public bool BurnOn = false;
    public int BurnDamage = 10;

    public bool EarthOn = false;
    public int EarthAddDamage = 0;

    public bool WindOn = false;
    public int WindAddDamage = 0;

    public bool FreezeOn = false;
    public int FreezeDamage = 20;

    public bool ParalyzeOn = false;
    public int ParalyzeDamage = 2;

    public GameObject enemyParalyzer;
    public GameObject enemyBurner;

    public List<GameObject> FoundMonsters;    
    public GameObject Monster;
    public float shortDis; 

    protected GameObject _player;
    protected PlayerStatus _playerstatus;

    protected float angle;
    protected Vector2 target, _target;
    protected Vector2 Direction;

    protected Transform FirePos;
    protected Transform FirePosPivot;

    [Header("����")]
    public AudioSource Sound;
    public AudioClip Snd_FireMagic;
    public AudioClip Snd_WindMagic;
    public AudioClip Snd_EarthMagic;
    public AudioClip Snd_WaterMagic;
    public AudioClip Snd_ThunderMagic;

    //ġ��Ÿ ����
    [SerializeField] protected GameObject[] monster;
    [SerializeField] protected int monsterSize = 1;
    [SerializeField] protected bool hit = false;
    //private GameObject[] monster;
    //int a = 0;
    //bool hit;


    //����ü �⺻ ����//
    [SerializeField] public float baseAttackDamage;
    [SerializeField] public float baseKnockBack;
    [SerializeField] public int basePenetration;
    [SerializeField] public float baseProjectileScale;
    [SerializeField] public float baseProjectileSpeed;
    [SerializeField] public float baseDuration;
    [SerializeField] public float baseCooltime;
    [SerializeField] public int ProjectileNum;
    [SerializeField] public int Percentage;
    [SerializeField] public int AttackCount;

    //����ü ���� ����//
    [SerializeField] public float attackDamage;
    [SerializeField] public float knockBack;
    [SerializeField] public int penetration;
    [SerializeField] public float projectileSpeed;
    [SerializeField] public float projectileScale;

    public float criticalChance = 0.0f;//~1.0f;
    public float criticalDamage = 1.5f;

    public Color DamageColor = new Color(255, 255, 100);
    public Color CriticalColor = new Color(180, 180, 75);

    private void Awake()
    {
        monster = new GameObject[30];
        _skillManagement = SkillManagement.instance;
        ItemInfoSet.instance = GameObject.Find("ItemList").GetComponent<ItemInfoSet>();
        _player = PlayerCtrl.playerInstance;
        _playerstatus = PlayerStatus.instance;
        //FirePosPivot = _player.GetComponentsInChildren<Transform>()[2].transform;
        //FirePos = FirePosPivot.GetComponentInChildren<Transform>().transform;
        _SM = SoundManager.instance;
        SetElemetalClass();
    }
    void SetElemetalClass()
    {
        if(_skillManagement.ElementClass == "Fire")
        {
            ElementClass = 1;
            if (ProjectileClass == 1)
            {
                BurnOn = true;
                BurnDamage = ((int)(5 * (100 + PlayerStatus.instance.getAttackDamage()) * 0.01f));
                if (_playerstatus.classLevel >= 5)
                {
                    BurnDamage = ((int)(10 * (100 + PlayerStatus.instance.getAttackDamage()) * 0.01f));
                }
            }
                
        }

        if (_skillManagement.ElementClass == "Earth")
        {
            ElementClass = 2;
            if (ProjectileClass == 2)
            {
                EarthOn = true;
                //EarthAddDamage += (int)(baseAttackDamage * _playerstatus.getMaxHP() * 0.001f);
            }
            
        }

        if (_skillManagement.ElementClass == "Wind")
        {
            ElementClass = 3;
            if (ProjectileClass == 3)
            {
                WindOn = true;
                //WindAddDamage += (int)(baseAttackDamage * (_playerstatus.getMovementSpeed()-20) * 0.02f);
            }
        }

        if (_skillManagement.ElementClass == "Water")
        {
            ElementClass = 4;
            if (ProjectileClass == 4)
            {
                FreezeOn = true;
                FreezeDamage = (int)(FreezeDamage + (FreezeDamage * (100 + PlayerStatus.instance.getAttackDamage()) * 0.001f));
            }
            //baseAttackDamage += baseAttackDamage * _playerstatus.getMaxHP() * 0.001f;
        }

        if (_skillManagement.ElementClass == "Thunder")
        {
            ElementClass = 5;
            if (ProjectileClass == 5)
            {
                ParalyzeOn = true;
                ParalyzeDamage = (int)((2 * (100 + PlayerStatus.instance.getAttackDamage()) * 0.01f));
                if (_playerstatus.classLevel >= 5)
                {
                    ParalyzeDamage = (int)((4 * (100 + PlayerStatus.instance.getAttackDamage()) * 0.01f));
                }
            }
        }
    }
    protected void SoundPlay(AudioClip Snd_Sound)
    {
        Sound.PlayOneShot(Snd_Sound, 0.2f);
    }    

    protected void ItemInfoInit(int index)
    {
        baseAttackDamage = ItemInfoSet.instance.Items[index].baseAttackDamage;
        baseKnockBack = ItemInfoSet.instance.Items[index].baseKnockBack;
        basePenetration = ItemInfoSet.instance.Items[index].basePenetration;
        baseProjectileScale = ItemInfoSet.instance.Items[index].baseProjectileScale;
        baseProjectileSpeed = ItemInfoSet.instance.Items[index].baseProjectileSpeed;
        baseDuration = ItemInfoSet.instance.Items[index].baseDuration;
        baseCooltime = ItemInfoSet.instance.Items[index].baseCooltime;
        if (WindOn)
        {
            baseAttackDamage += baseAttackDamage * (_playerstatus.getMovementSpeed() - 20) * 0.02f;
        }
        if (EarthOn)
        {
            baseAttackDamage += baseAttackDamage * _playerstatus.getMaxHP() * 0.001f;
        }
    }
    public static bool CalculateCritical(float CriticalChance)
    {        
        float randomValue = Random.Range(0.01f, 1.0f);

        if (randomValue <= CriticalChance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    // collision.gameObject.GetComponent<EnemyCtrl>().Hit(this.gameObject, (int)(attackDamage*criticalDamage), knockBack,CriticalColor,true);

    protected bool EnemyHit(Collider2D collision/*, GameObject ItemObject, bool hit, int a, GameObject[] monster*/)
    {
        if (hit == false)
        {
            hit = true;
            if (collision.gameObject.CompareTag("Enemy"))
            {
                if(collision.gameObject != null)
                {
                    int Index = System.Array.IndexOf(monster, collision.gameObject);
                    if (Index == -1)
                    {
                        monsterSize += 1;
                        if (monster.Length < monsterSize)
                        {
                            System.Array.Resize<GameObject>(ref monster, monsterSize * 2);
                        }
                        monster[monsterSize] = collision.gameObject;
                        //_skillManagement.GetInput("Enemy_Hit", collision.gameObject);

                        if (CalculateCritical(criticalChance))
                        {
                            //���� ��ũ��Ʈ�� �������� �˹�Ÿ��� �����ϸ� ���� �ǰ�ó�� ����//
                            collision.gameObject.GetComponent<EnemyCtrl>().Hit(this.gameObject, (int)(attackDamage * criticalDamage), knockBack, new Color(255, 255, 0), true);
                        }
                        else
                        {
                           
                            //���� ��ũ��Ʈ�� �������� �˹�Ÿ��� �����ϸ� ���� �ǰ�ó�� ����//
                            collision.gameObject.GetComponent<EnemyCtrl>().Hit(this.gameObject, (int)attackDamage, knockBack, DamageColor);
                            if (ParalyzeOn)
                            {
                                if (!collision.gameObject.GetComponent<EnemyCtrl>().isParalyzed)
                                {
                                    collision.gameObject.GetComponent<EnemyCtrl>().isParalyzed = true;
                                    var par = Instantiate(enemyParalyzer, collision.transform);
                                    par.GetComponent<EnemyParalyzer>().damage = ParalyzeDamage;
                                }
                            }
                        }

                        if (BurnOn)
                        {
                            if (!collision.gameObject.GetComponent<EnemyCtrl>().isBurned)
                            {
                                var par = Instantiate(enemyBurner, collision.transform);
                                par.GetComponent<EnemyBurner>().damage = BurnDamage;
                            }
                        }

                        if (FreezeOn)
                        {
                            if (collision.gameObject.GetComponent<EnemyCtrl>().isFreezed)
                            {
                                StartCoroutine(addtionalFreezeDamage(collision));
                            }
                            
                        }

                        

                        hit = false;
                        return true;
                    }
                }               
            }
            
            hit = false;
        }
        return false;
    }
    public IEnumerator addtionalFreezeDamage(Collider2D collision)
    {
        yield return new WaitForSeconds(0.2f);
        collision.gameObject.GetComponent<EnemyCtrl>().takeDamage(FreezeDamage, Color.cyan);
    }
    protected IEnumerator TargetActive(int a, GameObject[] monster)
    {
        yield return new WaitForSeconds(0.2f);
        monster[a] = null;
    }

    //���� ����� ���Ϳ��� ����ü �߻� Ȥ�� ����
    protected void ItemSkill1(GameObject ItemObject)
    {
        FoundMonsters = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));

        //Debug.Log("foundmonsters"+FoundMonsters.Count);
        if (FoundMonsters.Count>0)
        {
        shortDis = Vector2.Distance(_player.transform.position, FoundMonsters[0].transform.position); // ù��°�� �������� ����ֱ� 
        Monster = FoundMonsters[0]; // ù��°�� ����         
        foreach (GameObject found in FoundMonsters)
        {
            float Distance = Vector3.Distance(_player.transform.position, found.transform.position);

            if (Distance < shortDis) // ������ ���� �������� �Ÿ� ���
            {
                shortDis = Distance;
                Monster = found;
            }
        }

        target = _player.transform.position;
        _target = Monster.transform.position;
        }
    }

    //���콺 Ŀ�� ��ġ�� ����ü �߻� Ȥ�� ����
    protected void ItemSkill2(GameObject ItemObject)
    {
        target = _player.transform.position;
        _target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //angle = Mathf.Atan2(target.y - _target.y, target.x - _target.x) * Mathf.Rad2Deg;
        //ItemObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    //�÷��̾� ��ġ�� ����
    protected void ItemSkill3(GameObject ItemObject)
    {
        ItemObject.transform.position = _player.transform.position;
    }
    //�� ���� <thunderVeil>
    protected void ItemSkill4(float CoolTime1, float CoolTime2)
    {
        float Timer = 0.0f;
        
        Timer += Time.deltaTime;       
        if ((0.0f <= Timer) && (Timer < CoolTime1))
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
            this.GetComponent<BoxCollider2D>().enabled = true;
        }
        else if ((CoolTime1 <= Timer) && (Timer < CoolTime2))
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            Timer = 0.0f;            
        }
    }
    //��ų ������ġ�� ���콺 Ŀ�� ��ġ �϶�
    protected void ItemSkill5(GameObject ItemObject)
    {
        target = Input.mousePosition;
        target = Camera.main.ScreenToWorldPoint(target);
        //ItemObject.transform.position = Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition);        
        ItemObject.transform.position = target;
    }

    // ������ �������� ����ü ���� ���� 
    protected void ItemSkill6(GameObject ItemObject)
    {
        target = ItemObject.transform.position;
        _target = _player.GetComponent<PlayerCtrl>().RollVec;
        angle = Mathf.Atan2( _target.y, _target.x) * Mathf.Rad2Deg;
        ItemObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    protected void defaultInit()
    {
        defaultInitAttackDamage();
        defaultInitKnockBack();
        defaultInitPenetration();
        defaultInitProjectileSpeed();
        defaultInitProjectileScale();
        defaultInitCriticalChance();
        defaultInitCriticalDamage();
    }

    protected void defaultInitAttackDamage()
    {
        attackDamage += baseAttackDamage * (100 + PlayerStatus.instance.getAttackDamage()) / 100;
    }

    protected void defaultInitKnockBack()
    {
        knockBack += baseKnockBack * PlayerStatus.instance.getKnockBack();
    }
    protected void defaultInitPenetration()
    {
        penetration += basePenetration + PlayerStatus.instance.getPenetration();
    }
    protected void defaultInitProjectileSpeed()
    {
        projectileSpeed += baseProjectileSpeed * PlayerStatus.instance.getProjectileSpeed();
    }
    protected void defaultInitProjectileScale()
    {
        projectileScale += baseProjectileScale * PlayerStatus.instance.getProjectileScale();
    }

    protected void defaultInitCriticalChance()
    {
        criticalChance += PlayerStatus.instance.getCriticalChance();
    }
    protected void defaultInitCriticalDamage()
    {
        criticalDamage += PlayerStatus.instance.getCriticalDamage();
    }

    void DestroyNow()
    {
        Destroy(gameObject);
    }
}
