using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Heygame
{

    public class Tutorial : MonoBehaviour
    {
        [SerializeField] protected List<GameObject> tutorialCanvases;
        private int currentActiveCanvasIndex = 0;

        private void Start()
        {
            if (tutorialCanvases.Count == 0) return;

            /*foreach (GameObject canvas in tutorialCanvases)
            {
                canvas.SetActive(false);
            }*/

            //tutorialCanvases[currentActiveCanvasIndex].SetActive(true);
        }

        public void OnChildTriggerEnter2D(Collider2D collision)
        {
            tutorialCanvases[currentActiveCanvasIndex].SetActive(false);
            currentActiveCanvasIndex++;
            Debug.Log("currentActiveCanvasIndex = " + currentActiveCanvasIndex);
            if (tutorialCanvases[currentActiveCanvasIndex] != null)
                tutorialCanvases[currentActiveCanvasIndex].SetActive(true);
        }
    }
}
