using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 0.5f;
    [SerializeField] private float shakeIntensity = 0.1f;

    private Vector3 originalPosition;
    private float currentShakeDuration = 0f;

    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        if (currentShakeDuration > 0)
        {
            Vector3 shakeAmount = Random.insideUnitSphere * shakeIntensity;
            transform.localPosition = originalPosition + shakeAmount;

            currentShakeDuration -= Time.deltaTime;
        }
        else
        {
            currentShakeDuration = 0f;
            transform.localPosition = originalPosition;
        }
    }

    public void ShakeCamera()
    {
        currentShakeDuration = shakeDuration;
    }
}
