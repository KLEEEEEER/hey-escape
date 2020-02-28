using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] GameObject InventoryContent;
    [SerializeField] GameObject InventoryItemUIPrefab;

    Vector3 hidingPosition;
    [SerializeField] float showingNewItemsTime = 2f;
    [SerializeField] float smoothHiding;
    [SerializeField] Transform defaultPosition;
    bool isHidden = true;

    private void Start()
    {
        hidingPosition = transform.position;
    }

    private void OnEnable()
    {
        Inventory.instance.OnInventoryChanged += UpdateUI;
    }
    private void OnDisable()
    {
        Inventory.instance.OnInventoryChanged -= UpdateUI;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isHidden = !isHidden;
        }

        if (isHidden)
        {
            transform.position = Vector3.Lerp(transform.position, hidingPosition, smoothHiding * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, defaultPosition.position, smoothHiding * Time.deltaTime);
        }
    }

    void UpdateUI(string text)
    {
        RemoveAllInventoryItems();

        List<InventoryItem> items = Inventory.instance.GetItemsList();
        foreach (InventoryItem item in items)
        {
            GameObject itemUI = Instantiate(InventoryItemUIPrefab, InventoryContent.transform);
            InventoryItemUI inventoryItemUI = itemUI.GetComponent<InventoryItemUI>();
            if (inventoryItemUI != null)
            {
                inventoryItemUI.SetInfo(item.Icon, item.Name);
            }
        }

        if (isHidden)
        {
            StartCoroutine(ShowNewItems());
        }
    }

    IEnumerator ShowNewItems()
    {
        isHidden = false;
        yield return new WaitForSeconds(showingNewItemsTime);
        isHidden = true;
    }



    void RemoveAllInventoryItems()
    {
        foreach (Transform t in InventoryContent.transform)
        {
            Destroy(t.gameObject);
        }
    }
}
