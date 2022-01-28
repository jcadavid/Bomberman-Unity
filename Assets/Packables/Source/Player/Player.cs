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
    int _life = 10;
    private bool invulnerable;
    public int life;

    private void Start()
    {
        
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
            if(life <= 0){
                BombermanEvent.onPlayerDie?.Invoke(this);
            }
            else{
                _healthPoints = 1;
            }
        }
        Debug.Log(_healthPoints);
    }
}
