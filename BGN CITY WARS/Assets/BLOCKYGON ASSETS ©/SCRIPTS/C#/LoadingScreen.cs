using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    public Slider loadingSlider;
    public Image loadingImage;

    // The scene to load (you can set this in the Unity editor)
    public string sceneToLoad;

   public void LoadSceneAsync()
    {
        StartCoroutine(LoadSceneAsyncCoroutine());
    }

    IEnumerator LoadSceneAsyncCoroutine()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f); // 0.9 is the completion value

            // Update the loading slider
            if (loadingSlider != null)
            {
                loadingSlider.value = progress;
            }

            // Update the loading image fill amount
            if (loadingImage != null)
            {
                loadingImage.fillAmount = progress;
            }

            yield return null;
        }
    }
}
