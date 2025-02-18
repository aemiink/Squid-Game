using UnityEngine;

public class RandomWalkStrategy : INPCMovementStrategy
{
    public void Move(UnityEngine.AI.NavMeshAgent agent)
    {
        // Rastgele bir hedef seç ve hareket et
        float randomX = Random.Range(-10f, 10f);
        float randomZ = Random.Range(-10f, 10f);
        Vector3 targetPosition = new Vector3(randomX, agent.transform.position.y, randomZ);
        agent.SetDestination(targetPosition);
    }
}
