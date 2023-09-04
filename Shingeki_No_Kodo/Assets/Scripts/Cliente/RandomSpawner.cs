using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject[] clientPrefabs;
    [SerializeField] private Clock clock;

    private ShopManager shopManager;

    public List<int> _indiciOccupati = new List<int>();
    public List<int> indiciLiberi;

    private void Awake()
    {
        shopManager = FindObjectOfType<ShopManager>(true);
    }

    private void Start()
    {
        StartCoroutine(SpawnInTime());
    }

    IEnumerator SpawnInTime()
    {
        while (true)
        {
            if (clock.GetTimeIsRunning())
            {
                //Se il negozio è aperto
                if (clock.GetTimeRemaining() >= clock.GetTempoDiApertura() && clock.GetTimeRemaining() <= clock.GetTempoDiChiusura())
                {
                    if (_indiciOccupati.Count < 5 + (2 * shopManager.GetTavoliAcquistati()))
                    {
                        int randomIndex = GetRandomAvailableIndex();

                        //Entra nell'if solo se ha trovato un indice libero
                        if (randomIndex != -1)
                        {
                            Instantiate(clientPrefabs[randomIndex], spawnPoint.position, Quaternion.identity);
                            _indiciOccupati.Add(randomIndex);
                        }
                    }
                }
                else
                {
                    _indiciOccupati.Clear();
                }
                
                yield return new WaitForSeconds(Random.Range(5 - clock.GetContatoreGiorni(), 10 - clock.GetContatoreGiorni()));
            } 
            else
            {
                yield return null; //metto in pausa lo spawn dei clienti
            }
        }
    }

    private int GetRandomAvailableIndex()
    {
        indiciLiberi = new List<int>();

        for (int i = 0; i < 5 + (2 * shopManager.GetTavoliAcquistati()); i++)
        {
            if (!_indiciOccupati.Contains(i))
            {
                indiciLiberi.Add(i);
            }
        }

        if (indiciLiberi.Count > 0)
        {
            return indiciLiberi[Random.Range(0, indiciLiberi.Count)];
        }

        // ES: ci sono 3 indici liberi che sono: 8,13 e 4
        //     indiciLiberi[3] = {8,13,4};
        //     Random.Range(0, indiciLiberi.Count) restituisce un numero random tra 0 e la lunghezza della lista di indiciLiberi
        //     in questo caso esce un numero casuale tra 0 e 2 (indiciLiberi.Count escluso)
        //     mettiamo caso esca 1
        //     allora return indiciLiberi[1] = 13, quindi la funzione restitusce 13.

        return -1;
    }
}
