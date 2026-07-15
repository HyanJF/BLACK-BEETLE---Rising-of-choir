using UnityEngine;

public class WrongDrinkBarSeatState : BarSeatState
{
    private float timer;

    public WrongDrinkBarSeatState(
        BarSeatController controller)
        : base(controller)
    { }

    public override InteractionSoundType InteractionSound =>
        InteractionSoundType.WrongDrink;

    public override DialogueType Dialogue => 
        DialogueType.WrongDrink;

    public override ThoughtType Thought => 
        ThoughtType.Angry;

    public override DialogueColorType DialogueColor =>
        DialogueColorType.Error;

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
