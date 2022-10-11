using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//, IPointerEnterHandler, IPointerExitHandler

public class ItemUISet : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject ItemUI;

    //private ItemInfoSet ItemInfoSet.instance;

    private void Start()
    {
        //ItemInfoSet.instance = GameObject.Find("ItemList").GetComponent<ItemInfoSet>();
    }
  
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject pointerEnter = eventData.pointerEnter;


        ItemUI.transform.GetChild(0).GetComponent<Image>().sprite = gameObject.transform.GetChild(1).GetComponent<Image>().sprite;
        ItemUITextSet();

        ItemUI.SetActive(true);      
    }
    
    void ItemUITextSet()
    {
        for (int i = 0; i < ItemInfoSet.instance.Items.Count; i++)
        {
            if (ItemUI.transform.GetChild(0).GetComponent<Image>().sprite == ItemInfoSet.instance.Items[i].ItemImage)
            {
                ItemUI.transform.GetChild(1).GetComponent<Text>().text = ItemInfoSet.instance.Items[i].ItemAbility;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {      
        ItemUI.SetActive(false);
    }
}
