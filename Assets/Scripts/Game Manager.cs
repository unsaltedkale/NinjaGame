using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int coinTotal;
    
    public static GameManager gm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (gm != null && gm != this)
        {
            Destroy(gameObject);
        }
        else
        {
            gm = this;
            DontDestroyOnLoad(this.gameObject);
        }

    }

    public void coinAdd(int i)
    {
        coinTotal = coinTotal + i;
    }

    public void coinSet(int i)
    {
        coinTotal = i;
    }

    public int coinRead()
    {
        return coinTotal;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
