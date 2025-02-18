using UnityEngine;
public class WalkState : INPCLobbyState
{
    public void Enter(NPCLobbyBehavior npc)
    {
        npc.animator.SetTrigger("WalkTrigger");  // Yürüyüş animasyonunu başlat
        npc.SetRandomDestination(); // Hareket etmeye başla
    }

    public void Update(NPCLobbyBehavior npc)
    {
        if (npc.HasReachedDestination())  // Hedefe ulaştıysa
        {
            npc.ChangeState(new IdleState());  // Idle durumuna geç
        }
    }

    public void Exit(NPCLobbyBehavior npc)
    {
        // Durum değiştirilince yapılacak işlemler
    }
}
