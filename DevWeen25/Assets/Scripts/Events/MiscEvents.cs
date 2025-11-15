using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;


public class MiscEvents
{
    public void NextChapter()
    {
        UIAnimations uiAnimations = UnityEngine.Object.FindAnyObjectByType<UIAnimations>();
        
        

        if (uiAnimations != null)
        {
            uiAnimations.PlayFade();
        }
        else
        {
            Debug.LogWarning("UIAnimations component not found in scene!");
        }

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Verifica se a próxima cena existe
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log($"Loading scene at index {nextSceneIndex}");
            MonoBehaviour coroutineHost = uiAnimations as MonoBehaviour;
            if (coroutineHost == null && GameEventsManager.instance != null)
                coroutineHost = GameEventsManager.instance;

            if (coroutineHost != null)
            {
                coroutineHost.StartCoroutine(WaitAndLoadNextScene(1.0f, nextSceneIndex));
            }
            else
            {
                Debug.LogWarning("No MonoBehaviour available to start coroutine — loading scene immediately.");
                SceneManager.LoadScene(nextSceneIndex);
            }
        }
        else
        {
            Debug.LogWarning($"No scene at index {nextSceneIndex}. Total scenes in build: {SceneManager.sceneCountInBuildSettings}");
        }
    }

    private IEnumerator WaitAndLoadNextScene(float waitTime, int nextSceneIndex)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(nextSceneIndex);
    }
}