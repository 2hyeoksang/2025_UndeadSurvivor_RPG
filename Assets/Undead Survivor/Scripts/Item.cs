using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData data;
    public int level;
    public Weapon weapon;
    public Gear gear;

    Image icon;
    Text textlevel;
    Text textName;
    Text textDesc;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textlevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];

        textName.text = data.itemName;
    }

    private void LateUpdate()
    {

        switch (data.itemtype)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                textlevel.text = "Lv." + (level);
                if (level == 0)
                {
                    textDesc.text = string.Format(data.itemSetting, data.baseDamage, data.baseCount);
                }
                else
                {
                    textDesc.text = string.Format(data.itemDesc, data.damages[Mathf.Min(level, data.damages.Length - 1)] * 100, data.counts[Mathf.Min(level, data.counts.Length - 1)]);
                }
                break;

            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                textlevel.text = "Lv." + (level);
                textDesc.text = string.Format(data.itemDesc, data.damages[Mathf.Min(level, data.damages.Length - 1)] * 100);
                break;

            default:
                textDesc.text = string.Format(data.itemDesc);
                break;
        }
    }


    public void OnClick()
    {
        switch (data.itemtype)
        {
             case ItemData.ItemType.Melee:
             case ItemData.ItemType.Range:
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(data);
                }

                else
                {
                   float nextDamage = Weapon.instance.damage;
                   int nextCount = 0;

                   nextDamage += Weapon.instance.damage * data.damages[Mathf.Min(level, data.damages.Length - 1)];
                   nextCount += data.counts[Mathf.Min(level, data.counts.Length - 1)];

                   weapon.LevelUp(nextDamage, nextCount);
                }
                 level++;
                 break;

             case ItemData.ItemType.Glove:
             case ItemData.ItemType.Shoe:
                  if (level == 0)
                  {
                     GameObject newGear = new GameObject();
                     gear = newGear.AddComponent<Gear>();
                     gear.Init(data);
                  }

                  else
                  {
                      float nextRate = data.damages[Mathf.Min(level, data.damages.Length - 1)];
                      gear.LevelUP(nextRate);
                  }
                  level++;
                  break;

             case ItemData.ItemType.Heal:
                GameManager.instance.health = GameManager.instance.maxHealth;
                break;
        }
            
        GameManager.instance.skillPoint--;

        //if (level == data.damages.Length )
        //{
        //    GetComponent<Button>().interactable = false;
        //} 
    }
}
