using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IObjectItem
{
    void OnPickup();
    ItemData ClickItem();
    Status GetStatus();
}
public class ObjectItem : MonoBehaviour, IObjectItem
{
    public ItemData[] itemList;
    ItemData item;

    Status itemStatus;
    public void SetItemSkin(string skin)
    {
        switch (skin)
        {
            case "DiamondNecklace_20240910(Clone)":
                item = itemList[1];
                break;
            case "DiamondRing_20240910(Clone)":
                item = itemList[4];
                break;
            case "Necklace_20240910(Clone)":
                item = itemList[0];
                break;
            case "OrbNecklace_20240910(Clone)":
                item = itemList[2];
                break;
            case "OrbRing_20240910(Clone)":
                item = itemList[5];
                break;
            case "Ring_20240910(Clone)":
                item = itemList[3];
                break;
        }
    }

    public void SetItemStatus(Status status)
    {
        itemStatus = status;


            Debug.Log($"----Item Name: {itemStatus.ObjectName}");
            Debug.Log($"----Max HP: {itemStatus.MaxHP}");
            Debug.Log($"----Current HP: {itemStatus.CurrentHP}");
            Debug.Log($"----Attack Damage: {itemStatus.AttackDamage}");
            Debug.Log($"----Defense: {itemStatus.Defense}");
            Debug.Log($"----Attack Speed: {itemStatus.AttackSpeed}");

            // Skill Percent �迭 ���
            for (int i = 0; i < itemStatus.SkillPercent.Length; i++)
            {
                Debug.Log($"----Skill Percent[{i}]: {status.SkillPercent[i]}");
            }
    }
    
    /*[Header("������ �̹���")]
    public SpriteRenderer itemImage;
    void Start()
    {
        itemImage.sprite = itemData.sprite;
    }*/

/*    public string Name
    {
        get { return itemData.itemName; }
    }
    public Sprite Image
    {
        get { return itemData.sprite; }
    }*/
    public void OnPickup()
    {
        // TODO: Add logic what happens when item is picked up by player
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
    public  ItemData ClickItem()
    {
        return this.item;
    }
    public Status GetStatus()
    {
        return this.itemStatus;
    }
}