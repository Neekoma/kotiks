using UnityEngine;


namespace Vald
{
    public class FirstPlayerInputHandler : PlayerInputHandler
    {
        public override void HandleInput()
        {
            moveDirection = Input.GetAxis("Player1");
            ladderDirection = Input.GetAxisRaw("Player1_Ladder");
            jump = Input.GetKeyDown(KeyCode.Space);
        }
    }
}