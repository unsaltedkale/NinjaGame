using UnityEngine;

public class ItemBox : MonoBehaviour
{

    public GameObject gobgob;
    public GameObject powerup;
    public bool HasBeenHit = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Player") && !HasBeenHit)
        {
            if (other.gameObject.transform.position.y < transform.position.y)
            {
                Hit();

                GetComponent<SpriteRenderer>().color = new Color(0.4577f, 0.2509f, 0, 1);
                
                HasBeenHit = true;
            }
        }
    }

    public void Hit()
    {
        Vector3 vector = new Vector3(0, 1, 0);

        GameObject g = gobgob;

        float rnum = Random.Range(0f,1f);
        print(rnum);

        if (rnum <= 0.25)
        {
            g = powerup;
        }

        GameObject clone = Instantiate(g, transform.position + vector, Quaternion.identity);
        
    }

}
