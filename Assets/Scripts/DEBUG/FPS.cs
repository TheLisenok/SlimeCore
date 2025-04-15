using UnityEngine;
using System.Collections;

public class FPS : MonoBehaviour
{
    private float deltaTime = 0.0f;
    private float fps = 0.0f;
    private float updateInterval = 0.5f; // Задержка обновления FPS
    private float nextUpdateTime = 0.0f;

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        // Обновляем FPS только через определённый интервал времени
        if (Time.unscaledTime >= nextUpdateTime)
        {
            fps = 1.0f / deltaTime;
            nextUpdateTime = Time.unscaledTime + updateInterval;
        }
    }

    private void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();
        Rect rect = new Rect(10, 10, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = Color.white;

        string text = string.Format("{0:0.} FPS", fps);
        GUI.Label(rect, text, style);
    }
}
