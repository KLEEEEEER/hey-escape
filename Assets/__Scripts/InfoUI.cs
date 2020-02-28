using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoUI : MonoBehaviour
{
    [SerializeField] Font textFont;
    [SerializeField] float appearingTime = 4f;
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
        GameObject newText = new GameObject("InfoText");

        Text textComponent = newText.AddComponent<Text>();
        textComponent.text = text;
        textComponent.font = textFont;
        textComponent.alignment = TextAnchor.MiddleLeft;
        textComponent.color = Color.white;
        textComponent.fontSize = 40;

        newText.transform.localScale = new Vector3(1, 1, 1);
        newText.transform.SetParent(transform);

        RectTransform rect = transform.GetComponent<RectTransform>();
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, parentWidth);
        rect.localScale = new Vector3(1, 1, 1);

        //ContentSizeFitter sizeFitter = newText.AddComponent<ContentSizeFitter>();
        //sizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        //sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        yield return new WaitForSeconds(appearingTime);
        Destroy(newText);
    }
}
