public abstract class State
{
    protected BotController bot;

    public State(BotController bot)
    {
        this.bot = bot;
    }

    public virtual void Enter() { }

    public virtual void Update() { }

    public virtual void Exit() { }
}