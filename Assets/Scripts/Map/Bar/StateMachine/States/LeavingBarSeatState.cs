using UnityEngine;

public class LeavingBarSeatState : BarSeatState
{
    public LeavingBarSeatState(
        BarSeatController controller)
        : base(controller)
    { }

    private float timer;

    public override DialogueType Dialogue =>
        DialogueType.Leaving;

    public override ThoughtType Thought => 
        ThoughtType.Leaving;

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

        controller.FinishSession();
    }
}