using UnityEngine;

namespace HeyEscape.Core.Enemy
{
    public class EnemyHeySound : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip[] heySounds;

        public void PlayHeySound()
        {
            int pickedHeyId = Random.Range(0, heySounds.Length);
            AudioClip pickedHey = heySounds[pickedHeyId];
            audioSource.clip = pickedHey;
            audioSource.Play();
        }
    }
}