using UnityEngine;

public class GreetingBarSeatState : BarSeatState
{
    public GreetingBarSeatState(
        BarSeatController controller)
        : base(controller)
    { }

    public override InteractionSoundType InteractionSound =>
    InteractionSoundType.Greeting;

    public override DialogueType Dialogue => 
        DialogueType.Greeting;

    public override ThoughtType Thought => 
        ThoughtType.Greeting;

    public override bool CanPlayerInteract => 
        true;

    public override string InteractionText => 
        "Hablar";

    public override void OnPlayerInteract(PlayerManager player)
    {
        controller.DisableInteraction();

        controller.ChangeState(
            new OrderingBarSeatState(controller));
    }
}