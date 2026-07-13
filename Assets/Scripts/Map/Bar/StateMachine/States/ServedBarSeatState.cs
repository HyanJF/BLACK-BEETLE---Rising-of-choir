using UnityEngine;

public class ServedBarSeatState : BarSeatState
{
    private float timer;

    public ServedBarSeatState(
        BarSeatController controller)
        : base(controller)
    {
    }

    public override InteractionSoundType InteractionSound =>
        InteractionSoundType.Served;

    public override DialogueType Dialogue =>
        DialogueType.Served;

    public override ThoughtType Thought =>
        ThoughtType.Served;

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