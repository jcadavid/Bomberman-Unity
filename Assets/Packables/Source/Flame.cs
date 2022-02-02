using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{

    float _timeToDestroy = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("Destroy",_timeToDestroy);
    }

    void Destroy(){
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            other.GetComponent<Player>().ReduceHealth();
        } else if(other.gameObject.tag == "Enemy"){
            other.GetComponent<Enemy>().ReduceHealth();
        }
        

    }

    
}
