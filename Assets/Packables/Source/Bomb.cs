using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    float _timeToExplote = 5f;
    float _animationMultiplier = 1f;
    [SerializeField]
    float _animationAdditionMultipler = 3;
    private Animator _animator;
    private CircleCollider2D _circleCollider;
    [SerializeField]
    private int _radiusExplotion = 2;
    PlayerBombSpawner _playerSpawner;
    [SerializeField]
    GameObject _flamePrefab;

    
    void Start()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
        _animator = GetComponent<Animator>();
        _animationAdditionMultipler = _animationAdditionMultipler/(_timeToExplote*1000);
        Invoke("Explode",_timeToExplote);
    }

    public void Init(PlayerBombSpawner playerSpawner){
        _playerSpawner = playerSpawner;
    }

    // Update is called once per frame
    void Update()
    {
        _animationMultiplier += _animationAdditionMultipler*Time.deltaTime*1000;
        _animator.SetFloat("Animation_Multiplier",_animationMultiplier);
    }

    void Explode(){
        _playerSpawner._bombQuantity -= 1;
        gameObject.SetActive(false);
        FindObjectOfType<MapDestroyer>().Explode(transform.position);
        Destroy(gameObject);
    }

    

    

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            _circleCollider.isTrigger=false;
        }
    }

    
    

    
}
