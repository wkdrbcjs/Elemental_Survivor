using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class ItemInfo
{
    public string ItemName;
    public PlayerElement element;
    public string ItemAbility;
    public string Enhance;
    public Sprite ItemImage;
    public int ItemLevel;   
    public float baseAttackDamage;
    public float baseKnockBack;
    public int basePenetration;
    public float baseProjectileScale;
    public float baseProjectileSpeed;
    public float baseDuration;
    public float baseCooltime;
    public int ProjectileNum;
    public int Percentage;
    public int AttackCount;
}


//아이템 저장 리스트.  
public class ItemInfoSet : MonoBehaviour
{
    public List<ItemInfo> Items = new List<ItemInfo>();

    public Sprite[] ItemImages;

    public static ItemInfoSet instance;
    void Awake()
    {
        instance = this;
        //ItemImages = Resources.LoadAll<Sprite>("ItemImages");
        #region NormalClass
        ItemInfo ElementalMissile = new ItemInfo // 보류 = wispprojectile
        {
            ItemName = "ElementalMissile",
            element = PlayerElement.Normal,
            ItemLevel = 1,
            ItemImage = ItemImages[5],
            baseAttackDamage = 10,
            baseCooltime = 0.0f,
            baseDuration = 0.0f,
            baseKnockBack = 0.1f,
            basePenetration = 1,
            baseProjectileScale = 7.0f,
            baseProjectileSpeed = 40.0f,
            ProjectileNum = 1,
            Enhance = "none",
            ItemAbility = "Creates an elemental missile that tracks nearby enemies at regular intervals"
        };
        ItemInfo EnhancedArrow = new ItemInfo //보류 = normal attack
        {
            ItemName = "EnhancedArrow",
            element = PlayerElement.Normal,
            ItemLevel = 1,
            ItemImage = ItemImages[11],
            baseAttackDamage = 20,
            baseCooltime = 0.0f,
            baseDuration = 0.0f,
            baseKnockBack = 0.1f,
            basePenetration = 1,
            baseProjectileScale = 20.0f,
            baseProjectileSpeed = 20.0f,
            Enhance = "none",
            ItemAbility = "An enhanced attack is fired every time a character performs a basic attack 4 times"
        };
        #endregion

        #region FireClass
        ItemInfo FireBeam = new ItemInfo //
        {
            ItemName = "FireBeam",
            element = PlayerElement.Fire,
            ItemLevel = 1,
            ItemImage = ItemImages[9],
            baseAttackDamage = 70,
            baseCooltime = 2.0f,
            baseDuration = 0.0f,
            baseKnockBack = 0.3f,
            basePenetration = 1,
            baseProjectileScale = 30.0f,
            baseProjectileSpeed = 1.0f,
            ProjectileNum = 1,
            AttackCount = 10,
            Enhance = "none",
            ItemAbility = "Fires a projectile in the direction of the mouse cursor every 10 basic attacks"
        };
        ItemInfo Fireball = new ItemInfo // 보류
        {
            ItemName = "Fireball",
            element = PlayerElement.Fire,
            ItemLevel = 1,
            ItemImage = ItemImages[3],
            baseAttackDamage = 15,
            baseCooltime = 0.0f,
            baseDuration = 0.0f,
            baseKnockBack = 0.4f,
            basePenetration = 1,
            baseProjectileScale = 8.0f,
            baseProjectileSpeed = 2.0f,
            ProjectileNum = 1,
            AttackCount = 5,
            Enhance = "none",
            ItemAbility = "Fires a projectile in the direction of the mouse cursor every 5 basic attacks"
        };

        ItemInfo FireBite = new ItemInfo //
        {
            ItemName = "FireBite",
            element = PlayerElement.Fire,
            ItemLevel = 1,
            ItemImage = ItemImages[15],
            baseAttackDamage = 25,
            baseCooltime = 2.0f,
            baseDuration = 3.0f,
            baseKnockBack = 0.3f,
            basePenetration = 1,
            baseProjectileScale = 25.0f,
            baseProjectileSpeed = 20.0f,
            AttackCount = 6,
            ProjectileNum = 1,
            Enhance = "none",
            ItemAbility = "Fires a projectile in the direction of the mouse cursor every 10 basic attacks"
        };

        ItemInfo Lavas = new ItemInfo //
        {
            ItemName = "Lavas",
            element = PlayerElement.Fire,
            ItemLevel = 1,
            ItemImage = ItemImages[20],
            baseAttackDamage = 3,
            baseCooltime = 10.0f,
            baseDuration = 5.0f,
            baseKnockBack = 0.1f,
            basePenetration = 1,
            baseProjectileScale = 50.0f,
            baseProjectileSpeed = 1.0f,
            Enhance = "none",
            ItemAbility = "Creates lava at the character's current location at regular intervals."
        };

        ItemInfo SmallMeteor = new ItemInfo
        {
            ItemName = "SmallMeteor",
            element = PlayerElement.Fire,
            ItemImage = ItemImages[3],
            baseAttackDamage = 25,
            baseCooltime = 0.0f,
            baseDuration = 0.0f,
            baseKnockBack = 1.0f,
            basePenetration = 1,
            baseProjectileScale = 5.0f,
            baseProjectileSpeed = 3.0f,
            ProjectileNum = 1,
            ItemAbility = "none"
        };
        #endregion

        #region EarthClass
        ItemInfo EarthImpale = new ItemInfo //
        {
            ItemName = "EarthImpale",
            element = PlayerElement.Earth,
            ItemLevel = 1,
            ItemImage = ItemImages[0],
            baseAttackDamage = 45,
            baseCooltime = 14.0f,
            baseDuration = 0.0f,
            baseKnockBack = 0.2f,
            basePenetration = 1,
            baseProjectileScale = 50f,
            baseProjectileSpeed = 1.0f,
            ProjectileNum = 1,
            Enhance = "none",
            ItemAbility = "Creates spikes that rise from the ground at the location of the mouse cursor at regular intervals"  
        };

        ItemInfo EarthSaw = new ItemInfo //
        {
            ItemName = "EarthSaw",
            element = PlayerElement.Earth,
            ItemLevel = 1,
            ItemImage = ItemImages[6],
            baseAttackDamage = 4,
            baseCooltime = 6.0f,
            baseDuration = 0.0f,
            baseKnockBack = 0.3f,
            basePenetration = 5,
            baseProjectileScale = 35.0f,
            baseProjectileSpeed = 5.0f,
            ProjectileNum = 1,
            Enhance = "none",
            ItemAbility = "When dashing, fires a spinning saw that travels in the direction of nearby enemies"
        };

        ItemInfo EarthSpike = new ItemInfo //
        {
            ItemName = "EarthSpike",
            element = PlayerElement.Earth,
            ItemLevel = 1,
            ItemImage = ItemImages[17],
            baseAttackDamage = 5,
            baseCooltime = 7.0f,
            baseDuration = 0.0f,
            baseKnockBack = 0.3f,
            basePenetration = 1,
            baseProjectileScale = 35.0f,
            baseProjectileSpeed = 1.0f,
            ProjectileNum = 15,
            Enhance = "none",
            ItemAbility = "Fires a thorn that rises in the direction of the mouse cursor at regular intervals"
        };

        ItemInfo EarthCrash = new ItemInfo //
        {
            ItemName = "EarthCrash",
            element = PlayerElement.Earth,
            ItemLevel = 1,
            ItemImage = ItemImages[12],
            baseAttackDamage = 20,
            baseCooltime = 0.0f,
            baseDuration = 0.0f,
            baseKnockBack = 0.3f,
            basePenetration = 1,
            baseProjectileScale = 40.0f,
            baseProjectileSpeed = 1.0f,
            AttackCount = 8,
            Enhance = "none",
            ItemAbility = "Every 8th basic attack creates a colliding wall that pulls enemies around the mouse cursor to the mouse cursor location and deals damage"
        };
        #endregion

        #region WindClass
        ItemInfo ShadowBolt = new ItemInfo //
        {
            ItemName = "ShadowBolt",
            element = PlayerElement.Wind,
            ItemLevel = 1,
            ItemImage = ItemImages[2],
            baseAttackDamage = 30,
            baseCooltime = 0.0f,
            baseDuration = 0.0f,
            baseKnockBack = 0.3f,
            basePenetration = 1,
            baseProjectileScale = 15.0f,
            baseProjectileSpeed = 50.0f,
            ProjectileNum = 1,
            Enhance = "none",
            ItemAbility = "When use Dash, it fires a projectile in the same direction as Dash"
        };

        ItemInfo WindSlash = new ItemInfo //
        {
            ItemName = "WindSlash",
            element = PlayerElement.Wind,
            ItemLevel = 1,
            ItemImage = ItemImages[14],
            baseAttackDamage = 5,
            baseCooltime = 5.0f,
            baseDuration = 0.0f,
            baseKnockBack = 0.2f,
            basePenetration = 1,
            baseProjectileScale = 25.0f,
            baseProjectileSpeed = 25.0f,
            ProjectileNum = 3,
            Enhance = "none",
            ItemAbility = "Fires a returning projectile that flies to the nearest enemy at regular intervals"
        };

        ItemInfo WindCutter = new ItemInfo // = bezierctrl
        {
            ItemName = "WindCutter",
            element = PlayerElement.Wind,
            ItemLevel = 1,
            ItemImage = ItemImages[8],
            baseAttackDamage = 15,
            baseCooltime = 0.0f,
            baseDuration = 0.0f,
            baseKnockBack = 0.3f,
            basePenetration = 1,
            baseProjectileScale = 30.0f,
            baseProjectileSpeed = 1.0f,
            ProjectileNum = 6,
            Enhance = "none",
            ItemAbility = "When casting a basic attack after dashing, a projectile is created that flies towards a random enemy near the mouse point"
        };

        ItemInfo Tornado = new ItemInfo //
        {
            ItemName = "Tornado",
            element = PlayerElement.Wind,
            ItemLevel = 1,
            ItemImage = ItemImages[19],
            baseAttackDamage = 5,
            baseCooltime = 0.2f,
            baseDuration = 5.0f,
            baseKnockBack = -0.7f,
            basePenetration = 1,
            baseProjectileScale = 30.0f,
            baseProjectileSpeed = 0.5f,
            ProjectileNum = 1,
            Enhance = "none",
            ItemAbility = "When using Dash, Creates a projectile that flies to a random location"
        };
        #endregion

        #region WaterClass
        ItemInfo WaterSplash = new ItemInfo //
        {
            ItemName = "WaterSplash",
            element = PlayerElement.Water,
            ItemLevel = 1,
            ItemImage = ItemImages[1],
            baseAttackDamage = 35,
            baseCooltime = 8.0f,
            baseDuration = 0.0f,
            baseKnockBack = 0.6f,
            basePenetration = 1,
            baseProjectileScale = 50,
            baseProjectileSpeed = 1.0f,
            ProjectileNum = 1,
            Enhance = "none",
            ItemAbility = "Creates a column of water that deals damage to enemies at the location of the mouse cursor at regular intervals"
        };

        ItemInfo WaterWhip = new ItemInfo // = watercombo
        {
            ItemName = "WaterWhip",
            element = PlayerElement.Water,
            ItemLevel = 1,
            ItemImage = ItemImages[7],
            baseAttackDamage = 20,
            baseCooltime = 5.0f,
            baseDuration = 0.0f,
            baseKnockBack = 5.0f,
            basePenetration = 1,
            baseProjectileScale = 60.0f,
            baseProjectileSpeed = 1.0f,
            ProjectileNum = 1,
            Enhance = "none",
            ItemAbility = "Attacks by swinging in the direction of the mouse cursor at regular intervals"
        };

        ItemInfo WaterMine = new ItemInfo //
        {
            ItemName = "WaterMine",
            element = PlayerElement.Water,
            ItemLevel = 1,
            ItemImage = ItemImages[13],
            baseAttackDamage = 10,
            baseCooltime = 0.0f,
            baseDuration = 4.0f,
            baseKnockBack = 0.3f,
            basePenetration = 1,
            baseProjectileScale = 35.0f,
            baseProjectileSpeed = 1.0f,
            ProjectileNum = 5,
            Enhance = "none",
            ItemAbility = "When using Dash, it creates a mine that deals damage over the duration at the location where the dash is cast"
        };

        ItemInfo IceShot = new ItemInfo //
        {
            ItemName = "IceShot",
            element = PlayerElement.Water,
            ItemLevel = 1,
            ItemImage = ItemImages[18],
            baseAttackDamage = 15,
            baseCooltime = 5.0f,
            baseDuration = 0.0f,
            baseKnockBack = 0.3f,
            basePenetration = 1,
            baseProjectileScale = 20.0f,
            baseProjectileSpeed = 50.0f,
            ProjectileNum = 1,
            Enhance = "none",
            ItemAbility = "Fires a projectile in the direction of the mouse cursor at regular intervals"
        };
        #endregion

        #region ThunderClass
        ItemInfo ThunderBall = new ItemInfo //
        {
            ItemName = "ThunderBall",
            element = PlayerElement.Thunder,
            ItemLevel = 1,
            ItemImage = ItemImages[4],
            baseAttackDamage = 6,
            baseCooltime = 0.0f,
            baseDuration = 0.0f,
            baseKnockBack = 0.3f,
            basePenetration = 1,
            baseProjectileScale = 15.0f,
            baseProjectileSpeed = 25.0f,
            Percentage = 200,
            Enhance = "none",
            ItemAbility = "When performing basic attacks, there is a chance to fire a thunder ball in the direction of the mouse cursor"
        };

        ItemInfo ThunderExplosion = new ItemInfo //
        {
            ItemName = "ThunderExplosion",
            element = PlayerElement.Thunder,
            ItemLevel = 1,
            ItemImage = ItemImages[10],
            baseAttackDamage = 10,
            baseCooltime = 0.0f,
            baseDuration = 0.0f,
            baseKnockBack = 0.3f,
            basePenetration = 1,
            baseProjectileScale = 35.0f,
            baseProjectileSpeed = 1.0f,
            ProjectileNum = 1,
            Enhance = "none",
            ItemAbility = "When an enemy is killed, there is a chance to create an electric ball that explodes when it collides with a basic attack at the location of the defeated enemy.",
            Percentage = 50
        };

        ItemInfo Thunderstorm = new ItemInfo //
        {
            ItemName = "Thunderstorm",
            element = PlayerElement.Thunder,
            ItemLevel = 1,
            ItemImage = ItemImages[16],
            baseAttackDamage = 10,
            baseCooltime = 0.0f,
            baseDuration = 0.0f,
            baseKnockBack = 0.5f,
            basePenetration = 1,
            baseProjectileScale = 20.0f,
            baseProjectileSpeed = 1.0f,
            ProjectileNum = 1,
            Percentage = 100,
            Enhance = "none",
            ItemAbility = "When a basic attack hits, the enemy has a chance to receive additional Thunder Storm"
        };

        ItemInfo ThunderVeil = new ItemInfo
        {
            ItemName = "ThunderVeil",
            element = PlayerElement.Thunder,
            ItemLevel = 1,
            ItemImage = ItemImages[21],
            baseAttackDamage = 10,
            baseCooltime = 10.0f,
            baseDuration = 3.0f,
            baseKnockBack = 0.3f,
            basePenetration = 1,
            baseProjectileScale = 40.0f,
            baseProjectileSpeed = 1.0f,
            ProjectileNum = 1,
            Enhance = "none",
            ItemAbility = "Activates a curtain that damages enemies around the character when hit"
        };
        #endregion


        Items.Add(EarthImpale);
        Items.Add(WaterSplash);
        Items.Add(ShadowBolt);
        Items.Add(Fireball);
        Items.Add(ThunderBall);
        Items.Add(ElementalMissile);
        Items.Add(EarthSaw);
        Items.Add(WaterWhip);
        Items.Add(WindCutter);
        Items.Add(FireBeam);
        Items.Add(ThunderExplosion);
        Items.Add(EnhancedArrow);
        Items.Add(EarthCrash);
        Items.Add(WaterMine);
        Items.Add(WindSlash);
        Items.Add(FireBite);
        Items.Add(Thunderstorm);
        Items.Add(EarthSpike);
        Items.Add(IceShot);
        Items.Add(Tornado);
        Items.Add(Lavas);
        Items.Add(ThunderVeil);
        Items.Add(SmallMeteor);
    }

