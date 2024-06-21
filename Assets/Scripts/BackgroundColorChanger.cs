using UnityEngine;

public class BackgroundColorChanger : MonoBehaviour
{
    public Camera targetCamera; // دوربینی که می‌خواهید رنگ پس‌زمینه آن را تغییر دهید
    public float duration = 5f; // مدت زمانی که تغییر رنگ طول می‌کشد

    private float elapsedTime = 0f;

    void Start()
    {
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        float t = Mathf.Repeat(elapsedTime / duration, 1f);
        targetCamera.backgroundColor = Color.HSVToRGB(t, 1f, 1f);
    }
}
