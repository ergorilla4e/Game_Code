using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject[] clientPrefabs;
    [SerializeField] private Clock clock;
    [SerializeField] private ShopManager shopManager;

    public List<int> _indiciOccupati = new List<int>();
    public List<int> indiciLiberi;

    private void Start()
    {
        shopManager = FindObjectOfType<ShopManager>(true);
        StartCoroutine(SpawnInTime());
    }

    IEnumerator SpawnInTime()
    {
        while (true)
        {
            if (clock.GetTimeIsRunning())
            {
                if (clock.GetTimeRemaining() >= clock.GetTempoDiApertura() && clock.GetTimeRemaining() <= clock.GetTempoDiChiusura())
                {
                    if (_indiciOccupati.Count < 5 + (2 * shopManager.GetTavoliAcquistati()))
                    {
                        int randomIndex = GetRandomAvailableIndex();

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

        return -1;
    }
}
