using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class Cliente_Movements : MonoBehaviour
{

    [SerializeField] private Transform[] Points;
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float Pazienza = 2;

    private int _indexPoint = 0;
    private float _tempoAtteso = 0;

    private void Start()
    {
        this.transform.position = this.Points[_indexPoint].transform.position;
    }

    void Update()
    {
        #region SEGUI_PERCORSO
        if (_indexPoint < this.Points.Length && _tempoAtteso < Pazienza)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, this.Points[_indexPoint].transform.position, this.MoveSpeed * Time.deltaTime);

            if (this.transform.position == this.Points[_indexPoint].transform.position)
            {
                Debug.Log(_indexPoint);
                this._indexPoint++;
                _tempoAtteso = 0;
            }
        }
        else if (_indexPoint > 0 && _tempoAtteso >= Pazienza)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, this.Points[_indexPoint - 1].transform.position, this.MoveSpeed * Time.deltaTime);

            if (this.transform.position == this.Points[_indexPoint - 1].transform.position)
            {
                Debug.Log(_indexPoint);
                this._indexPoint--;
            }

        }

        if (_indexPoint == Points.Length)
        {
            _tempoAtteso += Time.deltaTime;
            Debug.Log(_tempoAtteso);
        }

        #endregion

        
        //TODO


    }




}
