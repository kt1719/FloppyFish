using TMPro;
using UnityEngine;

public class FPSTracker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsCounterText;
    private float fps;

    private void Start()
    {
        InvokeRepeating("GetFPS", 1, 1);
    }

    private void GetFPS()
    {
        fps = (int) (1f / Time.unscaledDeltaTime);
        fpsCounterText.text = "FPS: " + fps.ToString();
    }
}
