using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class FlickeringLight2D : MonoBehaviour
{
    [Header("Normal Light Settings")]
    public float normalIntensity = 1f;

    [Header("Flicker Event Settings")]
    [Tooltip("Minimum time between flicker events, in seconds.")]
    public float minTimeBetweenFlickers = 3f;
    [Tooltip("Maximum time between flicker events, in seconds.")]
    public float maxTimeBetweenFlickers = 8f;

    [Tooltip("How low the light dips during a flicker.")]
    public float flickerDipIntensity = 0.2f;

    [Tooltip("How long a single flicker event lasts.")]
    public float flickerDuration = 0.15f;

    [Tooltip("How many quick dips happen per flicker event.")]
    public int flickersPerEvent = 2;

    private Light2D light2D;

    private void Awake()
    {
        light2D = GetComponent<Light2D>();
        light2D.intensity = normalIntensity;
    }

    private void Start()
    {
        StartCoroutine(FlickerLoop());
    }

    private IEnumerator FlickerLoop()
    {
        while (true)
        {
            float waitTime = Random.Range(minTimeBetweenFlickers, maxTimeBetweenFlickers);
            yield return new WaitForSeconds(waitTime);

            yield return DoFlickerEvent();
        }
    }

    private IEnumerator DoFlickerEvent()
    {
        for (int i = 0; i < flickersPerEvent; i++)
        {
            light2D.intensity = flickerDipIntensity;
            yield return new WaitForSeconds(flickerDuration);

            light2D.intensity = normalIntensity;
            yield return new WaitForSeconds(flickerDuration);
        }
    }
}