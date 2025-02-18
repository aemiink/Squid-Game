public class ScanningState : DollState
{
    public ScanningState(DollController controller) : base(controller) { }

    public override void EnterState()
    {
        dollController.TurnHeadToPlayers();
        dollController.StartScanning();
    }

    public override void UpdateState()
    {
        if (dollController.PlayerMoved())
        {
            dollController.TriggerPlayerDeath();
        }
    }
}
