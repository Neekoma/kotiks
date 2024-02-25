using UnityEngine;

namespace Vald
{
    public class SecondPlayerInputHandler : PlayerInputHandler
    {
        public override void HandleInput()
        {
            moveDirection = Input.GetAxis("Player2");
            ladderDirection = Input.GetAxisRaw("Player2_Ladder");
            jump = Input.GetKeyDown(KeyCode.RightControl);
        }
    }
}