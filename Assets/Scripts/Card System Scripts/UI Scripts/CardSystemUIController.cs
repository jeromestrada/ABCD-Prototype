using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardSystemUIController : MonoBehaviour
{
    public DynamicCardSystemDisplay inventoryPanel;

    private void Awake()
    {
        inventoryPanel.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        CardSystemHolder.OnDynamicCardSystemDisplayRequested += DisplayInventory;
    }
    private void OnDisable()
    {
        CardSystemHolder.OnDynamicCardSystemDisplayRequested -= DisplayInventory;
    }
    // Update is called once per frame
    void Update()
    {
        if(inventoryPanel.gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape)) inventoryPanel.gameObject.SetActive(false);
    }

    void DisplayInventory(CardSystem invToDisplay)
    {
        inventoryPanel.gameObject.SetActive(true); 
        inventoryPanel.RefreshDynamicInventory(invToDisplay);
    }
}