    public void ItemAbilitySet()
    {
        //Debug.Log("ItemAbilitySet");
        for (int i = 0; i < 22; i++)
        {
            switch (i)
            {
                //Earth Impale
                case 0:
                    if (Items[0].ItemLevel < 9)
                    {
                        switch (Items[0].ItemLevel)
                        {
                            case 1:
                                Items[0].Enhance = "Base Damage up by 10";
                                break;
                            case 2:
                                Items[0].Enhance = "Creates 1 more projectile";
                                break;
                            case 3:
                                Items[0].Enhance = "Base Cooltime down by 1 sec";
                                break;
                            case 4:
                                Items[0].Enhance = "Base Damage up by 10";
                                break;
                            case 5:
                                Items[0].Enhance = "Creates 1 more projectile";
                                break;
                            case 6:
                                Items[0].Enhance = "Base Cooltime down by 1 sec";
                                break;
                            case 7:
                                Items[0].Enhance = "Creates 1 more projectile";
                                break;
                            case 8:
                                Items[0].Enhance = "Base Damage up by 5";
                                break;
                        }
                    }
                    else
                    {
                        Items[0].Enhance = "Base Damage up by 5";
                    }
                    break;
                //WaterSplash
                case 1:
                    if (Items[1].ItemLevel < 9)
                    {
                        switch (Items[1].ItemLevel)
                        {
                            case 1:
                                Items[1].Enhance = "Base Damage up by 15";
                                break;
                            case 2:
                                Items[1].Enhance = "Base Cooltime down by 1 sec";
                                break;
                            case 3:
                                Items[1].Enhance = "Creates 1 more projectile";
                                break;
                            case 4:
                                Items[1].Enhance = "Base Damage up by 15";
                                break;
                            case 5:
                                Items[1].Enhance = "Base Cooltime down by 1 sec";
                                break;
                            case 6:
                                Items[1].Enhance = "Creates 1 more projectile";
                                break;
                            case 7:
                                Items[1].Enhance = "Base Cooltime down by 2 sec";
                                break;
                            case 8:
                                Items[1].Enhance = "Base Damage up by 10";
                                break;
                        }
                    }
                    else
                    {
                        Items[1].Enhance = "Base Damage up by 10";
                    }
                    break;
                //ShadowBolt
                case 2:
                    if (Items[2].ItemLevel < 9)
                    {
                        switch (Items[2].ItemLevel)
                        {
                            case 1:
                                Items[2].Enhance = "Base Damage up by 10";
                                break;
                            case 2:
                                Items[2].Enhance = "Base Scale up by 25%";
                                break;
                            case 3:
                                Items[2].Enhance = "Fires 1 more projectile";
                                break;
                            case 4:
                                Items[2].Enhance = "Base Damage up by 10";
                                break;
                            case 5:
                                Items[2].Enhance = "Base Penetration up by 1";
                                break;
                            case 6:
                                Items[2].Enhance = "Base Scale up by 25%";
                                break;
                            case 7:
                                Items[2].Enhance = "Fires 1 more projectile";
                                break;
                            case 8:
                                Items[2].Enhance = "Base Damage up by 5";
                                break;
                        }
                    }
                    else
                    {
                        Items[2].Enhance = "Base Damage up by 5";
                    }
                    break;
                //Fireball
                case 3:
                    if (Items[3].ItemLevel < 9)
                    {
                        switch (Items[3].ItemLevel)
                        {
                            case 1:
                                Items[3].Enhance = "Base Damage up by 10";
                                break;
                            case 2:
                                Items[3].Enhance = "Base Scale up by 25%";
                                break;
                            case 3:
                                Items[3].Enhance = "Fires 1 more projectile";
                                break;
                            case 4:
                                Items[3].Enhance = "Base Damage up by 10";
                                break;
                            case 5:
                                Items[3].Enhance = "Base Scale up by 25%";
                                break;
                            case 6:
                                Items[3].Enhance = "Fires 1 more projectile";
                                break;
                            case 7:
                                Items[3].Enhance = "Activation condition basic attack count decrease by 1";
                                break;
                            case 8:
                                Items[3].Enhance = "Base Damage up by 5";
                                break;
                        }
                    }
                    else
                    {
                        Items[3].Enhance = "Base Damage up by 5";
                    }
                    break;
                //ThunderBall         
                case 4:
                    if (Items[4].ItemLevel < 9)
                    {
                        switch (Items[4].ItemLevel)
                        {
                            case 1:
                                Items[4].Enhance = "Base Damage up by 5";
                                break;
                            case 2:
                                Items[4].Enhance = "Increases Projectile Activation Chance by 5%";
                                break;
                            case 3:
                                Items[4].Enhance = "Increases Projectile Activation Chance by 5%";
                                break;
                            case 4:
                                Items[4].Enhance = "Base Damage up by 5";
                                break;
                            case 5:
                                Items[4].Enhance = "Increases Projectile Activation Chance by 5%";
                                break;
                            case 6:
                                Items[4].Enhance = "Increases Projectile Activation Chance by 5%";
                                break;
                            case 7:
                                Items[4].Enhance = "Base Damage up by 5";
                                break;
                            case 8:
                                Items[4].Enhance = "Base Damage up by 5";
                                break;
                        }
                    }
                    else
                    {
                        Items[4].Enhance = "Base Damage up by 5";
                    }
                    break;
                //ElementalMissile
                case 5:
                    if (Items[5].ItemLevel < 9)
                    {
                        switch (Items[5].ItemLevel)
                        {
                            case 1:
                                Items[5].Enhance = "Base Damage up by 5";
                                break;
                            case 2:
                                Items[5].Enhance = "Fires 1 more projectile";
                                break;
                            case 3:
                                Items[5].Enhance = "Base Damage up by 5";
                                break;
                            case 4:
                                Items[5].Enhance = "Fires 1 more projectile";
                                break;
                            case 5:
                                Items[5].Enhance = "Base Damage up by 5";
                                break;
                            case 6:
                                Items[5].Enhance = "Fires 1 more projectile";
                                break;
                            case 7:
                                Items[5].Enhance = "Base Damage up by 5";
                                break;
                            case 8:
                                Items[5].Enhance = "Fires 1 more projectile";
                                break;
                        }
                    }
                    else
                    {
                        Items[5].Enhance = "Base Damage up by 2";
                    }
                    break;
                //EarthSaw
                case 6:
                    if (Items[6].ItemLevel < 9)
                    {
                        switch (Items[6].ItemLevel)
                        {
                            case 1:
                                Items[6].Enhance = "Fires 1 more projectile";
                                break;
                            case 2:
                                Items[6].Enhance = "Base Damage up by 6";
                                break;
                            case 3:
                                Items[6].Enhance = "Base Cooltime down by 1 sec";
                                break;
                            case 4:
                                Items[6].Enhance = "Base Damage up by 3";
                                break;
                            case 5:
                                Items[6].Enhance = "Fires 1 more projectile";
                                break;
                            case 6:
                                Items[6].Enhance = "Base Cooltime down by 1 sec";
                                break;
                            case 7:
                                Items[6].Enhance = "Fires 1 more projectile";
                                break;
                            case 8:
                                Items[6].Enhance = "Base Damage up by 3";
                                break;
                        }
                    }
                    else
                    {
                        Items[6].Enhance = "Base Damage up by 3";
                    }
                    break;
                //WaterWhip
                case 7:
                    if (Items[7].ItemLevel < 9)
                    {
                        switch (Items[7].ItemLevel)
                        {
                            case 1:
                                Items[7].Enhance = "Base Damage up by 10";
                                break;
                            case 2:
                                Items[7].Enhance = "Increase the number of hits by 1";
                                break;
                            case 3:
                                Items[7].Enhance = "Increase the number of hits by 1";
                                break;
                            case 4:
                                Items[7].Enhance = "Base Damage up by 10";
                                break;
                            case 5:
                                Items[7].Enhance = "Base Damage up by 10";
                                break;
                            case 6:
                                Items[7].Enhance = "Increase the number of hits by 1";
                                break;
                            case 7:
                                Items[7].Enhance = "Base Damage up by 10";
                                break;
                            case 8:
                                Items[7].Enhance = "Base Damage up by 5";
                                break;
                        }
                    }
                    else
                    {
                        Items[7].Enhance = "Base Damage up by 5";
                    }
                    break;
                //WindCutter
                case 8:
                    if (Items[8].ItemLevel < 9)
                    {
                        switch (Items[8].ItemLevel)
                        {
                            case 1:
                                Items[8].Enhance = "Fires 1 more projectile";
                                break;
                            case 2:
                                Items[8].Enhance = "Base Damage up by 3";
                                break;
                            case 3:
                                Items[8].Enhance = "Fires 1 more projectile";
                                break;
                            case 4:
                                Items[8].Enhance = "Base Damage up by 3";
                                break;
                            case 5:
                                Items[8].Enhance = "Fires 2 more projectile";
                                break;
                            case 6:
                                Items[8].Enhance = "Base Damage up by 3";
                                break;
                            case 7:
                                Items[8].Enhance = "Fires 3 more projectile";
                                break;
                            case 8:
                                Items[8].Enhance = "Base Damage up by 5";
                                break;
                        }
                    }
                    else
                    {
                        Items[8].Enhance = "Base Damage up by 5";
                    }
                    break;
                //FireBeam
                case 9:
                    if (Items[9].ItemLevel < 9)
                    {
                        switch (Items[9].ItemLevel)
                        {
                            case 1:
                                Items[9].Enhance = "Base Damage up by 10";
                                break;
                            case 2:
                                Items[9].Enhance = "Activation condition basic attack count decrease by 1";
                                break;
                            case 3:
                                Items[9].Enhance = "Base Damage up by 10";
                                break;
                            case 4:
                                Items[9].Enhance = "Fires 1 more projectile";
                                break;
                            case 5:
                                Items[9].Enhance = "Increase the number of hits by 1";
                                break;
                            case 6:
                                Items[9].Enhance = "Activation condition basic attack count decrease by 1";
                                break;
                            case 7:
                                Items[9].Enhance = "Fires 1 more projectile";
                                break;
                            case 8:
                                Items[9].Enhance = "Base Damage up by 5";
                                break;
                        }
                    }
                    else
                    {
                        Items[9].Enhance = "Base Damage up by 5";
                    }
                    break;
                //ThunderExplosion
                case 10:
                    if (Items[10].ItemLevel < 9)
                    {
                        switch (Items[10].ItemLevel)
                        {
                            case 1:
                                Items[10].Enhance = "Increases Projectile Activation Chance by 5%";
                                break;
                            case 2:
                                Items[10].Enhance = "Creates 1 more projectile";
                                break;
                            case 3:
                                Items[10].Enhance = "Base Damage up by 5";
                                break;
                            case 4:
                                Items[10].Enhance = "Base Scale up by 50%";
                                break;
                            case 5:
                                Items[10].Enhance = "Creates 2 more projectile";
                                break;
                            case 6:
                                Items[10].Enhance = "Increases Projectile Activation Chance by 5%";
                                break;
                            case 7:
                                Items[10].Enhance = "Base Scale up by 50%";
                                break;
                            case 8:
                                Items[10].Enhance = "Base Damage up by 5";
                                break;
                        }
                    }
                    else
                    {
                        Items[10].Enhance = "Base Damage up by 5";
                    }
                    break;
                //EnhancedArrow
                case 11:
                    if (Items[11].ItemLevel < 9)
                    {
                        switch (Items[11].ItemLevel)
                        {
                            case 1:
                                Items[11].Enhance = "Base Damage up by 5";
                                break;
                            case 2:
                                Items[11].Enhance = "Base Damage up by 5";
                                break;
                            case 3:
                                Items[11].Enhance = "Base Scale up by 25%";
                                break;
                            case 4:
                                Items[11].Enhance = "Base Damage up by 5";
                                break;
                            case 5:
                                Items[11].Enhance = "Base Damage up by 5";
                                break;
                            case 6:
                                Items[11].Enhance = "Base Scale up by 25%";
                                break;
                            case 7:
                                Items[11].Enhance = "Activation condition basic attack count decrease by 1";
                                break;
                            case 8:
                                Items[11].Enhance = "Base Damage up by 3";
                                break;
                        }
                    }
                    else
                    {
                        Items[11].Enhance = "Base Damage up by 3";
                    }
                    break;
                //EarthCrash
                case 12:
                    if (Items[12].ItemLevel < 9)
                    {
                        switch (Items[12].ItemLevel)
                        {
                            case 1:
                                Items[12].Enhance = "Base Damage up by 10";
                                break;
                            case 2:
                                Items[12].Enhance = "Base Scale up by 50%";
                                break;
                            case 3:
                                Items[12].Enhance = "Activation condition basic attack count decrease by 1";
                                break;
                            case 4:
                                Items[12].Enhance = "Base Damage up by 10";
                                break;
                            case 5:
                                Items[12].Enhance = "Base Scale up by 50%";
                                break;
                            case 6:
                                Items[12].Enhance = "Activation condition basic attack count decrease by 1";
                                break;
                            case 7:
                                Items[12].Enhance = "Base Scale up by 50%";
                                break;
                            case 8:
                                Items[12].Enhance = "Base Damage up by 5";
                                break;
                        }
                    }
                    else
                    {
                        Items[12].Enhance = "Base Damage up by 5";
                    }
                    break;
                //WaterMine
                case 13:
                    if (Items[13].ItemLevel < 9)
                    {
                        switch (Items[13].ItemLevel)
                        {
                            case 1:
                                Items[13].Enhance = "Creates 1 more projectile";
                                break;
                            case 2:
                                Items[13].Enhance = "Base Damage up by 10";
                                break;
                            case 3:
                                Items[13].Enhance = "Base Duration down by 1 sec";
                                break;
                            case 4:
                                Items[13].Enhance = "Base Damage up by 10";
                                break;
                            case 5:
                                Items[13].Enhance = "Creates 1 more projectile";
                                break;
                            case 6:
                                Items[13].Enhance = "Base Duration down by 1 sec";
                                break;
                            case 7:
                                Items[13].Enhance = "Base Scale up by 50%";
                                break;
                            case 8:
                                Items[13].Enhance = "Base Damage up by 5";
                                break;
                        }
                    }
                    else
                    {
                        Items[13].Enhance = "Base Damage up by 5";
                    }
                    break;
                //WindSlash
                case 14:
                    if (Items[14].ItemLevel < 9)
                    {
                        switch (Items[14].ItemLevel)
                        {
                            case 1:
                                Items[14].Enhance = "Fires 1 more projectile";
                                break;
                            case 2:
                                Items[14].Enhance = "Base Damage up by 5";
                                break;
                            case 3:
                                Items[14].Enhance = "Fires 1 more projectile";
                                break;
                            case 4:
                                Items[14].Enhance = "Base Damage up by 5";
                                break;
                            case 5:
                                Items[14].Enhance = "Fires 1 more projectile";
                                break;
                            case 6:
                                Items[14].Enhance = "Base Damage up by 10";
                                break;
                            case 7:
                                Items[14].Enhance = "Fires 2 more projectile";
                                break;
                            case 8:
                                Items[14].Enhance = "Base Damage up by 5";
                                break;
                        }
                    }
                    else
                    {
                        Items[14].Enhance = "Base Damage up by 5";
                    }
                    break;
                //FireBite
                case 15:
                    if (Items[15].ItemLevel < 9)
                    {
                        switch (Items[15].ItemLevel)
                        {
                            case 1:
                                Items[15].Enhance = "Base Damage up by 10";
                                break;
                            case 2:
                                Items[15].Enhance = "Activation condition basic attack count decrease by 1";
                                break;
                            case 3:
                                Items[15].Enhance = "Increase the number of hits by 1";
                                break;
                            case 4:
                                Items[15].Enhance = "Fires 1 more projectile";
                                break;
                            case 5:
                                Items[15].Enhance = "Base Duration up by 2 sec";
                                break;
                            case 6:
                                Items[15].Enhance = "Activation condition basic attack count decrease by 1";
                                break;
                            case 7:
                                Items[15].Enhance = "Fires 1 more projectile";
                                break;
                            case 8:
                                Items[15].Enhance = "Base Damage up by 5";
                                break;
                        }
                    }
                    else
                    {
                        Items[15].Enhance = "Base Damage up by 5";
                    }
                    break;
                //Thunderstorm
                case 16:
                    if (Items[16].ItemLevel < 9)
                    {
                        switch (Items[16].ItemLevel)
                        {
                            case 1:
                                Items[16].Enhance = "Base Damage up by 10";
                                break;
                            case 2:
                                Items[16].Enhance = "Increases Projectile Activation Chance by 5%";
                                break;
                            case 3:
                                Items[16].Enhance = "Increases Projectile Activation Chance by 5%";
                                break;
                            case 4:
                                Items[16].Enhance = "Base Damage up by 10";
                                break;
                            case 5:
                                Items[16].Enhance = "Increases Projectile Activation Chance by 5%";
                                break;
                            case 6:
                                Items[16].Enhance = "Increases Projectile Activation Chance by 5%";
                                break;
                            case 7:
                                Items[16].Enhance = "Base Damage up by 10";
                                break;
                            case 8:
                                Items[16].Enhance = "Base Damage up by 3";
                                break;
                        }
                    }
                    else
                    {
                        Items[16].Enhance = "Base Damage up by 3";
                    }
                    break;
                //EarthSpike
                case 17:
                    if (Items[17].ItemLevel < 9)
                    {
                        switch (Items[17].ItemLevel)
                        {
                            case 1:
                                Items[17].Enhance = "Base Damage up by 3";
                                break;
                            case 2:
                                Items[17].Enhance = "Fires 2 more projectile";
                                break;
                            case 3:
                                Items[17].Enhance = "Fires 2 more projectile";
                                break;
                            case 4:
                                Items[17].Enhance = "Base Damage up by 3";
                                break;
                            case 5:
                                Items[17].Enhance = "Base Scale up by 25%";
                                break;
                            case 6:
                                Items[17].Enhance = "Fires 2 more projectile";
                                break;
                            case 7:
                                Items[17].Enhance = "Base Scale up by 25%";
                                break;
                            case 8:
                                Items[17].Enhance = "Base Damage up by 2";
                                break;
                        }
                    }
                    else
                    {
                        Items[17].Enhance = "Base Damage up by 2";
                    }
                    break;
                //IceShot
                case 18:
                    if (Items[18].ItemLevel < 9)
                    {
                        switch (Items[18].ItemLevel)
                        {
                            case 1:
                                Items[18].Enhance = "Base Damage up by 5";
                                break;
                            case 2:
                                Items[18].Enhance = "Fires 1 more projectile";
                                break;
                            case 3:
                                Items[18].Enhance = "Base Damage up by 5";
                                break;
                            case 4:
                                Items[18].Enhance = "Fires 1 more projectile";
                                break;
                            case 5:
                                Items[18].Enhance = "Base Cooltime down by 1";
                                break;
                            case 6:
                                Items[18].Enhance = "Base Damage up by 5";
                                break;
                            case 7:
                                Items[18].Enhance = "Base Cooltime down by 1";
                                break;
                            case 8:
                                Items[18].Enhance = "Base Damage up by 5";
                                break;
                        }
                    }
                    else
                    {
                        Items[18].Enhance = "Base Damage up by 5";
                    }
                    break;
                //Tornado
                case 19:
                    if (Items[19].ItemLevel < 9)
                    {
                        switch (Items[19].ItemLevel)
                        {
                            case 1:
                                Items[19].Enhance = "Base Damage up by 1";
                                break;
                            case 2:
                                Items[19].Enhance = "Base Scale up by 10%";
                                break;
                            case 3:
                                Items[19].Enhance = "Base Cooltime down by 0.05";
                                break;
                            case 4:
                                Items[19].Enhance = "Base Damage up by 2";
                                break;
                            case 5:
                                Items[19].Enhance = "Base Scale up by 10%";
                                break;
                            case 6:
                                Items[19].Enhance = "BaseCooltime down by 0.05";
                                break;
                            case 7:
                                Items[19].Enhance = "Base Scale up by 10%";
                                break;
                            case 8:
                                Items[19].Enhance = "Base Damage up by 2";
                                break;
                        }
                    }
                    else
                    {
                        Items[19].Enhance = "Base Damage up by 2";
                    }
                    break;
                //Lavas
                case 20:
                    if (Items[20].ItemLevel < 9)
                    {
                        switch (Items[20].ItemLevel)
                        {
                            case 1:
                                Items[20].Enhance = "Base Damage up by 2";
                                break;
                            case 2:
                                Items[20].Enhance = "Base Scale up by 25%";
                                break;
                            case 3:
                                Items[20].Enhance = "Base Duration up by 2 sec";
                                break;
                            case 4:
                                Items[20].Enhance = "Base Damage up by 2";
                                break;
                            case 5:
                                Items[20].Enhance = "Base Scale up by 25%";
                                break;
                            case 6:
                                Items[20].Enhance = "Base Duration up by 2 sec";
                                break;
                            case 7:
                                Items[20].Enhance = "Enemies hit by lava are slowed.";
                                break;
                            case 8:
                                Items[20].Enhance = "Base Damage up by 1";
                                break;
                        }
                    }
                    else
                    {
                        Items[20].Enhance = "Base Damage up by 1";
                    }
                    break;
                //ThunderVeil
                case 21:
                    if (Items[21].ItemLevel < 9)
                    {
                        switch (Items[21].ItemLevel)
                        {
                            case 1:
                                Items[21].Enhance = "Base Damage up by 5";
                                break;
                            case 2:
                                Items[21].Enhance = "Base Duration up by 2 sec";
                                break;
                            case 3:
                                Items[21].Enhance = "Base Damage up by 5";
                                break;
                            case 4:
                                Items[21].Enhance = "Defense increases during the veil activation time by 2";
                                break;
                            case 5:
                                Items[21].Enhance = "Base Duration up by 2 sec";
                                break;
                            case 6:
                                Items[21].Enhance = "Base Scale up by 50%";
                                break;
                            case 7:
                                Items[21].Enhance = "Defense increases during the veil activation time by 2";
                                break;
                            case 8:
                                Items[21].Enhance = "Base Damage up by 3";
                                break;
                        }
                    }
                    else
                    {
                        Items[21].Enhance = "Base Damage up by 3";
                    }
                    break;
            }
        }
    }
}