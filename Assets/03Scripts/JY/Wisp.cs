using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Deceleration
{
    slow = 3, normal = 2, fast = 1, Die = 0
}
public class Wisp : MonoBehaviour
{
    public PlayerElement _wisps;
    public Deceleration _deceleration;
    public Transform WispPos;
   
    private Vector2 DF = new Vector2(0, 0);
    private Vector2 CV = new Vector2(0, 0);
    private Vector2 SF = new Vector2(0, 0);
    private Vector2 Vec = new Vector2(0, 0);


    public Animator WispAnimator;
    private SpriteRenderer SprRender;

    private bool WispProjectileIsDelay;

    public RuntimeAnimatorController EarthWipsAnimator;
    public RuntimeAnimatorController FireWispAnimator;
    public RuntimeAnimatorController WaterWispAnimator;
    public RuntimeAnimatorController WindWispAnimator;
    public RuntimeAnimatorController ThunderWispAnimator;


    [Header("Wisp Projectile")]
    [SerializeField]
    private GameObject WispProjectile;
    [SerializeField]
    private GameObject EarthWispProjectile;
    [SerializeField]
    private GameObject FireWispProjectile;
    [SerializeField]
    private GameObject WaterWispProjectile;
    [SerializeField]
    private GameObject WindWispProjectile;
    [SerializeField]
    private GameObject ThunderWispProjectile;

    public RuntimeAnimatorController PlayerAnimator;

    public bool attackAble;

    // Start is called before the first frame update
    private void Awake()
    {

        WispAnimator = GetComponent<Animator>();
    }
    void Start()
    {
        DF = new Vector2(0, 0);
        CV = new Vector2(0, 0);
        SF = new Vector2(0, 0);
        Vec = new Vector2(0, 0);


        transform.position = WispPos.position;
        SprRender = GetComponent<SpriteRenderer>();        
        WispSelect(_wisps);
        attackAble = true;
    }

    // Update is called once per frame


    private void FixedUpdate()
    {
        Move();
    }


    public void WispSelect(PlayerElement wisps)
    {
        switch(wisps)
        {
            case PlayerElement.Earth:
                WispProjectile = EarthWispProjectile;
                WispAnimator.runtimeAnimatorController = EarthWipsAnimator;
                break;
            case PlayerElement.Fire:
                WispProjectile = FireWispProjectile;
                WispAnimator.runtimeAnimatorController = FireWispAnimator;
                break;
            case PlayerElement.Water:
                WispProjectile = WaterWispProjectile;
                WispAnimator.runtimeAnimatorController = WaterWispAnimator;
                break;
            case PlayerElement.Wind:
                WispProjectile = WindWispProjectile;
                WispAnimator.runtimeAnimatorController = WindWispAnimator;
                break;
            case PlayerElement.Thunder:
                WispProjectile = ThunderWispProjectile;
                WispAnimator.runtimeAnimatorController = ThunderWispAnimator;
                break;
        }
    }



    void Move()
    {
        if( Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D)   )
        {
            WispAnimator.SetTrigger("Move");
        }
        else
        {
            WispAnimator.ResetTrigger("Move");
            BackToIdle();
        }
      
        DF = new Vector2(WispPos.position.x - transform.position.x, WispPos.position.y - transform.position.y);

        SF = (DF - CV) * 0.1f;
        Vec += SF;
        CV = Vec;
        transform.Translate((float)Deceleration.normal * Time.deltaTime * Vec, Space.World);

        if (WispPos.position.x > transform.position.x)//ÁÂ¿ì¹ÝÀü
        {
            SprRender.flipX = true;
        }
        else
        {
            SprRender.flipX = false;
        }
    }

    public void Attack()
    {
        StartCoroutine("Projectile");
      
    }

    IEnumerator Projectile()
    {
        WispAnimator.SetTrigger("Attack");
        for (int index = 0;
            index < ItemInfoSet.instance.Items[5].ProjectileNum;
            index++)
        {
            Instantiate(WispProjectile, transform.position, WispProjectile.transform.rotation);
            yield return new WaitForSeconds(0.3f);
        }
        
        //yield return new WaitForSeconds(2.0f);
        //WispProjectileIsDelay = false;
        WispAnimator.ResetTrigger("Attack");
        BackToIdle();
    }


    void Hurt()
    {
        WispAnimator.SetTrigger("Hurt");
    }

    void Die()
    {
        WispAnimator.SetTrigger("Die");
    }

    void BackToIdle()
    {
        WispAnimator.SetTrigger("Idle");
    }
}
