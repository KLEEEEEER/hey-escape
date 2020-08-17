using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<InventoryItem> items;

    public event Action<string> OnInventoryChanged;
    public event Action<string> OnInventoryUIText;

    public LocalizedString youFoundStringLocalized;
    private string youFoundString;
    public LocalizedString youUsedStringLocalized;
    private string youUsedString;

    private static Inventory s_Instance = null;

    public static Inventory instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(Inventory)) as Inventory;
            }

            /*if (s_Instance == null)
            {
                var obj = new GameObject("Inventory");
                s_Instance = obj.AddComponent<Inventory>();
            }*/

            return s_Instance;
        }
    }

    private void OnDestroy()
    {
        s_Instance = null;
        Destroy(gameObject);
    }

    private void Start()
    {
        items = new List<InventoryItem>();

        //youFoundStringLocalized.Arguments.Add(this); // Add our new argument
        youFoundStringLocalized.RegisterChangeHandler(UpdateFoundString);
        youUsedStringLocalized.RegisterChangeHandler(UpdateUsedString);
    }
    void UpdateFoundString(string s) { youFoundString = s; }
    void UpdateUsedString(string s) { youUsedString = s; }


    public void AddItem(InventoryItem item)
    {
        item.nameLocalized.RegisterChangeHandler(item.UpdateString);
        items.Add(item);
        OnInventoryChanged.Invoke(youFoundString + " " + item.nameString);
    }

    public InventoryItem HasItem(System.Type type)
    {
        foreach (InventoryItem inventoryItem in items)
        {
            if (inventoryItem.GetType() == type)
            {
                return inventoryItem;
            }
        }
        return null;
    }

    public void UseItem(System.Type type)
    {
        foreach (InventoryItem inventoryItem in items)
        {
            if (inventoryItem.GetType() == type)
            {
                items.Remove(inventoryItem);
                OnInventoryChanged.Invoke(youUsedString + " " + inventoryItem.nameString);
                return;
            }
        }
    }

    public List<InventoryItem> GetItemsList()
    {
        return items;
    }

    public void ShowInventoryMessage(string text)
    {
        OnInventoryUIText.Invoke(text);
    }
}
