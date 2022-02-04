using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public int _healthPoints;
    bool invulnerable;
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public SpriteRenderer sp;


    private void Start() {
        sp = GetComponent<SpriteRenderer>();
    }
    internal void ReduceHealth()
    {
        if (!invulnerable)
        {
            if (_healthPoints == 1)
            {
                BombermanEvent.onEnemyDeath?.Invoke();
                Destroy(gameObject);
            }
            _healthPoints -= 1;
            StartCoroutine("FlashCo");
        }
    }

    private IEnumerator FlashCo()
    {
        invulnerable = true;
        int temp = 0;
        gameObject.layer = 10;
        while (temp < numberOfFlashes)
        {
            sp.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            sp.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        invulnerable = false;
        gameObject.layer = 3;
    }

}
