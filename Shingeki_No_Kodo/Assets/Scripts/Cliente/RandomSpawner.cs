using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] public GameObject[] clientPrefabs;
    [SerializeField] private Clock clock;

    public List<int> _indiciOccupati = new List<int>();
    public List<int> indiciLiberi;

    private void Start()
    {
        StartCoroutine(SpawnInTime());
    }

    IEnumerator SpawnInTime()
    {
        while (true)
        {
            if (clock.GetTimeIsRunning()) //Todo: far funzionare il codice con il timer, se si ferma non entrano i clienti
            {
                if (clock.GetTimeRemaining() >= clock.GetTempoDiApertura() && clock.GetTimeRemaining() <= clock.GetTempoDiChiusura())
                {
                    if (_indiciOccupati.Count < 5)
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

                yield return new WaitForSeconds(Random.Range(3, 14));
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

        for (int i = 0; i < clientPrefabs.Length; i++)
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
