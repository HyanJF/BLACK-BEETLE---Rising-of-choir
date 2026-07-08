using UnityEngine;

public class OrderingBarSeatState : BarSeatState
{
    public OrderingBarSeatState(
        BarSeatController controller)
        : base(controller)
    { }

    public override DialogueType Dialogue =>
        DialogueType.Ordering;

    public override ThoughtType Thought => 
        ThoughtType.Ordering;

    public override string InteractionText => 
        "Atender";

    public override bool CanPlayerInteract =>
        true;

    public override void OnPlayerInteract(PlayerManager player)
    {
        controller.DisableInteraction();

        controller.ServeDrink(player);
    }
}
