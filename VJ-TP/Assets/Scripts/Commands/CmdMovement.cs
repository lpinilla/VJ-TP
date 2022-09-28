using UnityEngine;

public class CmdMovement : ICommand
{
    // properties
    private IMoveable _moveable;
    private Vector3 _direction;

    public CmdMovement(IMoveable moveable, Vector3 direction) {
        _moveable = moveable;
        _direction = direction;
    }

    public void Execute() => _moveable.Travel(_direction);
}
