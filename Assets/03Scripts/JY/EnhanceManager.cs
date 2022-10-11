using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnhanceManager : MonoBehaviour
{  
    private SkillManagement _skillManagement;
    
    
    // Start is called before the first frame update
    void Start()
    {
        ItemInfoSet.instance = GameObject.Find("ItemList").GetComponent<ItemInfoSet>();
        _skillManagement = SkillManagement.instance;



        IsEnhanced();
    }

    void IsEnhanced()
    {       
        switch(_skillManagement.ElementClass)
        {
            case "Fire":
                ItemInfoSet.instance.Items[3].baseProjectileScale *= 2;
                ItemInfoSet.instance.Items[9].baseProjectileScale *= 2;
                ItemInfoSet.instance.Items[15].baseProjectileSpeed *= 2;
                ItemInfoSet.instance.Items[20].baseCooltime -= 3;
                break;
            case "Wind":
                //shadow bolt´Â skillmanagement¿¡¼­
                ItemInfoSet.instance.Items[3].ProjectileNum *= 2;
                ItemInfoSet.instance.Items[19].ProjectileNum += 1;
                ItemInfoSet.instance.Items[14].baseProjectileScale *= 2;
                break;
            case "Water":
                ItemInfoSet.instance.Items[7].baseProjectileScale *= 2;
                ItemInfoSet.instance.Items[1].baseProjectileScale *= 2;
                break;
            case "Earth":
                ItemInfoSet.instance.Items[0].baseCooltime /= 2;
                break;
            case "Thunder":
                ItemInfoSet.instance.Items[16].baseProjectileScale *= 1.5f;
                break;
        }
    }


}
