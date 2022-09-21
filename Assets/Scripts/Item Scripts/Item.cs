using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="New Item", menuName ="Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name;
    public string description;
    public Sprite icon;

    public virtual void Use()
    {
        // use item, when inherited define usage in sub class.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
