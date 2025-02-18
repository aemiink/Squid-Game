using UnityEngine;
using System.Collections;

public class NPCController : MonoBehaviour
{
    public DollController dollController;
    public PlayerMovement playerMovement;
    public float greenLightDuration = 5f;
    public float redLightDuration = 3f;
    private bool isGreenLight = false;

    void Start()
    {
        StartCoroutine(ControlLights());
    }

    private IEnumerator ControlLights()
    {
        while (true)
        {
            // Green Light Phase
            isGreenLight = true;
            playerMovement.EnableMovement(true);
            yield return new WaitForSeconds(greenLightDuration);

            // Red Light Phase
            isGreenLight = false;
            playerMovement.EnableMovement(false);
            dollController.CheckForPlayerMovement(); // Kýrmýzý ýþýkta oyuncuyu kontrol et
            yield return new WaitForSeconds(redLightDuration);
        }
    }

    public bool IsGreenLight()
    {
        return isGreenLight;
    }
}
