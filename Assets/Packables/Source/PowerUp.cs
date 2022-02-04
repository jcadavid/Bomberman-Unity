using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType
{
    AdditionalBomb,
    AdditionalFlame,
    BoostSpeed
}
public class PowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    BombermanController _bombermanController;
    private const string POWER_UP_PATH = "Sprites/PowerUp/PU_{0}";
    private PowerUpType type;
    private SpriteRenderer _renderer;

    [SerializeField]

    private void Start()
    {
        Init();
    }

   /* private void Update()
    {
        float yValue = Time.deltaTime * yVelocity;
        transform.Translate(0, -yValue, 0);
    }
    */
    private void Init()
    {
        _renderer = GetComponentInChildren<SpriteRenderer>();
        type = (PowerUpType)Random.Range(0, 3);
        _renderer.sprite = GetPowerUpSprite();
        _bombermanController = FindObjectOfType<BombermanController>();

    }

    private Sprite GetPowerUpSprite()
    {
        string path = string.Empty;
        path = string.Format(POWER_UP_PATH, ((int)type));
        Debug.Log(path);
        if (string.IsNullOrEmpty(path))
        {
            return null;
        }

        return Resources.Load<Sprite>(path);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Effect(other);
            Destroy(gameObject);                        
        }        
    }

    private void Effect(Collider2D other)
    {
        switch (type)
        {
            case PowerUpType.AdditionalBomb:
                {
                    AdditionalBomb(other);
                    break;
                }
            case PowerUpType.AdditionalFlame:
                {
                    AdditionalFlame();
                    break;
                }
            case PowerUpType.BoostSpeed:
                {
                    BoostSpeed(other);
                    break;
                }            
            default: break;
        }
    }

    private void BoostSpeed(Collider2D other)
    {
        other.gameObject.GetComponent<PlayerMovement>().BoostSpeed();
    }

    private void AdditionalFlame()
    {
        _bombermanController.addRadiusExplotion();
    }

    private void AdditionalBomb(Collider2D other)
    {
        other.gameObject.GetComponentInChildren<PlayerBombSpawner>().AddMaxBomb();
    }
}
