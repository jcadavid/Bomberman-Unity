using System;
using System.Collections;
using UnityEngine;

public enum PlayerNumber
{
    Player1,
    Player2,
    Player3,
    Player4
}
public class Player : MonoBehaviour
{


    public int _healthPoints;

    public PlayerNumber _playerNumber;
    public int _maxLives = 5;
    private bool invulnerable;
    public int _currentLife;

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
        _currentLife = 5;
        BombermanEvent.OnLifeUpdatedEvent?.Invoke(_currentLife);
    }

    void Update()
    {

    }

    public void ReduceHealth()
    {
        if (!invulnerable)
        {
            if (_currentLife == 1)
            {
                BombermanEvent.onPlayerDie?.Invoke(this);
                Destroy(gameObject);
            }
            _currentLife -= 1;
            BombermanEvent.OnLifeUpdatedEvent?.Invoke(_currentLife);
            transform.position = initialPosition;
        }

    }

    private IEnumerator FlashCo()
    {
        int temp = 0;
        Physics.IgnoreLayerCollision(3, 8, true);
        while (temp < numberOfFlashes)
        {
            sp.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            sp.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
        }
        Physics.IgnoreLayerCollision(3, 8, false);
    }


}
