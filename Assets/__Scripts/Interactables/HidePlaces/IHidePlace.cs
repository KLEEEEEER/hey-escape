using HeyEscape.Interactables.HidePlaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHidePlace
{
    void OnHide();
    void OnUnhide();
    bool IsAccessible();
    HidePlaceInfoSO GetHidePlaceInfo();
}
