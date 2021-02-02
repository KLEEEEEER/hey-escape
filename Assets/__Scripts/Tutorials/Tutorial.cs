using System.Collections.Generic;
using UnityEngine;

namespace HeyEscape.Tutorials
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] protected List<GameObject> tutorialCanvases;
        private int currentActiveCanvasIndex = 0;
        
        public void OnChildTriggerEnter2D(Collider2D collision)
        {
            tutorialCanvases[currentActiveCanvasIndex].SetActive(false);
            currentActiveCanvasIndex++;
            if (tutorialCanvases[currentActiveCanvasIndex] != null)
                tutorialCanvases[currentActiveCanvasIndex].SetActive(true);
        }
    }
}
