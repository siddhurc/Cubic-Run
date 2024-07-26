using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneManager : MonoBehaviour
{

    public PlayableDirector playableDirector;
    public MainMenuManager mainMenuManager;

    // Start is called before the first frame update
    void Start()
    {
        playableDirector.stopped += OnCutsceneFinished;
    }

    private void OnDestroy()
    {
        playableDirector.stopped -= OnCutsceneFinished;
    }

    public void PlayCutscene()
    {
        playableDirector.Play();
    }

    private void OnCutsceneFinished(PlayableDirector director)
    {
        if (director == playableDirector)
        {
            //mainMenuManager.StartCoroutine(mainMenuManager.LoadAssetBundleAndSceneAdditively());
            mainMenuManager.ActivateNewSceneCamera();
        }
    }
}
