using UnityEngine;
using UnityEngine.AI;

public class NPCLobbyBehavior : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;
    public float IdleDuration = 3f;
    public float LastMoveTime = 0f;

    private INPCLobbyState currentState;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        ChangeState(new IdleState()); // Başlangıç durumu
    }

    private void Update()
    {
        currentState.Update(this); // Mevcut durumu güncelle

        if (!HasReachedDestination())
        {
            // Hedefe ulaşılmadığında WalkTrigger parametresini tetikle
            animator.SetTrigger("WalkTrigger");  // Doğru parametreyi kullanıyoruz
            agent.isStopped = false;  // Agent hareket etsin
            agent.speed = 3.5f;  // Yavaş hareket için hız ayarını yapın
        }
        else
        {
            // Hedefe ulaşıldığında IdleTrigger parametresini tetikle
            animator.SetTrigger("IdleTrigger"); // Doğru parametreyi kullanıyoruz
            agent.isStopped = true;  // Agent durduğunda hareketi durdur
            agent.speed = 0;  // Hız sıfırlansın, böylece hareket etmesin
        }
    }

    public void ChangeState(INPCLobbyState newState)
    {
        currentState?.Exit(this);
        currentState = newState;
        currentState.Enter(this);
    }

    public bool HasReachedDestination()
    {
        return Vector3.Distance(transform.position, agent.destination) < 1f;
    }

    public void SetRandomDestination()
    {
        float randomX = Random.Range(-10f, 10f);
        float randomZ = Random.Range(-10f, 10f);
        Vector3 targetPosition = new Vector3(randomX, transform.position.y, randomZ);
        agent.SetDestination(targetPosition);
        LastMoveTime = Time.time;
    }
}
