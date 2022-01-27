using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
   public int _healthPoints;


    internal void ReduceHealth(){       
        _healthPoints -= 1;       
        Debug.Log(_healthPoints);
    }
}
