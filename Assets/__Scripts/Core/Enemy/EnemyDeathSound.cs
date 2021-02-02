using UnityEngine;

namespace HeyEscape.Core.Enemy
{
    public class EnemyDeathSound : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip[] deathSounds;

        public void PlayDeathSound()
        {
            int pickedDeathId = Random.Range(0, deathSounds.Length);
            AudioClip pickedDeath = deathSounds[pickedDeathId];
            audioSource.PlayOneShot(pickedDeath);
        }
    }
}