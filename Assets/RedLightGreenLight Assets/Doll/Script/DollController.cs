using UnityEngine;

public class DollController : MonoBehaviour
{
    private DollState currentState;
    public GameObject bloodEffectPrefab;
    public GameObject gameOverText;
    public PlayerMovement playerMovement;
    private DollAnimatorController dollAnimatorController; // DollAnimatorController referans�
    public AudioSource singingAudio; // �ark� m�zi�i i�in AudioSource

    public bool IsSinging { get; private set; }

    public void Start()
    {
        currentState = new SingingState(this);
        currentState.EnterState();
        dollAnimatorController = GetComponent<DollAnimatorController>(); // DollAnimatorController'� al�yoruz
    }

    void Update()
    {
        currentState.UpdateState();
    }

    public void SwitchToScanningState()
    {
        currentState = new ScanningState(this);
        currentState.EnterState();
    }

    public void StartSinging()
    {
        // �ark�y� ba�lat
        IsSinging = true;
        singingAudio.Play(); // �ark�y� ba�lat
        dollAnimatorController.StartSinging(); // �ark� s�ylerken animasyonu ba�lat
    }

    public void TurnHeadAwayFromPlayers()
    {
        dollAnimatorController.TurnHead(); // Kafay� oyunculardan �evirme animasyonunu ba�lat
    }

    public void TurnHeadToPlayers()
    {
        dollAnimatorController.StopTurningHead(); // Kafay� oyunculara �evirmek i�in animasyon durduruluyor
    }

    public void StartScanning()
    {
        dollAnimatorController.StartScanning(); // Tarama animasyonunu ba�lat
    }

    public bool PlayerMoved()
    {
        // K�rm�z� ���kta oyuncu hareket ediyor mu kontrol et
        return false; // Bunu oyun mant���n�za g�re g�ncelleyin
    }

    public void TriggerPlayerDeath()
    {
        // �l�m animasyonunu ba�lat ve kan efektini ekle
        Debug.Log("Player detected and shot!");

        // Kan efektini tetikle
        Instantiate(bloodEffectPrefab, transform.position, Quaternion.identity);

        // �l�m animasyonunu tetikle
        dollAnimatorController.TriggerDeath();

        // Game Over ekran�n� g�ster
        TriggerGameOver();
    }

    private void TriggerGameOver()
    {
        gameOverText.SetActive(true); // Game Over mesaj�n� aktif et
        Time.timeScale = 0; // Oyun durduruluyor
    }

    public void CheckForPlayerMovement()
    {
        if (!playerMovement.CanMove() && Input.GetAxis("Vertical") != 0)
        {
            TriggerPlayerDeath();
        }
    }
}
