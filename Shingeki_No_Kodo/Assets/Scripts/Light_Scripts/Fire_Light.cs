using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Fire_Light : MonoBehaviour
{
    private Light2D lightComponent;

    [SerializeField] private Clock clock;

    private float graduateSpeedLight = 0.5f;

    private void Awake()
    {
        lightComponent = this.gameObject.GetComponent<Light2D>();
    }

    private void Update()
    {
        if (clock.GetTimeRemaining() >= clock.GetTempoDiApertura() - 120 && clock.GetTimeRemaining() <= clock.GetTempoDiChiusura() - 60)
        {
            if (lightComponent.intensity <= 2.3f)
            {
                lightComponent.intensity += Time.deltaTime * graduateSpeedLight;
            }
        }
        else
        {
            if (lightComponent.intensity >= 0f)
            {
                lightComponent.intensity -= Time.deltaTime * graduateSpeedLight;
            }
        }
    }
}
