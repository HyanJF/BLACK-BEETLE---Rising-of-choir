public class BarSeatStateMachine
{
    public BarSeatState CurrentState { get; private set; }

    public void ChangeState(BarSeatState newState)
    {
        CurrentState?.Exit();

        CurrentState = newState;

        CurrentState?.Enter();
    }

    public void Update()
    {
        CurrentState?.Update();
    }
}

public abstract class BarSeatState
{
    protected BarSeatController controller;

    protected BarSeatState(
        BarSeatController controller)
    {
        this.controller = controller;
    }

    public virtual InteractionSoundType InteractionSound =>
    InteractionSoundType.None;

    public virtual DialogueType Dialogue =>
        DialogueType.None;

    public virtual ThoughtType Thought =>
        ThoughtType.None;

    public virtual DialogueColorType DialogueColor =>
    DialogueColorType.Normal;

    public virtual string InteractionText => 
        string.Empty;

    public virtual bool CanPlayerInteract =>
        true;

    public virtual void Enter() { }

    public virtual void Update() { }

    public virtual void Exit() { }

    public virtual void OnPlayerInteract(
        PlayerManager player)
    {
    }
}