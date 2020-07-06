using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<InventoryItem> items;

    public event Action<string> OnInventoryChanged;
    public event Action<string> OnInventoryUIText;

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
    }
    public void AddItem(InventoryItem item)
    {
        items.Add(item);
        OnInventoryChanged.Invoke("Added " + item.Name);
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
                OnInventoryChanged.Invoke("Used " + inventoryItem.Name);
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
