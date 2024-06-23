using UnityEngine;

public class DynamicAspectRatioHandler : MonoBehaviour
{
    public Camera mainCamera; // Main camera to adjust
    public Vector2 targetResolution = new Vector2(1080, 1920); // Reference resolution for 16:9
    private float targetAspect = 9.0f / 16.0f; // 16:9 aspect ratio

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Use the main camera if not assigned
        }
        AdjustCameraViewport();
    }

    void AdjustCameraViewport()
    {
        float windowAspect = (float)Screen.width / Screen.height; // Current window aspect ratio
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f) // Screen is taller
        {
            // Adjust viewport height and center it vertically
            Rect rect = mainCamera.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            mainCamera.rect = rect;
        }
        else // Screen is wider
        {
            // Adjust viewport width and center it horizontally
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = mainCamera.rect;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
            mainCamera.rect = rect;
        }
    }

    void OnPreCull()
    {
        // Ensure the background remains black during adjustments
        GL.Clear(true, true, Color.black);
    }
}
