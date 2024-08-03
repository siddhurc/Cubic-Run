using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public GameObject mainMenu;
    public string assetBundleName = "cubicrun_assetbundle_45";
    public string newGameSceneName = "NewGameScene";
    public TMP_Text loadingText;
    public TMP_Text debugLog;
    public CutSceneManager cutsceneManager; // Ensure this reference is set in the Unity editor

    private AssetBundle assetBundle;

    public Slider loadingSlider;

    public void StartNewGame()
    {
        mainMenu.SetActive(false);
        StartCoroutine(LoadAssetBundleAndSceneAdditively());
        cutsceneManager.PlayCutscene();
    }

    public void Options()
    {
        // Your options logic here
    }

    public void About()
    {
        // Your about logic here
    }

    public void ExitGame()
    {
        Debug.Log("Exiting Game!!!");
        Application.Quit();
    }

    public IEnumerator LoadAssetBundleAndSceneAdditively()
    {
        string assetBundlePath = System.IO.Path.Combine(Application.streamingAssetsPath, assetBundleName);

        if (string.IsNullOrEmpty(assetBundlePath))
        {
            debugLog.text = "empty asset bundle path..";
            Debug.LogError("empty asset bundle path..");
            yield break;
        }

        AssetBundleCreateRequest bundleCreateRequest = AssetBundle.LoadFromFileAsync(assetBundlePath);

        if (bundleCreateRequest == null)
        {
            debugLog.text = "error in creating bundle request..";
            Debug.LogError("error in creating bundle request..");
            yield break;
        }

        while (!bundleCreateRequest.isDone)
        {
            float progressValue = Mathf.Clamp01(bundleCreateRequest.progress / 0.9f);
            loadingText.text = "Loading Asset Bundle ... " + Mathf.Round(progressValue * 100f) + "%";
            loadingSlider.value = progressValue;
            yield return null;
        }

        assetBundle = bundleCreateRequest.assetBundle;

        if (assetBundle == null)
        {
            debugLog.text = "asset bundle not loaded properly..";
            Debug.LogError("asset bundle not loaded properly..");
            yield break;
        }

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(newGameSceneName, LoadSceneMode.Additive);

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingText.text = "Loading Game Scene ... " + Mathf.Round(progressValue * 100f) + "%";
            loadingSlider.value = progressValue;
            yield return null;
        }

        // Optionally unload the asset bundle if no longer needed
        assetBundle.Unload(false);

        // Cleanup
        assetBundle = null;

        // Activate the new scene's camera and deactivate the main menu camera
        ActivateNewSceneCamera();

        // Destroy the MainMenuManager game object
        Destroy(gameObject);
    }

    public void ActivateNewSceneCamera()
    {
        Camera[] cameras = Camera.allCameras;
        foreach (var cam in cameras)
        {
            if (cam.gameObject.scene.name == newGameSceneName)
            {
                cam.gameObject.SetActive(true);
            }
            else
            {
                cam.gameObject.SetActive(false);
            }
        }
    }

    void OnDestroy()
    {
        if (assetBundle != null)
        {
            assetBundle.Unload(true); // Unload the asset bundle when no longer needed
        }
    }
}
