public abstract class PlayerInputHandler
{
    public float moveDirection { get; protected set; }
    public bool jump { get; protected set; }

    public abstract void HandleInput();
}