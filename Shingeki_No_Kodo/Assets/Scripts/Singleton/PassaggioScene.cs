using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassaggioScene : MonoBehaviour
{
    #region PARTE DEL SINGLETON
    private static PassaggioScene _instance;

    public static PassaggioScene Instance //property
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PassaggioScene>(true);
            
                if( _instance == null )
                {
                    GameObject obj = Instantiate<GameObject>( Resources.Load("Canvas_Passaggio_Scena") as GameObject);
                    _instance = obj.GetComponent<PassaggioScene>();
                    DontDestroyOnLoad( obj );
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if ( _instance == null )
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if( _instance != this )
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        if ( _instance == null ) 
        {
            _instance = null;
        }
    }
    #endregion    //Classe statica che esiste sempre all'interno del gioco

    public enum STATE
    {
        TRANSPARENT,
        TO_OPAQUE,
        OPAQUE,
        TO_TRANSPARENT
    }

    [SerializeField] private STATE _state = STATE.OPAQUE;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _fadeDuration = 1;

    private float timer = 0;

    private Action onPassaggioScenaCompletato;

    private void Start()
    {
        if(this._canvasGroup == null)
        {
            this._canvasGroup = GetComponent<CanvasGroup>();
        }
    }

    public void StartFadeToOpaque(Action callback = null)
    {
        this.gameObject.SetActive(true);
        this.timer = 0;
        this._state = STATE.TO_OPAQUE;
        this._canvasGroup.alpha = 0;

        this.onPassaggioScenaCompletato = callback;
    }

    public void StartFadeToTransparent(Action callback = null)
    {
        this.gameObject.SetActive(true);
        this.timer = 0;
        this._state = STATE.TO_TRANSPARENT;
        this._canvasGroup.alpha = 1;

        this.onPassaggioScenaCompletato = callback;
    }

    private void Update()
    {
        switch(this._state)
        {
            case STATE.TRANSPARENT:
                
                break;

            case STATE.OPAQUE:

                break;

            case STATE.TO_OPAQUE:
                this.timer += Time.deltaTime;
                if( this.timer < this._fadeDuration )
                {
                    this._canvasGroup.alpha = this.timer / this._fadeDuration;
                }
                else
                {
                    this.timer = 0;
                    this._canvasGroup.alpha = 1;
                    this._state = STATE.OPAQUE;

                    if(this.onPassaggioScenaCompletato != null )
                    {
                        this.onPassaggioScenaCompletato();
                    }
                }
                break;

            case STATE.TO_TRANSPARENT:
                this.timer += Time.deltaTime;
                if (this.timer < this._fadeDuration)
                {
                    this._canvasGroup.alpha = 1 - (this.timer / this._fadeDuration);
                }
                else
                {
                    this.timer = 0;
                    this._canvasGroup.alpha = 0;
                    this._state = STATE.TRANSPARENT;
                    this.gameObject.SetActive(false);

                    if (this.onPassaggioScenaCompletato != null)
                    {
                        this.onPassaggioScenaCompletato();
                    }
                }
                break;
        }
    }


}
