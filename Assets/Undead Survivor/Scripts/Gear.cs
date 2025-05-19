using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate;

    public void Init(ItemData data)
    {
        // Basic Set
        name = "Gear" + data.itemID;
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;

        // Property Set
        type = data.itemtype;
        rate = data.damages[0];
        ApplyGear();
    }


    public void LevelUP(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }

    void ApplyGear()
    {
        switch (type)
        {
            case ItemData.ItemType.Glove:
                RateUp();
                break;

            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
        }
    }


    void RateUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach (Weapon weapon in weapons)
        {
            switch (weapon.id)
            {
                case 0:
                    float speed = 150; // 원래는 Character.WeaponSpeed 를 곱해주는데 지금은 캐릭터별 그게 없어서 필요 없을듯
                    weapon.speed = speed + (speed * rate);
                    break;

                default:
                    speed = 0.5f;
                    weapon.speed = speed * (1f - rate);
                    break;
            }
        }
    }


    void SpeedUp()
    {
        float speed = 5;
        GameManager.instance.player.speed = speed + (speed * rate);
    }
}
