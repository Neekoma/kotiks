using UnityEngine;

public class SecondPlayerInputHandler : PlayerInputHandler
{
    public override float HandleInput()
    {
        return Input.GetAxisRaw("Player2");
    }
}