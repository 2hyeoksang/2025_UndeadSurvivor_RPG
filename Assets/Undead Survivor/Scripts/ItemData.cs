using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { Melee, Range, Glove, Shoe, Heal }

    [Header("# Main Info")]
    public ItemType itemtype;
    public int itemID;
    public string itemName;
    [TextArea]
    public string itemSetting;
    [TextArea]
    public string itemDesc;
    public Sprite itemIcon;

    [Header("# Level Data")]
    public float baseDamage;
    public int baseCount;
    public float[] damages;
    public int[] counts;

    [Header("# Weapon")]
    public GameObject projectile;
    //public Sprite hand;   얘는 아마 손에 무기 안쥐게할거라서 쓸모 없을거같음
}
