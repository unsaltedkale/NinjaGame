using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using Unity.Mathematics;

public class GameManager : MonoBehaviour
{

    public int coinTotal;
    public static GameManager gm;
    public TextMeshProUGUI coinText;
    public GameObject player;
    public Vector3 respawnplace;

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

        coinText.text = "";
    }

    public void coinAdd(int i)
    {
        coinTotal = coinTotal + i;
        Render();
    }

    public void coinSet(int i)
    {
        coinTotal = i;
        Render();
    }

    public int coinRead()
    {
        Render();
        return coinTotal;
    }
    public void Render()
    {
        coinText.text = ("Coins: " + coinTotal);
    }

    public IEnumerator FindPlayer()
    {
        print("TRYING");
        player = GameObject.Find("Player Ninja(Clone)");

        if (player.CompareTag("Player") == true)
        {
            
        }

        else if (player.CompareTag("Player") == false)
        {
            print("RETRYING");
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(FindPlayer());
        }
    }

    public void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
