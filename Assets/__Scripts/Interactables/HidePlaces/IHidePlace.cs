using HeyEscape.Core.Player.FSM;
using HeyEscape.Interactables.HidePlaces;

namespace HeyEscape.Interactables.HidePlaces
{
    public interface IHidePlace
    {
        void OnHide(PlayerFSM player);
        void OnUnhide(PlayerFSM player);
        bool IsAccessible();
        HidePlaceInfoSO GetHidePlaceInfo();
    }
}