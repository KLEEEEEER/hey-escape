using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FXVolumeSliderDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private AudioSource audio;
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private bool isRunning = false;
    [SerializeField] private float timeBetweenSounds = 0.5f;

    private WaitForSeconds delay;

    private void Start()
    {
        delay = new WaitForSeconds(timeBetweenSounds);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isRunning = true;
        StartCoroutine(soundsCoroutine());
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isRunning = false;
        StopCoroutine(soundsCoroutine());
    }

    private IEnumerator soundsCoroutine()
    {
        while (isRunning)
        {
            foreach (AudioClip clip in clips)
            {
                if (!isRunning) break;

                audio.PlayOneShot(clip);
                yield return delay;
            }
        }
    }
}
