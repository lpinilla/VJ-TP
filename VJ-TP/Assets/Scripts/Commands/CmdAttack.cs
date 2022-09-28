using UnityEngine;

public class CmdAttack : ICommand
{
    private IBaseGun _gun;

    public CmdAttack(IBaseGun gun) {
        _gun = gun;
    }

    public void Execute() {
        _gun.Attack();
    }
}
