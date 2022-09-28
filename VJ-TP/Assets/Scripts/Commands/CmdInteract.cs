using UnityEngine;

public class CmdInteract : ICommand
{
    private IInteractable _interactable;

    public CmdInteract(IInteractable interactable) {
        _interactable = interactable;
    }

    public void Execute() => _interactable.Interact();

}
