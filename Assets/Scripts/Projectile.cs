using UnityEngine;
using System.Collections;
using Unity.Mathematics;

public class Projectile : MonoBehaviour
{

    public float speed = 20f;
    public Vector3 direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
        StartCoroutine(die_soon());
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator die_soon()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
