using UnityEngine;

public class DollController : MonoBehaviour
{
    private DollState currentState;
    public GameObject bloodEffectPrefab;
    public GameObject gameOverText;
    public PlayerMovement playerMovement;
    private DollAnimatorController dollAnimatorController; // DollAnimatorController referansý
    public AudioSource singingAudio; // Þarký müziði için AudioSource

    public bool IsSinging { get; private set; }

    public void Start()
    {
        currentState = new SingingState(this);
        currentState.EnterState();
        dollAnimatorController = GetComponent<DollAnimatorController>(); // DollAnimatorController'ý alýyoruz
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
        // Þarkýyý baþlat
        IsSinging = true;
        singingAudio.Play(); // Þarkýyý baþlat
        dollAnimatorController.StartSinging(); // Þarký söylerken animasyonu baþlat
    }

    public void TurnHeadAwayFromPlayers()
    {
        dollAnimatorController.TurnHead(); // Kafayý oyunculardan çevirme animasyonunu baþlat
    }

    public void TurnHeadToPlayers()
    {
        dollAnimatorController.StopTurningHead(); // Kafayý oyunculara çevirmek için animasyon durduruluyor
    }

    public void StartScanning()
    {
        dollAnimatorController.StartScanning(); // Tarama animasyonunu baþlat
    }

    public bool PlayerMoved()
    {
        // Kýrmýzý ýþýkta oyuncu hareket ediyor mu kontrol et
        return false; // Bunu oyun mantýðýnýza göre güncelleyin
    }

    public void TriggerPlayerDeath()
    {
        // Ölüm animasyonunu baþlat ve kan efektini ekle
        Debug.Log("Player detected and shot!");

        // Kan efektini tetikle
        Instantiate(bloodEffectPrefab, transform.position, Quaternion.identity);

        // Ölüm animasyonunu tetikle
        dollAnimatorController.TriggerDeath();

        // Game Over ekranýný göster
        TriggerGameOver();
    }

    private void TriggerGameOver()
    {
        gameOverText.SetActive(true); // Game Over mesajýný aktif et
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
