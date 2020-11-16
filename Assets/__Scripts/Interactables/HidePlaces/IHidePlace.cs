using HeyEscape.Interactables.HidePlaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHidePlace
{
    /*void Hide();
    void Unhide();*/
    bool IsAccessible();
    HidePlaceInfoSO GetHidePlaceInfo();
}
