using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{    
    public int _healthPoints = 1;
    int _life = 5;
    private bool invulnerable = false;

    private void Start()
    {
        
    }    

    void Update()
    {
               
    }

    internal void ReduceHealth()
    {
        if(!invulnerable){
            _healthPoints -= 1;
        }
        Debug.Log(_healthPoints);
    }
}
