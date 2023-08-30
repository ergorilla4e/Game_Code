using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Global_Light : MonoBehaviour
{
    private Light2D lightComponent;
    
    [SerializeField] private Clock clock;

    private float graduateSpeedLight = 0.05f;

    void Start()
    {
        lightComponent = this.gameObject.GetComponent<Light2D>();
    }

   
    void Update()
    {

        if(clock.GetTimeRemaining() >= clock.GetTempoDiApertura() - 120 && clock.GetTimeRemaining() <= clock.GetTempoDiChiusura() - 60)
        {
            if(lightComponent.intensity <= 1f)
            {
                lightComponent.intensity += Time.deltaTime * graduateSpeedLight;
            }
        }
        else
        {
            if(lightComponent.intensity >= 0.7f)
            {
                lightComponent.intensity -= Time.deltaTime * graduateSpeedLight;
            }
        }

    }
}
