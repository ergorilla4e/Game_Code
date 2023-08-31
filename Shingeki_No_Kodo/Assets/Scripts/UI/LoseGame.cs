using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComoponent;
    [SerializeField] private GameObject bottone_torna_al_menu;
    [SerializeField] private GameObject bottone_riprova_il_gioco;
    [SerializeField] private string[] lines;
    [SerializeField] private float speedText;

    private int _index;
    private int _contatoreInvii;

    private void Start()
    {
        bottone_torna_al_menu.SetActive(false);
        bottone_riprova_il_gioco.SetActive(false);
        _contatoreInvii = 1;
        textComoponent.text = string.Empty;
        StartDialog();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComoponent.text == lines[_index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComoponent.text = lines[_index];
            }
        }

        if (_contatoreInvii >= lines.Length)
        {
            bottone_torna_al_menu.SetActive(true);
            bottone_riprova_il_gioco.SetActive(true);
        }

    }

    public void ReturnToMenu()
    {
        PassaggioScene.Instance.StartFadeToOpaque(
            () =>
            {
                SceneManager.LoadScene(0);
                PassaggioScene.Instance.StartFadeToTransparent();
            });
    }

    public void RestartGame()
    {
        PassaggioScene.Instance.StartFadeToOpaque(
            () =>
            {
                SceneManager.LoadScene(1);
                PassaggioScene.Instance.StartFadeToTransparent();
            });
    }

    public void StartDialog()
    {
        _index = 0;

        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[_index].ToCharArray())
        {
            textComoponent.text += c;
            yield return new WaitForSeconds(speedText);
        }
    }

    public void NextLine()
    {
        if (_index < lines.Length - 1)
        {
            _contatoreInvii++;

            _index++;
            textComoponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
    }

}
