using UnityEngine;

public class SecondPlayerInputHandler : PlayerInputHandler
{
    public override void HandleInput()
    {
        moveDirection = Input.GetAxis("Player2");
        jump = Input.GetKeyDown(KeyCode.RightControl);
    }
}