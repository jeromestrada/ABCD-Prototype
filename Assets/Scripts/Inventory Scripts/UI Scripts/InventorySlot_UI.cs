using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot_UI : MonoBehaviour
{
    [SerializeField] private Image cardSprite;
    [SerializeField] private TextMeshProUGUI cardCount;
    [SerializeField] private InventorySlot assignedInventorySlot;

    private Button button;

    public InventorySlot AssignedInventorySlot => assignedInventorySlot;
    public InventoryDisplay ParentDisplay {get; private set;}

    private void Awake()
    {
        ClearSlot();

        button = GetComponent<Button>();
        button?.onClick.AddListener(OnUISlotClick);
        // since the UI slot is inside a container that is a child of the Parent Display
        // we have to access the parent of the parent of this object
        ParentDisplay = transform.parent.parent.GetComponent<InventoryDisplay>(); 
    }

    public void Init(InventorySlot slot)
    {
        assignedInventorySlot = slot; // ties the ui to the system.
        UpdateUISlot(slot);
    }

    public void UpdateUISlot(InventorySlot slot)
    {
        if(slot.Card != null)
        {
            cardSprite.sprite = slot.Card.cardIcon;
            cardSprite.color = Color.white;
            if (slot.StackSize > 1) cardCount.text = slot.StackSize.ToString();
            else cardCount.text = "";
        }
        else
        {
            ClearSlot();
        }
    }

    public void UpdateUISlot()
    {
        if(assignedInventorySlot != null) UpdateUISlot(assignedInventorySlot);
    }

    public void ClearSlot()
    {
        assignedInventorySlot?.ClearSlot();
        cardSprite.sprite = null;
        cardSprite.color = Color.clear;
        cardCount.text = "";
    }

    public void OnUISlotClick()
    {
        // access display class function.
        ParentDisplay?.SlotClicked(this);
        // functionality will depend on the type of display this slot's display parent is.
        // see parentDisplay's SlotClicked for more details
        
    }
}
