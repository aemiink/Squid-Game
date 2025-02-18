using UnityEngine;
public class IdleState : INPCLobbyState
{
    public void Enter(NPCLobbyBehavior npc)
    {
        npc.animator.SetTrigger("IdleTrigger"); // Idle animasyonunu başlat
    }

    public void Update(NPCLobbyBehavior npc)
    {
        // NPC'nin idle durumunda belirli bir süre beklemesini sağlayabiliriz
        if (Time.time - npc.LastMoveTime > npc.IdleDuration)
        {
            npc.ChangeState(new WalkState()); // Idle süresi bittiğinde yürümeye geç
        }
    }

    public void Exit(NPCLobbyBehavior npc)
    {
        // Durum değiştirilince yapılacak işlemler
    }
}
