using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScalarHandler : MonoBehaviour
{
    public Canvas canvas; // Reference to the Canvas component
    public CanvasScaler canvasScaler; // Reference to the CanvasScaler component

    void Start()
    {
        if (canvas == null)
        {
            canvas = GetComponent<Canvas>();
        }

        if (canvasScaler == null)
        {
            canvasScaler = canvas.GetComponent<CanvasScaler>();
        }

        AdjustCanvasScaler();
    }

    void AdjustCanvasScaler()
    {
        // Set the UI Scale Mode to Scale With Screen Size
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

        // Set the reference resolution (can be the same as the target resolution of your game)
        canvasScaler.referenceResolution = new Vector2(1080, 1920); // Adjust as necessary

        // Set the screen match mode to match the width or height, whichever is smaller
        canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;

        // Match the width or height based on the screen's aspect ratio
        float windowAspect = (float)Screen.width / Screen.height;
        float targetAspect = 9.0f / 16.0f;

        canvasScaler.matchWidthOrHeight = (windowAspect > targetAspect) ? 1 : 0;
    }
}
