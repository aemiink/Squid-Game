using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Variable of Objects")]
    public GameObject firstPersonCam;
    public GameObject thirdPersonCam;

    [Header("Variable of DataType")]
    public float transitionSpeed = 5f; 
    private bool isFirstPerson;

    private void Start()
    {
        isFirstPerson = false;
    }

    void Update()
    {
        SwitchCamera();
    }

    void SwitchCamera()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (isFirstPerson) 
            {
                firstPersonCam.SetActive(false);
                thirdPersonCam.SetActive(true);
                isFirstPerson = false;
            }
            else
            {
                firstPersonCam.SetActive(true);
                thirdPersonCam.SetActive(false);
                isFirstPerson = true;
            }
   
        }
    }
}
