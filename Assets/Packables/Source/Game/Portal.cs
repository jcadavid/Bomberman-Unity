using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float rotationSpeed = 10f;
    Vector3 rotation;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rotation.z = rotationSpeed * Time.deltaTime;

        transform.Rotate(rotation, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<BombermanController>().checkEnemies(transform.position,other);
        }
    }
}
