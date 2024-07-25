using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private AssetBundle assetBundle;

    public Slider loadingSlider;

    public void StartNewGame()
    {
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);
        StartCoroutine(LoadAssetBundleAndScene());
    }

    public void LoadGame()
    {

    }

    public void Options()
    {

    }

    public void About()
    {

    }

    public void ExitGame()
    {
        Debug.Log("Exiting Game!!!");
        Application.Quit();
    }

    IEnumerator LoadAssetBundleAndScene()
    {
        string assetBundlePath = System.IO.Path.Combine(Application.streamingAssetsPath, assetBundleName);

        if (assetBundlePath == null)
        {
            debugLog.text = "empty asset bundle path..";
            Debug.LogError("empty asset bundle path..");
        }
            

        AssetBundleCreateRequest bundleCreateRequest = AssetBundle.LoadFromFileAsync(assetBundlePath);

        if (bundleCreateRequest == null)
        {
            debugLog.text = "error in creating bundle request..";
            Debug.LogError("error in creating bundle request..");
        }
            

        while (!bundleCreateRequest.isDone)
        {
            float progressValue = Mathf.Clamp01(bundleCreateRequest.progress / 0.9f);
            loadingText.text = "Loading Asset Bundle ... "+ Mathf.Round(progressValue * 100f) + "%";
            loadingSlider.value = progressValue;
            yield return null;
        }

        assetBundle = bundleCreateRequest.assetBundle;

        if (assetBundle == null)
        {
            debugLog.text = "asset bundle not loaded properly..";
            Debug.LogError("asset bundle not loaded properly..");
        }
           

        //string scenePathInAssetBundle = assetBundle.GetAllScenePaths().FirstOrDefault(s => s.Contains(newGameSceneName));

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(newGameSceneName);

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingText.text = "Loading Game Scene ... " + Mathf.Round(progressValue * 100f) + "%";
            loadingSlider.value = progressValue;
            yield return null;
        }

        // Unload asset bundle
        assetBundle.Unload(false);

        // Cleanup
        assetBundle = null;

        //destroy the mainmenumanager game object
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        if (assetBundle != null)
        {
            assetBundle.Unload(true); // Unload the asset bundle when no longer needed
        }
    }

}
