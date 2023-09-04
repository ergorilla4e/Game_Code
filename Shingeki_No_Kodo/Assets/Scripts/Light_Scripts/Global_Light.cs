using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Global_Light : MonoBehaviour
{
    [SerializeField] private Clock clock;

    private Light2D _lightComponent;
    private float _graduateSpeedLight = 0.05f;

    private void Awake()
    {
        _lightComponent = this.gameObject.GetComponent<Light2D>();
    }

    private void Update()
    {
        //Controllo l'orario del giorno e in base a quello aggiorno la luminosità della Light2D (Sole) per dare un effetto di giorno/notte
        if (clock.GetTimeRemaining() >= clock.GetTempoDiApertura() - 120 && clock.GetTimeRemaining() <= clock.GetTempoDiChiusura() - 60)
        {
            if (_lightComponent.intensity <= 1f)
            {
                _lightComponent.intensity += Time.deltaTime * _graduateSpeedLight;
            }
        }
        else
        {
            if (_lightComponent.intensity >= 0.7f)
            {
                _lightComponent.intensity -= Time.deltaTime * _graduateSpeedLight;
            }
        }
    }
}
