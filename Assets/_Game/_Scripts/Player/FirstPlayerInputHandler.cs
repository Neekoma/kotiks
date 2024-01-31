using UnityEngine;

public class FirstPlayerInputHandler : PlayerInputHandler
{
    public override void HandleInput()
    {
        moveDirection = Input.GetAxis("Player1");
        jump = Input.GetKeyDown(KeyCode.Space);
    }
}