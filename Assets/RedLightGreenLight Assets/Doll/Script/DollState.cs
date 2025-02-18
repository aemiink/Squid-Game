public abstract class DollState
{
    protected DollController dollController;

    public DollState(DollController controller)
    {
        dollController = controller;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
}