using HeyEscape.Core.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HeyEscape.UI
{
    public class InfoUI : MonoBehaviour
    {
        [SerializeField] Font textFont;
        [SerializeField] float newTextAppearingTime = 4f;
        [SerializeField] float fadeAwayTime = 4f;
        [SerializeField] GameObject InfoTextPrefab;
        private float parentWidth;

        bool deletingMessagesRunning = true;

        Coroutine deletingCoroutine;


        private WaitForSeconds delayNewTextAppearing;
        private WaitForSeconds delayfadeAway;

        private void Start()
        {
            RectTransform rect = transform.GetComponent<RectTransform>();
            parentWidth = rect.rect.width;
            deletingCoroutine = StartCoroutine(DeleteLastMessage());

            delayNewTextAppearing = new WaitForSeconds(newTextAppearingTime);
            delayfadeAway = new WaitForSeconds(fadeAwayTime);
        }

        private void OnEnable()
        {
            Inventory.instance.OnInventoryChanged += AppearText;
            Inventory.instance.OnInventoryUIText += AppearText;
        }

        private void OnDisable()
        {
            if (Inventory.instance != null)
            {
                Inventory.instance.OnInventoryChanged -= AppearText;
                Inventory.instance.OnInventoryUIText -= AppearText;
            }
        }

        IEnumerator DeleteLastMessage()
        {
            if (transform.childCount <= 0) yield return 0;

            deletingMessagesRunning = true;
            yield return delayNewTextAppearing;
            while (transform.childCount > 0)
            {
                Transform child = transform.GetChild(0);
                Destroy(child.gameObject);
                yield return delayfadeAway;
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
    }
}