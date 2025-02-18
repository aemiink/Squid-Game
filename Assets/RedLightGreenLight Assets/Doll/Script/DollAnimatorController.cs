using UnityEngine;

public class DollAnimatorController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartSinging()
    {
        animator.SetBool("isSinging", true); // Þarký söylerken animasyonu baþlat
    }

    public void StopSinging()
    {
        animator.SetBool("isSinging", false); // Þarkýyý bitirip animasyonu durdur
    }

    public void TurnHead()
    {
        animator.SetBool("isTurningHead", true); // Kafayý çevirme animasyonu
    }

    public void StopTurningHead()
    {
        animator.SetBool("isTurningHead", false); // Kafa çevirme animasyonunu durdur
    }

    public void StartScanning()
    {
        animator.SetBool("isScanning", true); // Tarama animasyonu baþlat
    }

    public void StopScanning()
    {
        animator.SetBool("isScanning", false); // Tarama animasyonunu durdur
    }

    public void TriggerDeath()
    {
        animator.SetBool("isDead", true); // Ölüm animasyonu baþlat
    }
}
