public class SingingState : DollState
{
    public SingingState(DollController controller) : base(controller) { }

    public override void EnterState()
    {
        dollController.StartSinging();
        dollController.TurnHeadAwayFromPlayers();
    }

    public override void UpdateState()
    {
        if (!dollController.IsSinging)
        {
            dollController.SwitchToScanningState();
        }
    }
}
