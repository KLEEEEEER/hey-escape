using UnityEngine;

namespace HeyEscape.Core.Player.FSM.States
{
    public class PlayerFSMBaseState
    {
        protected PlayerFSM fsm;

        public PlayerFSMBaseState(PlayerFSM playerFSM) {
            fsm = playerFSM;
        }
        public virtual void EnterState() { }
        public virtual void ExitState() { }
        public virtual void Update() { }
        public virtual void OnTriggerEnter2D(Collider2D collision) { }
        public virtual void OnTriggerExit2D(Collider2D collision) { }
        public virtual void FixedUpdate() { }
        public virtual void LateUpdate() { }
    }
}