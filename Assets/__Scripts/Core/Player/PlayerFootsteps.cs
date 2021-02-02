using UnityEngine;

namespace HeyEscape.Core.Player
{
    public class PlayerFootsteps : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip[] footstepsSounds;

        public void PlayFootstepSound()
        {
            int pickedFootstepId = Random.Range(0, footstepsSounds.Length);
            AudioClip pickedFootstep = footstepsSounds[pickedFootstepId];
            audioSource.PlayOneShot(pickedFootstep);
        }
    }
}