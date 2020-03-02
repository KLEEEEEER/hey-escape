using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoUI : MonoBehaviour
{
    [SerializeField] Font textFont;
    [SerializeField] float newTextAppearingTime = 4f;
    [SerializeField] float fadeAwayTime = 4f;
    [SerializeField] GameObject InfoTextPrefab;
    private float parentWidth;

    bool deletingMessagesRunning = true;

    Coroutine deletingCoroutine;

    private void Start()
    {
        RectTransform rect = transform.GetComponent<RectTransform>();
        parentWidth = rect.rect.width;
        deletingCoroutine = StartCoroutine(DeleteLastMessage());
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

    IEnumerator DeleteLastMessage()
    {
        if (transform.childCount <= 0) yield return 0;

        deletingMessagesRunning = true;
        yield return new WaitForSeconds(newTextAppearingTime);
        while (transform.childCount > 0) 
        {
            Transform child = transform.GetChild(0);
            Destroy(child.gameObject);
            yield return new WaitForSeconds(fadeAwayTime);
        }
        deletingMessagesRunning = false;
    }

    public void AppearText(string text)
    {
        GameObject newText = Instantiate(InfoTextPrefab, transform);
        Text textComponent = newText.GetComponent<Text>();
        textComponent.text = text;

        if (!deletingMessagesRunning) 
        {
            deletingCoroutine = StartCoroutine(DeleteLastMessage());
        }
        else
        {
            StopCoroutine(deletingCoroutine);
            deletingCoroutine = StartCoroutine(DeleteLastMessage());
        }
    }

    public void OnGameOver()
    {
        gameObject.SetActive(false);
    }

    /*IEnumerator ShowTextLine(string text)
    {
        GameObject newText = Instantiate(InfoTextPrefab, transform);
        Text textComponent = newText.GetComponent<Text>();
        textComponent.text = text;
        yield return new WaitForSeconds(appearingTime);
        Destroy(newText);
    }*/
}
