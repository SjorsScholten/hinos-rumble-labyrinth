namespace hinos.interaction
{
    public interface IInteractionHandler
    {
        bool IsInteractable { get; }
        void HandleInteraction(object source);
    }
}

