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
        animator.SetBool("isSinging", true); // �ark� s�ylerken animasyonu ba�lat
    }

    public void StopSinging()
    {
        animator.SetBool("isSinging", false); // �ark�y� bitirip animasyonu durdur
    }

    public void TurnHead()
    {
        animator.SetBool("isTurningHead", true); // Kafay� �evirme animasyonu
    }

    public void StopTurningHead()
    {
        animator.SetBool("isTurningHead", false); // Kafa �evirme animasyonunu durdur
    }

    public void StartScanning()
    {
        animator.SetBool("isScanning", true); // Tarama animasyonu ba�lat
    }

    public void StopScanning()
    {
        animator.SetBool("isScanning", false); // Tarama animasyonunu durdur
    }

    public void TriggerDeath()
    {
        animator.SetBool("isDead", true); // �l�m animasyonu ba�lat
    }
}
