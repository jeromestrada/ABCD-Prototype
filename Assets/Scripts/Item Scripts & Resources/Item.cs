using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="New Item", menuName ="Inventory/Item")]
public class Item : ScriptableObject
{
    protected EquipmentManager equipmentManager;
    new public string name;
    [TextArea(4,4)]
    public string description;
    public Sprite icon;
    public SkinnedMeshRenderer mesh;
    protected ItemType _itemType;

    public ItemType ItemType => _itemType;
    public virtual void Use()
    {
        // use item, when inherited define usage in sub class.
    }
}

public enum ItemType { Weapon, Protection, Utility}
