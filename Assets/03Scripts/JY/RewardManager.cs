using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RewardManager : MonoBehaviour
{
    //private ItemInfoSet ItemInfoSet.instance;
    private SkillManagement _skillManagement;
    private PlayerCtrl _playerctrl;
    public GameObject RewardUI;

    public AudioSource sound;
    public AudioClip Snd_Reward;

    public GameObject[] ItemButtons;
    public GameObject[] Items;  

    private int[] randomNumbers = new int[3];
    private bool isSame;

    private static RewardManager _instance;   
    public static RewardManager Instance
    {
        get
        {           
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(RewardManager)) as RewardManager;

                if (_instance == null)
                {
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //ItemInfoSet.instance = GameObject.Find("ItemList").GetComponent<ItemInfoSet>();
        _skillManagement = SkillManagement.instance;
        _playerctrl = PlayerCtrl.playerInstance.GetComponent<PlayerCtrl>();
    } 

    void RandomNumberSet()
    {
        for (int index1 = 0; index1 < 3; index1++)
        {
            while (true)
            {
                randomNumbers[index1] = Random.Range(1, 23);
                isSame = false;
                for (int index2 = 0; index2 < index1; index2++)
                {
                    if (randomNumbers[index2] == randomNumbers[index1])
                    {
                        isSame = true;
                        break;
                    }
                }
                if (!isSame)
                {
                    break;
                }
            }
            //Debug.Log(randomNumbers[index1]);
        }
    }

    void ItemSet()
    {
        RewardUI.SetActive(true);
        RandomNumberSet();
        ItemInfoSet.instance.ItemAbilitySet();
        sound.PlayOneShot(Snd_Reward, 0.1f);
        for (int k = 0; k < randomNumbers.Length; k++)
        {
            for (int d = 0; d < ItemInfoSet.instance.Items.Count; d++)
            {
                /*if (randomNumbers[k] == d + 1)
                {
                    if (k == 0)
                    {
                        GameObject.Find("Image1").GetComponent<Image>().sprite = ItemInfoSet.instance.Items[d].ItemImage;
                        //ItemButton1.GetComponentInChildren<Text>().text = "<color=white><size=200>" + ItemInfoSet.instance.Items[d].ItemName + "</size></color>" + " " 
                        //    + "(Item Level: " + ItemInfoSet.instance.Items[d].ItemLevel + ")"
                        //    + "\n" + "\t" + "\n" + ItemInfoSet.instance.Items[d].ItemAbility;
                        
                    }
                    else if (k == 1)
                    {
                        GameObject.Find("Image2").GetComponent<Image>().sprite = ItemInfoSet.instance.Items[d].ItemImage;
                        //ItemButton2.GetComponentInChildren<Text>().text = "<color=white><size=200>" + ItemInfoSet.instance.Items[d].ItemName + "</size></color>" + " "
                        //    + "(Item Level: " + ItemInfoSet.instance.Items[d].ItemLevel + ")"
                        //    + "\n" + "\t" + "\n" + ItemInfoSet.instance.Items[d].ItemAbility;
                    }
                    else
                    {
                        GameObject.Find("Image3").GetComponent<Image>().sprite = ItemInfoSet.instance.Items[d].ItemImage;
                        //ItemButton3.GetComponentInChildren<Text>().text = "<color=white><size=200>" + ItemInfoSet.instance.Items[d].ItemName + "</size></color>" + " "
                        //    + "(Item Level: " + ItemInfoSet.instance.Items[d].ItemLevel + ")"
                        //    + "\n" + "\t" + "\n" + ItemInfoSet.instance.Items[d].ItemAbility;
                    }
                }*/
                if (randomNumbers[k] == d + 1)
                {
                    ItemButtons[k].GetComponentsInChildren<Image>()[1].sprite = ItemInfoSet.instance.Items[d].ItemImage;                   
                    ItemButtons[k].GetComponentsInChildren<Text>()[0].text = ItemInfoSet.instance.Items[d].ItemName;
                    ItemButtons[k].GetComponentsInChildren<Text>()[1].text = "Class: " + "<color=#FFFFFF>" + ItemInfoSet.instance.Items[d].element.ToString() + "</color>";
                    ItemButtons[k].GetComponentsInChildren<Text>()[2].text = "Skill Level: " + "<color=#FFFFFF>" + ItemInfoSet.instance.Items[d].ItemLevel.ToString() + "</color>";
                    ItemButtons[k].GetComponentsInChildren<Text>()[3].text = "<color=#FFFFFF>" + ItemInfoSet.instance.Items[d].Enhance + "</color>";
                    ItemButtons[k].GetComponentsInChildren<Text>()[4].text = "<color=#FFFFFF>" + ItemInfoSet.instance.Items[d].ItemAbility + "</color>";
                    //ItemButtons[k].transform.GetChild(0).GetComponent<Image>().sprite = ItemInfoSet.instance.Items[d].ItemImage;
                }
                
            }
        }        
    }

    public void ItemClick(int sellect)
    {
        for (int index = 0; index < ItemInfoSet.instance.Items.Count; index++)
        {
            if (ItemButtons[sellect-1].transform.GetChild(0).GetComponent<Image>().sprite == ItemInfoSet.instance.Items[index].ItemImage)
            {
                ItemInfoSet.instance.Items[index].ItemLevel += 1;
                switch (index)
                {
                    case 0:
                        _skillManagement.EarthImpaleActive = true;
                        if (ItemInfoSet.instance.Items[0].ItemLevel < 9)
                        {
                            switch (ItemInfoSet.instance.Items[0].ItemLevel)
                            {
                                case 1:
                                    ItemInfoSet.instance.Items[0].baseAttackDamage = 45;
                                    break;
                                case 2:
                                    ItemInfoSet.instance.Items[0].baseAttackDamage += 20;
                                    break;
                                case 3:
                                    ItemInfoSet.instance.Items[0].ProjectileNum += 1;
                                    break;
                                case 4:
                                    ItemInfoSet.instance.Items[0].baseCooltime = 13.0f;
                                    break;
                                case 5:
                                    ItemInfoSet.instance.Items[0].baseAttackDamage += 20;
                                    break;
                                case 6:
                                    ItemInfoSet.instance.Items[0].ProjectileNum += 1;
                                    break;
                                case 7:
                                    ItemInfoSet.instance.Items[0].baseCooltime = 12.0f;
                                    break;
                                case 8:
                                    ItemInfoSet.instance.Items[0].ProjectileNum += 1;
                                    break;
                            }
                        }
                        else
                        {
                            ItemInfoSet.instance.Items[0].baseAttackDamage += 10;
                        }
                        break;
                    case 1:
                        _skillManagement.WaterSplashActive = true;
                        if (ItemInfoSet.instance.Items[1].ItemLevel < 9)
                        {
                            switch (ItemInfoSet.instance.Items[1].ItemLevel)
                            {
                                case 1:
                                    ItemInfoSet.instance.Items[1].baseAttackDamage = 35;
                                    break;
                                case 2:
                                    ItemInfoSet.instance.Items[1].baseAttackDamage += 20;
                                    break;
                                case 3:
                                    ItemInfoSet.instance.Items[1].baseCooltime -= 1;
                                    break;
                                case 4:
                                    ItemInfoSet.instance.Items[1].ProjectileNum += 1;
                                    break;
                                case 5:
                                    ItemInfoSet.instance.Items[1].baseAttackDamage += 20;
                                    break;
                                case 6:
                                    ItemInfoSet.instance.Items[1].baseCooltime -= 1;
                                    break;
                                case 7:
                                    ItemInfoSet.instance.Items[1].ProjectileNum += 1;
                                    break;
                                case 8:
                                    ItemInfoSet.instance.Items[1].baseCooltime -= 1;
                                    break;
                            }
                        }
                        else
                        {
                            ItemInfoSet.instance.Items[1].baseAttackDamage += 20;
                        }
                        break;
                    case 2:
                        _skillManagement.ShadowBoltActive = true;
                        if (ItemInfoSet.instance.Items[2].ItemLevel < 9)
                        {
                            switch (ItemInfoSet.instance.Items[2].ItemLevel)
                            {
                                case 1:
                                    ItemInfoSet.instance.Items[2].baseAttackDamage = 50;
                                    break;
                                case 2:
                                    ItemInfoSet.instance.Items[2].baseAttackDamage += 20;
                                    break;
                                case 3:
                                    ItemInfoSet.instance.Items[2].baseProjectileScale = 13.0f;
                                    break;
                                case 4:
                                    ItemInfoSet.instance.Items[2].ProjectileNum += 1;
                                    break;
                                case 5:
                                    ItemInfoSet.instance.Items[2].baseAttackDamage += 20;
                                    break;
                                case 6:
                                    ItemInfoSet.instance.Items[2].basePenetration += 1;
                                    break;
                                case 7:
                                    ItemInfoSet.instance.Items[2].baseProjectileScale = 16.0f;
                                    break;
                                case 8:
                                    ItemInfoSet.instance.Items[2].ProjectileNum += 1;
                                    break;
                            }
                        }
                        else
                        {
                            ItemInfoSet.instance.Items[2].baseAttackDamage += 10;
                        }
                        break;
                    case 3:
                        _skillManagement.FireBallActive = true;
                        if (ItemInfoSet.instance.Items[3].ItemLevel < 9)
                        {
                            switch (ItemInfoSet.instance.Items[3].ItemLevel)
                            {
                                case 1:
                                    ItemInfoSet.instance.Items[3].baseAttackDamage = 15;
                                    break;
                                case 2:
                                    ItemInfoSet.instance.Items[3].baseAttackDamage += 10;
                                    break;
                                case 3:
                                    ItemInfoSet.instance.Items[3].baseProjectileScale *= 1.25f;
                                    break;
                                case 4:
                                    ItemInfoSet.instance.Items[3].ProjectileNum += 1;
                                    break;
                                case 5:
                                    ItemInfoSet.instance.Items[3].baseAttackDamage += 10;
                                    break;
                                case 6:
                                    ItemInfoSet.instance.Items[3].baseProjectileScale *= 1.25f;
                                    break;
                                case 7:
                                    ItemInfoSet.instance.Items[3].ProjectileNum += 1;
                                    break;
                                case 8:
                                    ItemInfoSet.instance.Items[3].AttackCount -= 1;
                                    break;
                            }
                        }
                        else
                        {
                            ItemInfoSet.instance.Items[3].baseAttackDamage += 5;
                        }
                        break;
                    case 4:
                        _skillManagement.ThunderBallActive = true;
                        if (ItemInfoSet.instance.Items[4].ItemLevel < 9)
                        {
                            switch (ItemInfoSet.instance.Items[4].ItemLevel)
                            {
                                case 1:
                                    ItemInfoSet.instance.Items[4].baseAttackDamage = 10;
                                    break;
                                case 2:
                                    ItemInfoSet.instance.Items[4].baseAttackDamage += 2;
                                    break;
                                case 3:
                                    ItemInfoSet.instance.Items[4].Percentage += 50;
                                    break;
                                case 4:
                                    ItemInfoSet.instance.Items[4].Percentage += 50;
                                    break;
                                case 5:
                                    ItemInfoSet.instance.Items[4].baseAttackDamage += 2;
                                    break;
                                case 6:
                                    ItemInfoSet.instance.Items[4].Percentage += 50;
                                    break;
                                case 7:
                                    ItemInfoSet.instance.Items[4].Percentage += 50;
                                    break;
                                case 8:
                                    ItemInfoSet.instance.Items[4].baseAttackDamage += 2;
                                    break;
                            }
                        }
                        else
                        {
                            ItemInfoSet.instance.Items[4].baseAttackDamage += 2;
                        }
                        break;
                    case 5:
                        _skillManagement.PetAttack = true;
                        //Debug.Log("asdasd");
                        if (ItemInfoSet.instance.Items[5].ItemLevel < 9)
                        {
                            switch (ItemInfoSet.instance.Items[5].ItemLevel)
                            {
                                case 1:
                                    ItemInfoSet.instance.Items[5].baseAttackDamage = 10;
                                    break;
                                case 2:
                                    ItemInfoSet.instance.Items[5].ProjectileNum += 1;
                                    break;
                                case 3:
                                    ItemInfoSet.instance.Items[5].baseAttackDamage += 5;
                                    break;
                                case 4:
                                    ItemInfoSet.instance.Items[5].ProjectileNum += 1;
                                    break;
                                case 5:
                                    ItemInfoSet.instance.Items[5].baseAttackDamage += 5;
                                    break;
                                case 6:
                                    ItemInfoSet.instance.Items[5].ProjectileNum += 1;
                                    break;
                                case 7:
                                    ItemInfoSet.instance.Items[5].baseAttackDamage += 5;
                                    break;
                                case 8:
                                    ItemInfoSet.instance.Items[5].ProjectileNum += 1;
                                    break;
                            }
                        }
                        else
                        {
                            ItemInfoSet.instance.Items[5].baseAttackDamage += 2;
                        }
                        break;
                    case 6:
                        _skillManagement.EarthSawActive = true;
                        if (ItemInfoSet.instance.Items[6].ItemLevel < 9)
                        {
                            switch (ItemInfoSet.instance.Items[6].ItemLevel)
                            {
                                case 1:
                                    ItemInfoSet.instance.Items[6].baseAttackDamage = 4;
                                    break;
                                case 2:
                                    ItemInfoSet.instance.Items[6].ProjectileNum += 1;
                                    break;
                                case 3:
                                    ItemInfoSet.instance.Items[6].baseAttackDamage += 2;
                                    break;
                                case 4:
                                    ItemInfoSet.instance.Items[6].baseCooltime = 5.0f;
                                    break;
                                case 5:
                                    ItemInfoSet.instance.Items[6].baseAttackDamage += 2;
                                    break;
                                case 6:
                                    ItemInfoSet.instance.Items[6].ProjectileNum += 1;
                                    break;
                                case 7:
                                    ItemInfoSet.instance.Items[6].baseCooltime = 4.0f;
                                    break;
                                case 8:
                                    ItemInfoSet.instance.Items[6].ProjectileNum += 1;
                                    break;
                            }
                        }
                        else
                        {
                            ItemInfoSet.instance.Items[6].baseAttackDamage += 2;
                        }
                        break;
                    case 7:
                        _skillManagement.WaterWhipActive = true;
                        if (ItemInfoSet.instance.Items[7].ItemLevel < 9)
                        {
                            switch (ItemInfoSet.instance.Items[7].ItemLevel)
                            {
                                case 1:
                                    ItemInfoSet.instance.Items[7].baseAttackDamage = 20;
                                    break;
                                case 2:
                                    ItemInfoSet.instance.Items[7].baseAttackDamage += 15;
                                    break;
                                case 3:
                                    ItemInfoSet.instance.Items[7].ProjectileNum += 1;
                                    break;
                                case 4:
                                    ItemInfoSet.instance.Items[7].ProjectileNum += 1;
                                    break;
                                case 5:
                                    ItemInfoSet.instance.Items[7].baseAttackDamage += 15;
                                    break;
                                case 6:
                                    ItemInfoSet.instance.Items[7].baseAttackDamage += 15;
                                    break;
                                case 7:
                                    ItemInfoSet.instance.Items[7].ProjectileNum += 1;
                                    break;
                                case 8:
                                    ItemInfoSet.instance.Items[7].baseAttackDamage += 15;
                                    break;
                            }
                        }
                        else
                        {
                            ItemInfoSet.instance.Items[7].baseAttackDamage += 10;
                        }
                        break;
                    case 8:
                        _skillManagement.WindCutterActive = true;
                        if (ItemInfoSet.instance.Items[8].ItemLevel < 9)
                        {
                            switch (ItemInfoSet.instance.Items[8].ItemLevel)
                            {
                                case 1:
                                    ItemInfoSet.instance.Items[8].baseAttackDamage = 10;
                                    break;
                                case 2:
                                    ItemInfoSet.instance.Items[8].ProjectileNum += 1;
                                    break;
                                case 3:
                                    ItemInfoSet.instance.Items[8].baseAttackDamage += 5;
                                    break;
                                case 4:
                                    ItemInfoSet.instance.Items[8].ProjectileNum += 1;
                                    break;
                                case 5:
                                    ItemInfoSet.instance.Items[8].baseAttackDamage += 5;
                                    break;
                                case 6:
                                    ItemInfoSet.instance.Items[8].ProjectileNum += 2;
                                    break;
                                case 7:
                                    ItemInfoSet.instance.Items[8].baseAttackDamage += 5;
                                    break;
                                case 8:
                                    ItemInfoSet.instance.Items[8].ProjectileNum += 3;
                                    break;
                            }
                        }
                        else
                        {
                            ItemInfoSet.instance.Items[8].baseAttackDamage += 5;
                        }
                        break;
                    case 9:
                        _skillManagement.FireBeamActive = true;
                        if (ItemInfoSet.instance.Items[9].ItemLevel < 9)
                        {
                            switch (ItemInfoSet.instance.Items[9].ItemLevel)
                            {
                                case 1:
                                    ItemInfoSet.instance.Items[9].baseAttackDamage = 70;
                                    break;
                                case 2:
                                    ItemInfoSet.instance.Items[9].baseAttackDamage += 20;
                                    break;
                                case 3:
                                    ItemInfoSet.instance.Items[9].AttackCount -= 1;
                                    break;
                                case 4:
                                    ItemInfoSet.instance.Items[9].baseAttackDamage += 20;
                                    break;
                                case 5:
                                    ItemInfoSet.instance.Items[9].ProjectileNum += 1;
                                    break;
                                case 6:
                                    //Debug.Log("타격 횟수 1 중가");
                                    break;
                                case 7:
                                    ItemInfoSet.instance.Items[9].AttackCount -= 1;
                                    break;
                                case 8:
                                    ItemInfoSet.instance.Items[9].ProjectileNum += 1;
                                    break;
                            }
                        }
                        else
                        {
                            ItemInfoSet.instance.Items[9].baseAttackDamage += 10;
                        }
                        break;
                    case 10:
                        _skillManagement.ThunderExplosionActive = true;
                        if (ItemInfoSet.instance.Items[10].ItemLevel < 9)
                        {
                            switch (ItemInfoSet.instance.Items[10].ItemLevel)
                            {
                                case 1:
                                    ItemInfoSet.instance.Items[10].baseAttackDamage = 15;
                                    break;
                                case 2:
                                    ItemInfoSet.instance.Items[10].Percentage += 100;
                                    break;
                                case 3:
                                    ItemInfoSet.instance.Items[10].ProjectileNum += 2;
                                    break;
                                case 4:
                                    ItemInfoSet.instance.Items[10].baseAttackDamage = 15;
                                    break;
                                case 5:
                                    ItemInfoSet.instance.Items[10].baseProjectileScale *= 1.5f;
                                    break;
                                case 6:
                                    ItemInfoSet.instance.Items[10].ProjectileNum += 2;
                                    break;
                                case 7:
                                    ItemInfoSet.instance.Items[10].Percentage += 100;
                                    break;
                                case 8:
                                    ItemInfoSet.instance.Items[10].baseProjectileScale *= 1.5f;
                                    break;
                            }
                        }
                        else
                        {
                            ItemInfoSet.instance.Items[10].baseAttackDamage += 5;
                        }
                        break;
                    case 11:
                        _playerctrl.Skill_B = true;
                        //Items[11].GetComponent<EnhancedArrow>().ItemEnhance();                           
                        break;
                    case 12:
                        _skillManagement.EarthCrashActive = true;
                        if (ItemInfoSet.instance.Items[12].ItemLevel < 9)
                        {
                            switch (ItemInfoSet.instance.Items[12].ItemLevel)
                            {
                                case 1:
                                    ItemInfoSet.instance.Items[12].baseAttackDamage = 20;
                                    break;
                                case 2:
                                    ItemInfoSet.instance.Items[12].baseAttackDamage += 20;
                                    break;
                                case 3:
                                    ItemInfoSet.instance.Items[12].baseProjectileScale *= 1.5f;
                                    break;
                                case 4:
                                    ItemInfoSet.instance.Items[12].AttackCount -= 1;
                                    break;
                                case 5:
                                    ItemInfoSet.instance.Items[12].baseAttackDamage += 20;
                                    break;
                                case 6:
                                    ItemInfoSet.instance.Items[12].baseProjectileScale *= 1.5f;
                                    break;
                                case 7:
                                    ItemInfoSet.instance.Items[12].AttackCount -= 1;
                                    break;
                                case 8:
                                    ItemInfoSet.instance.Items[12].baseProjectileScale *= 1.5f;
                                    break;
                            }
                        }
                        else
                        {
                            ItemInfoSet.instance.Items[12].baseAttackDamage += 10;
                        }
                        break;
                    case 13:
                        _skillManagement.WaterMineActive = true;
                        if (ItemInfoSet.instance.Items[13].ItemLevel < 9)
                        {
                            switch (ItemInfoSet.instance.Items[13].ItemLevel)
                            {
                                case 1:
                                    ItemInfoSet.instance.Items[13].baseAttackDamage = 10;
                                    break;
                                case 2:
                                    ItemInfoSet.instance.Items[13].ProjectileNum += 1;
                                    break;
                                case 3:
                                    ItemInfoSet.instance.Items[13].baseAttackDamage = 10;
                                    break;
                                case 4:
                                    ItemInfoSet.instance.Items[13].baseDuration -= 1;
                                    break;
                                case 5:
                                    ItemInfoSet.instance.Items[13].baseAttackDamage = 10;
                                    break;
                                case 6:
                                    ItemInfoSet.instance.Items[13].ProjectileNum += 1;
                                    break;
                                case 7:
                                    ItemInfoSet.instance.Items[13].baseDuration -= 1;
                                    break;
                                case 8:
                                    ItemInfoSet.instance.Items[13].baseProjectileScale *= 1.5f;
                                    break;
                            }
                        }
                        else
                        {
                            ItemInfoSet.instance.Items[13].baseAttackDamage += 3;
                        }
                        break;
                    case 14:
                        _skillManagement.WindSlashActive = true;
                        if (ItemInfoSet.instance.Items[14].ItemLevel < 9)
                        {
                            switch (ItemInfoSet.instance.Items[14].ItemLevel)
                            {
                                case 1:
                                    ItemInfoSet.instance.Items[14].baseAttackDamage = 5;
                                    break;
                                case 2:
                                    ItemInfoSet.instance.Items[14].ProjectileNum += 1;
                                    break;
                                case 3:
                                    ItemInfoSet.instance.Items[14].baseAttackDamage = 5;
                                    break;
                                case 4:
                                    ItemInfoSet.instance.Items[14].ProjectileNum += 1;
                                    break;
                                case 5:
                                    ItemInfoSet.instance.Items[14].baseAttackDamage = 5;
                                    break;
                                case 6:
                                    ItemInfoSet.instance.Items[14].ProjectileNum += 1;
                                    break;
                                case 7:
                                    ItemInfoSet.instance.Items[14].baseAttackDamage = 10;
                                    break;
                                case 8:
                                    ItemInfoSet.instance.Items[14].ProjectileNum += 2;
                                    break;
                            }
                        }
                        else
                        {
                            ItemInfoSet.instance.Items[14].baseAttackDamage += 5;
                        }
                        break;
                    case 15:
                        _skillManagement.FireBiteActive = true;
                        if (ItemInfoSet.instance.Items[15].ItemLevel < 9)
                        {
                            switch (ItemInfoSet.instance.Items[15].ItemLevel)
                            {
                                case 1:
                                    ItemInfoSet.instance.Items[15].baseAttackDamage = 20;
                                    break;
                                case 2:
                                    ItemInfoSet.instance.Items[15].baseAttackDamage += 10;
                                    break;
                                case 3:
                                    ItemInfoSet.instance.Items[15].AttackCount -= 1;
                                    break;
                                case 4:
                                    //Debug.Log("타격 횟수 1회 증가");
                                    break;
                                case 5:
                                    ItemInfoSet.instance.Items[15].ProjectileNum += 1;
                                    break;
                                case 6:
                                    ItemInfoSet.instance.Items[15].baseDuration += 2.0f;
                                    break;
                                case 7:
                                    ItemInfoSet.instance.Items[15].AttackCount -= 1;
                                    break;
                                case 8:
                                    ItemInfoSet.instance.Items[15].ProjectileNum += 1;
                                    break;
                            }
                        }
                        else
                        {
                            ItemInfoSet.instance.Items[15].baseAttackDamage += 5;
                        }
                        break;
                    case 16:
                        _skillManagement.ThunderStormActive = true;
                        if (ItemInfoSet.instance.Items[16].ItemLevel < 9)
                        {
                            switch (ItemInfoSet.instance.Items[16].ItemLevel)
                            {
                                case 1:
                                    ItemInfoSet.instance.Items[16].baseAttackDamage = 20;
                                    break;
                                case 2:
                                    ItemInfoSet.instance.Items[16].baseAttackDamage += 10;
                                    break;
                                case 3:
                                    ItemInfoSet.instance.Items[16].Percentage += 50;
                                    break;
                                case 4:
                                    ItemInfoSet.instance.Items[16].Percentage += 50;
                                    break;
                                case 5:
                                    ItemInfoSet.instance.Items[16].baseAttackDamage += 10;
                                    break;
                                case 6:
                                    ItemInfoSet.instance.Items[16].Percentage += 50;
                                    break;
                                case 7:
                                    ItemInfoSet.instance.Items[16].Percentage += 50;
                                    break;
                                case 8:
                                    ItemInfoSet.instance.Items[16].baseAttackDamage += 10;
                                    break;
                            }
                        }
                        else
                        {
                            ItemInfoSet.instance.Items[16].baseAttackDamage += 3;
                        }
                        break;
                    case 17:
                        _skillManagement.EarthSpikeActive = true;
                        if (ItemInfoSet.instance.Items[17].ItemLevel < 9)
                        {
                            switch (ItemInfoSet.instance.Items[17].ItemLevel)
                            {
                                case 1:
                                    ItemInfoSet.instance.Items[17].baseAttackDamage = 5;
                                    break;
                                case 2:
                                    ItemInfoSet.instance.Items[17].baseAttackDamage += 3;
                                    break;
                                case 3:
                                    ItemInfoSet.instance.Items[17].ProjectileNum += 2;
                                    break;
                                case 4:
                                    ItemInfoSet.instance.Items[17].ProjectileNum += 2;
                                    break;
                                case 5:
                                    ItemInfoSet.instance.Items[17].baseAttackDamage += 3;
                                    break;
                                case 6:
                                    ItemInfoSet.instance.Items[17].baseProjectileScale *= 1.25f;
                                    break;
                                case 7:
                                    ItemInfoSet.instance.Items[17].ProjectileNum += 2;
                                    break;
                                case 8:
                                    ItemInfoSet.instance.Items[17].baseProjectileScale *= 1.25f;
                                    break;
                            }
                        }
                        else
                        {
                            ItemInfoSet.instance.Items[17].baseAttackDamage += 2;
                        }
                        break;
                    case 18:
                        _skillManagement.IceShotActive = true;
                        if (ItemInfoSet.instance.Items[18].ItemLevel < 9)
                        {
                            switch (ItemInfoSet.instance.Items[18].ItemLevel)
                            {
                                case 1:
                                    ItemInfoSet.instance.Items[18].baseAttackDamage = 15;
                                    break;
                                case 2:
                                    ItemInfoSet.instance.Items[18].baseAttackDamage += 5;
                                    break;
                                case 3:
                                    ItemInfoSet.instance.Items[18].ProjectileNum += 1;
                                    break;
                                case 4:
                                    ItemInfoSet.instance.Items[18].baseAttackDamage += 5;
                                    break;
                                case 5:
                                    ItemInfoSet.instance.Items[18].ProjectileNum += 1;
                                    break;
                                case 6:
                                    ItemInfoSet.instance.Items[18].baseCooltime -= 1.0f;
                                    break;
                                case 7:
                                    ItemInfoSet.instance.Items[18].baseAttackDamage += 5;
                                    break;
                                case 8:
                                    ItemInfoSet.instance.Items[18].baseCooltime -= 1.0f;
                                    break;
                            }
                        }
                        else
                        {
                            ItemInfoSet.instance.Items[18].baseAttackDamage += 5;
                        }
                        break;
                    case 19:
                        _skillManagement.TornadoActive = true;
                        if (ItemInfoSet.instance.Items[19].ItemLevel < 9)
                        {
                            switch (ItemInfoSet.instance.Items[19].ItemLevel)
                            {
                                case 1:
                                    ItemInfoSet.instance.Items[19].baseAttackDamage = 5;
                                    break;
                                case 2:
                                    ItemInfoSet.instance.Items[19].baseAttackDamage += 1;
                                    break;
                                case 3:
                                    ItemInfoSet.instance.Items[19].baseProjectileScale += 2.0f;
                                    break;
                                case 4:
                                    ItemInfoSet.instance.Items[19].baseCooltime -= 0.05f;
                                    break;
                                case 5:
                                    ItemInfoSet.instance.Items[19].baseAttackDamage += 4;
                                    break;
                                case 6:
                                    ItemInfoSet.instance.Items[19].baseProjectileScale += 2.0f;
                                    break;
                                case 7:
                                    ItemInfoSet.instance.Items[19].baseCooltime -= 0.05f;
                                    break;
                                case 8:
                                    ItemInfoSet.instance.Items[19].baseProjectileScale += 2.0f;
                                    break;
                            }
                        }
                        else
                        {
                            ItemInfoSet.instance.Items[19].baseAttackDamage += 4;
                        }
                        break;
                    case 20:
                        _skillManagement.LavasActive = true;
                        if (ItemInfoSet.instance.Items[20].ItemLevel < 9)
                        {
                            switch (ItemInfoSet.instance.Items[20].ItemLevel)
                            {
                                case 1:
                                    ItemInfoSet.instance.Items[20].baseAttackDamage = 3;
                                    break;
                                case 2:
                                    ItemInfoSet.instance.Items[20].baseAttackDamage += 5;
                                    break;
                                case 3:
                                    ItemInfoSet.instance.Items[20].baseProjectileScale *= 1.25f;
                                    break;
                                case 4:
                                    ItemInfoSet.instance.Items[20].baseDuration += 2.0f;
                                    break;
                                case 5:
                                    ItemInfoSet.instance.Items[20].baseAttackDamage += 5;
                                    break;
                                case 6:
                                    ItemInfoSet.instance.Items[20].baseProjectileScale *= 1.25f;
                                    break;
                                case 7:
                                    ItemInfoSet.instance.Items[20].baseDuration += 2.0f;
                                    break;
                                case 8:
                                    //Debug.Log("이동속도 30% 감소");
                                    break;
                            }
                        }
                        else
                        {
                            ItemInfoSet.instance.Items[20].baseAttackDamage += 5;
                        }
                        break;
                    case 21:
                        _skillManagement.ThunderVeilActive = true;
                        if (ItemInfoSet.instance.Items[21].ItemLevel < 9)
                        {
                            switch (ItemInfoSet.instance.Items[21].ItemLevel)
                            {
                                case 1:
                                    ItemInfoSet.instance.Items[21].baseAttackDamage = 10;
                                    break;
                                case 2:
                                    ItemInfoSet.instance.Items[21].baseAttackDamage += 5;
                                    break;
                                case 3:
                                    ItemInfoSet.instance.Items[21].baseDuration += 2.0f;
                                    break;
                                case 4:
                                    ItemInfoSet.instance.Items[21].baseAttackDamage += 5;
                                    break;
                                case 5:
                                    //Debug.Log("방어력 증가");
                                    break;
                                case 6:
                                    ItemInfoSet.instance.Items[21].baseDuration += 2.0f;
                                    break;
                                case 7:
                                    ItemInfoSet.instance.Items[21].baseProjectileScale *= 1.5f;
                                    break;
                                case 8:
                                    //Debug.Log("방어력 증가");
                                    break;
                            }
                        }
                        else
                        {
                            ItemInfoSet.instance.Items[4].baseAttackDamage += 3;
                        }
                        break;
                }
            }
        }
  

        Time.timeScale = 1.0f;
        RewardUI.SetActive(false);
    }    
}
