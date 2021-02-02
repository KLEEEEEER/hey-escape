using UnityEngine;
using UnityEngine.UI;

namespace HeyEscape.Core.Debug
{
    public class ShowFPS : MonoBehaviour
    {
        public int count;
        public int samples = 100;
        public float totalTime;
        public Text fpsText;

        public void Start()
        {
            count = samples;
            totalTime = 0f;
        }

        public void Update()
        {
            count -= 1;
            totalTime += Time.deltaTime;

            if (count <= 0)
            {
                float fps = samples / totalTime;
                displayFPS(fps);
                totalTime = 0f;
                count = samples;
            }
        }

        private void displayFPS(float value)
        {
            fpsText.text = value.ToString();
        }
    }
}