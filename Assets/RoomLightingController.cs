using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RoomLightingController : MonoBehaviour
{
    public Light2D globalLight;
    public GameObject playerSpotlight;

    [Header("Other lights to turn off after Quest 3 (e.g. lamps, candles)")]
    public GameObject[] lightsToDisableAfterQuest3;

    [Header("Normal Mode (Default, before Quest 3)")]
    public float normalGlobalIntensity = 1f;
    public Color normalGlobalColor = Color.white;

    [Header("After Quest 3")]
    [Tooltip("If true, this scene dims to a nighttime blue tint (for the actual outdoor scene). If false, it goes fully dark (for indoor rooms).")]
    public bool isOutdoorScene = false;

    [Tooltip("Used when isOutdoorScene is true - a dim nighttime look.")]
    public float outdoorNightIntensity = 0.3f;
    public Color outdoorNightColor = new Color(0.15f, 0.15f, 0.35f);

    [Tooltip("Used when isOutdoorScene is false - full darkness for indoor rooms.")]
    public float indoorDarkIntensity = 0.03f;
    public Color indoorDarkColor = Color.black;

    private bool appliedDarkMode = false;

    private void Start()
    {
        ApplyLightingState();
    }

    private void Update()
    {
        if (GameState.ThirdQuestCompleted && !appliedDarkMode)
        {
            ApplyLightingState();
        }
    }

    private void ApplyLightingState()
    {
        if (GameState.ThirdQuestCompleted)
        {
            if (isOutdoorScene)
            {
                globalLight.intensity = outdoorNightIntensity;
                globalLight.color = outdoorNightColor;
            }
            else
            {
                globalLight.intensity = indoorDarkIntensity;
                globalLight.color = indoorDarkColor;
            }

            if (playerSpotlight != null)
                playerSpotlight.SetActive(true);

            foreach (GameObject light in lightsToDisableAfterQuest3)
            {
                if (light != null)
                    light.SetActive(false);
            }

            appliedDarkMode = true;
        }
        else
        {
            globalLight.intensity = normalGlobalIntensity;
            globalLight.color = normalGlobalColor;

            if (playerSpotlight != null)
                playerSpotlight.SetActive(false);
        }
    }
}