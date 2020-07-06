using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MusicVolumeSliderDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private AudioSource audio;

    public void OnBeginDrag(PointerEventData eventData)
    {
        audio.Play();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        audio.Stop();
    }
}
