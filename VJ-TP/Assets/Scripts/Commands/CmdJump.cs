using UnityEngine;

public class CmdJump : ICommand
{
    // properties
    private IMoveable _moveable;

    public CmdJump(IMoveable moveable) {
        _moveable = moveable;
    }

    public void Execute() => _moveable.Jump();
}
