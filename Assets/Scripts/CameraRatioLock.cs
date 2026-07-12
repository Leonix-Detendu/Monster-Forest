using UnityEngine;

public class CameraRatioLock : MonoBehaviour
{
    private void Start()
    {
        float targetAspect = 16f / 9f;

        float windowAspect = (float)Screen.width / Screen.height;

        float scaleHeight = windowAspect / targetAspect;

        Camera camera = GetComponent<Camera>();

        if (scaleHeight > 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f / scaleHeight;
            rect.height = 1.0f;
            rect.x = (1.0f - rect.width) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }
}
