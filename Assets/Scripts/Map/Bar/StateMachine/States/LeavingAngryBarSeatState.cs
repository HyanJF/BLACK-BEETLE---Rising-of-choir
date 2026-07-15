using UnityEngine;

public class LeavingAngryBarSeatState : BarSeatState
{
    public LeavingAngryBarSeatState(
        BarSeatController controller)
        : base( controller )
        { }

    private float timer;

    public override InteractionSoundType InteractionSound =>
        InteractionSoundType.LeavingAngry;
    public override DialogueType Dialogue => 
        DialogueType.LeavingAngry;
    public override ThoughtType Thought => 
        ThoughtType.Angry;
    public override DialogueColorType DialogueColor =>
     DialogueColorType.Error;

    public override string InteractionText => 
        string.Empty;

    public override void Enter()
    {
        timer = controller.GetDialogueDuration(Dialogue);
    }

    public override void Update()
    {
        timer -= Time.deltaTime;

        if ( timer > 0f)
            return;

        controller.FinishSession();
    }
}
