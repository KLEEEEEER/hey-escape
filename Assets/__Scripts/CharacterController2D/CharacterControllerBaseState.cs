using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterControllerBaseState
{
    public abstract void EnterState(CharacterController2D player);
    public abstract void Update(CharacterController2D player);
    public abstract void OnCollisionEnter(CharacterController2D player);
    public abstract void FixedUpdate(CharacterController2D player);
    public abstract void LateUpdate(CharacterController2D player);
}
