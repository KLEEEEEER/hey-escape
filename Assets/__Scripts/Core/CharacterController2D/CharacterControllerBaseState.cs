using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CharacterControllerBaseState
{
    public static UnityEvent OnUseButtonPressed = new UnityEvent();
    public static UnityEvent OnJumpButtonPressed = new UnityEvent();
    public abstract void EnterState(CharacterController2D player);
    public abstract void Update(CharacterController2D player);
    public abstract void OnTriggerEnter2D(CharacterController2D player, Collider2D collision);
    public abstract void OnTriggerExit2D(CharacterController2D player, Collider2D collision);
    public abstract void FixedUpdate(CharacterController2D player);
    public abstract void LateUpdate(CharacterController2D player);
}
