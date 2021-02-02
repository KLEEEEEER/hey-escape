using HeyEscape.Core.Loaders;
using UnityEngine;

namespace HeyEscape.UI
{
    public class FloorsPassedText : MonoBehaviour
    {
        public int current_floor_number
        {
            get
            {
                if (LevelLoader.instance != null)
                    return LevelLoader.instance.GetCurrentLevelNumber();
                else
                    return 0;
            }
        }
        public int floors_number
        {
            get
            {
                if (LevelLoader.instance != null)
                    return LevelLoader.instance.GetLevelsCount();
                else
                    return 0;
            }
        }
    }
}