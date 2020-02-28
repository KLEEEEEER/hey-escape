using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IHidePlace
{
    void Hide(GameObject player);
    void Unhide(GameObject player);
    bool IsAccessible();
}
