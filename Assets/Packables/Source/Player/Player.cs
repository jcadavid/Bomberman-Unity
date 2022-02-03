using System;
using System.Collections;
using UnityEngine;

public enum PlayerNumber{
    Player1,
    Player2,
    Player3,
    Player4
}
public class Player : MonoBehaviour
{   


    public int _healthPoints;

    public PlayerNumber _playerNumber;
    public int _lives = 5;
    private bool invulnerable;
    public int life;

    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public SpriteRenderer sp;
    
    Vector3 initialPosition;

    
    private void Start()
    {
        initialPosition = transform.position;
        sp = GetComponent<SpriteRenderer>();
    }    

    void Update()
    {
         
    }

    public void ReduceHealth()
    {
        if(!invulnerable){
            _healthPoints -= 1;
        }
        if(_healthPoints <= 0){
            if(_lives <= 0){
                BombermanEvent.onPlayerDie?.Invoke(this);
            }
            else{
                _healthPoints = 1;
                _lives -=1;
                transform.position = initialPosition;
                StartCoroutine(FlashCo());
            }
        }
        Debug.Log(_healthPoints);
    }

    private IEnumerator FlashCo(){
        int temp = 0;
        Physics.IgnoreLayerCollision(3,8,true);
        while(temp < numberOfFlashes){
            sp.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            sp.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
        }
        Physics.IgnoreLayerCollision(3, 8,false);
    }


}
