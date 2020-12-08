using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelFader : MonoBehaviour
{
    [SerializeField] private Image img;
    [SerializeField] private AnimationCurve Curve;
    [SerializeField] private float animationTime = 0.5f;

    private static LevelFader s_Instance = null;
    public static LevelFader instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(LevelFader)) as LevelFader;
            }

            if (s_Instance == null)
            {
                var obj = new GameObject("LevelFader");
                s_Instance = obj.AddComponent<LevelFader>();
            }

            return s_Instance;
        }
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator FadeInCoroutine()
    {
        float t = animationTime;

        while (t > 0f)
        {
            t -= Time.deltaTime;

            float a = Curve.Evaluate(t);

            Color tempColor = img.color;
            tempColor.a = a;
            img.color = tempColor;

            yield return 0;
        }
    }

    IEnumerator FadeOutCoroutine()
    {
        float t = 0f;

        while (t < animationTime)
        {
            t += Time.deltaTime;

            float a = Curve.Evaluate(t);

            Color tempColor = img.color;
            tempColor.a = a;
            img.color = tempColor;

            yield return 0;
        }
    }
}
