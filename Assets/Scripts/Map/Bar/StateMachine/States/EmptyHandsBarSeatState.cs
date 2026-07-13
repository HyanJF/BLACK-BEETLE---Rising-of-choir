using UnityEngine;

public class EmptyHandsBarSeatState : BarSeatState
{
    private float timer;

    public EmptyHandsBarSeatState(
        BarSeatController controller)
        : base(controller)
    { }

    public override InteractionSoundType InteractionSound =>
        InteractionSoundType.EmptyHands;

    public override DialogueType Dialogue => 
        DialogueType.EmptyHands;

    public override ThoughtType Thought => 
        ThoughtType.EmptyHands;

    public override string InteractionText => 
        string.Empty;

    public override bool CanPlayerInteract => 
        false;

    public override void Enter()
    {
        timer = controller.GetDialogueDuration(Dialogue);
    }

    public override void Update()
    {
        timer -= Time.deltaTime;

        if (timer > 0f)
            return;

        controller.ChangeState(
            new OrderingBarSeatState(controller));
    }
}
