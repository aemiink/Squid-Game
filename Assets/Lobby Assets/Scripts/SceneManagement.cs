using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneManagement : MonoBehaviour
{
    public float delayTime = 5f;
    public int sceneIndex = 1;
    public GameObject[] uiImages; 
    public GameObject additionalUIElement; 
    private bool playerInTrigger = false; 

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !playerInTrigger)
        {
            playerInTrigger = true;
            Invoke("ChangeScene", delayTime); 
            StartCoroutine(ShowUIImages()); 
            additionalUIElement.SetActive(false); 
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }

    private IEnumerator ShowUIImages()
    {
        for (int i = 0; i < uiImages.Length; i++)
        {
            uiImages[i].SetActive(true);  
            yield return new WaitForSeconds(1f); 
            StartCoroutine(ShrinkAndHide(uiImages[i]));  
            yield return new WaitUntil(() => uiImages[i].transform.localScale == Vector3.zero); 
        }
    }


    private IEnumerator ShrinkAndHide(GameObject img)
    {
        Vector3 originalScale = img.transform.localScale;  
        Vector3 targetScale = Vector3.zero;  

        float shrinkDuration = 1f; 
        float elapsedTime = 0f;  


        while (elapsedTime < shrinkDuration)
        {
            img.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / shrinkDuration);  
            elapsedTime += Time.deltaTime;  
            yield return null;  
        }

        img.transform.localScale = targetScale;  
        img.SetActive(false); 
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false; 

            CancelInvoke("ChangeScene");

            foreach (var img in uiImages)
            {
                img.SetActive(false);  
                img.transform.localScale = Vector3.one;  
            }
            additionalUIElement.SetActive(true);
        }
    }
}
