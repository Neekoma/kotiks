using UnityEngine;

public class FirstPlayerInputHandler : PlayerInputHandler
{
    public override float HandleInput()
    {
        return Input.GetAxisRaw("Player1");
    }
}