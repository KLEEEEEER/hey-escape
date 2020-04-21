using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterControllerInWindowState : CharacterControllerBaseState
{
    public UnityEvent OnWindowExit = new UnityEvent();
    public override void EnterState(CharacterController2D player)
    {
        GameManager.instance.PlayerMovement.disableMovement = true;
    }

    public override void OnTriggerEnter2D(CharacterController2D player, Collider2D collision)
    {
        
    }

    public override void LateUpdate(CharacterController2D player)
    {

    }

    public override void FixedUpdate(CharacterController2D player)
    {
        
    }

    public override void Update(CharacterController2D player)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (GameManager.instance.PlayerRenderer != null) GameManager.instance.PlayerRenderer.enabled = true;
            GameManager.instance.CharacterController2D.TransitionToState(GameManager.instance.CharacterController2D.IdleState);
            GameManager.instance.PlayerMovement.disableMovement = false;
            GameManager.instance.PlayerComponent.UnhidePlayer();
            OnWindowExit.Invoke();
            OnWindowExit.RemoveAllListeners();
        }
    }
    public override void OnTriggerExit2D(CharacterController2D player, Collider2D collision)
    {
        
    }
}
