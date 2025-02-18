using UnityEngine;

public class TargetWalkStrategy : INPCMovementStrategy
{
    public void Move(UnityEngine.AI.NavMeshAgent agent)
    {
        // Sabit bir hedefe yönel
        Vector3 targetPosition = new Vector3(5f, agent.transform.position.y, 5f);
        agent.SetDestination(targetPosition);
    }
}

