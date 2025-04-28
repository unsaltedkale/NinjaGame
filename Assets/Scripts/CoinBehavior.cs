using UnityEngine;

public class CoinBehavior : MonoBehaviour
{

    public GameManager gm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);

            gm.coinAdd(1);

            print("Total Coins: " + gm.coinRead());
        }
    }
}
