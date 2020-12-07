using HeyEscape.Core.Player.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ExitDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite openedDoor;
    [SerializeField] private bool isClosed = false;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip doorOpen;
    [SerializeField] private AudioClip doorClosed;

    [SerializeField] Light2D light;
    IEnumerator coroutine;
    [SerializeField] float lightChangeSpeed = 0.2f;
    private void Awake()
    {
        coroutine = animateIntensity();
    }

    private void OnEnable()
    {
        StartCoroutine(coroutine);
    }

    private void OnDisable()
    {
        StopCoroutine(coroutine);
    }

    public void Interact(PlayerFSM player)
    {
        if (isClosed && Inventory.instance.HasItem(typeof(ExitKey)))
        {
            Inventory.instance.UseItem(typeof(ExitKey));
            spriteRenderer.sprite = openedDoor;
            isClosed = false;
            audioSource.clip = doorOpen;
            audioSource.Play();
            StopCoroutine(coroutine);
            light.intensity = 0f;
            return;
        }
        else
        {
            audioSource.clip = doorClosed;
            audioSource.Play();
        }

        if (!isClosed)
        {
            LevelLoader.instance.LoadNextLevel();
        }
    }

    IEnumerator animateIntensity()
    {
        //if (light.intensity < 0f) yield break;
        float multiply = 1f;
        while (isClosed)
        {
            light.intensity -= Time.deltaTime * multiply * lightChangeSpeed;
            if (light.intensity < 0f) 
            {
                light.intensity = 0f;
                multiply *= -1f;
            }
            else if (light.intensity >= 0.2f)
            {
                light.intensity = 0.2f;
                multiply *= -1f;
            }
            yield return null;
        }
    }
}
