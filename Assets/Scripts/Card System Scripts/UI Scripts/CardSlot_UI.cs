using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardSlot_UI : Slot_UI
{
    [SerializeField] private Image cardSprite;
    [SerializeField] private TextMeshProUGUI cardCount;
    [SerializeField] private PlayerCardSlot assignedInventorySlot;

    private Button button;

    public PlayerCardSlot AssignedInventorySlot => assignedInventorySlot;
    

    private void Awake()
    {
        ClearSlot();

        button = GetComponent<Button>();
        button?.onClick.AddListener(OnUISlotClick);
        
    }

    public void Init(PlayerCardSlot slot)
    {
        assignedInventorySlot = slot; // ties the ui to the system.
        UpdateUISlot(slot);
    }

    public void UpdateUISlot(PlayerCardSlot slot)
    {
        if(slot.Card != null)
        {
            cardSprite.sprite = slot.Card.cardIcon;
            cardSprite.color = Color.white;
            if (slot.RemainingUses > 1) cardCount.text = slot.RemainingUses.ToString();
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
}
