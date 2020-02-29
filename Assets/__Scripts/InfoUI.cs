using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoUI : MonoBehaviour
{
    [SerializeField] Font textFont;
    [SerializeField] float appearingTime = 4f;
    [SerializeField] GameObject InfoTextPrefab;
    private float parentWidth;

    private void Start()
    {
        RectTransform rect = transform.GetComponent<RectTransform>();
        parentWidth = rect.rect.width;
    }

    private void OnEnable()
    {
        Inventory.instance.OnInventoryChanged += AppearText;
        Inventory.instance.OnInventoryUIText += AppearText;
        GameManager.instance.PlayerComponent.OnPlayerInteractEvent += AppearText;
    }

    private void OnDisable()
    {
        Inventory.instance.OnInventoryChanged -= AppearText;
        Inventory.instance.OnInventoryUIText -= AppearText;
        GameManager.instance.PlayerComponent.OnPlayerInteractEvent -= AppearText;
    }

    public void AppearText(string text)
    {
        StartCoroutine(ShowTextLine(text));
    }

    IEnumerator ShowTextLine(string text)
    {
        GameObject newText = Instantiate(InfoTextPrefab, transform);
        Text textComponent = newText.GetComponent<Text>();
        textComponent.text = text;
        yield return new WaitForSeconds(appearingTime);
        Destroy(newText);
    }
}
