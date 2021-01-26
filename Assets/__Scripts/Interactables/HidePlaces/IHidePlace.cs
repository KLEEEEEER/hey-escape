using HeyEscape.Core.Player.FSM;
using HeyEscape.Interactables.HidePlaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHidePlace
{
    void OnHide(PlayerFSM player);
    void OnUnhide(PlayerFSM player);
    bool IsAccessible();
    HidePlaceInfoSO GetHidePlaceInfo();
}
