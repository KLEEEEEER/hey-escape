using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace HeyEscape.Core.Player
{
    [RequireComponent(typeof(Light2D))]
    public class PlayerLightVision : MonoBehaviour
    {
        [SerializeField] float fullVisionRadius = 4.9f;

        public enum VisionState
        {
            Full,
            InDoor,
            Disabled
        }

        Light2D light;
        VisionState currentVisionState = VisionState.Full;

        private void Start()
        {
            light = GetComponent<Light2D>();
            SetVisionState(currentVisionState);
        }

        public void SetVisionState(VisionState visionState)
        {
            switch(visionState)
            {
                case VisionState.Full:
                    light.pointLightOuterRadius = fullVisionRadius;
                    break;
                case VisionState.InDoor:
                    light.pointLightOuterRadius = 3f;
                    break;
                case VisionState.Disabled:
                    light.pointLightOuterRadius = 0f;
                    break;
            }
        }
    }
}