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
    public bool coinsearching = false;
    public bool playersearching = false;

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

    void Update()
    {
        if (coinText == null && coinsearching == false)
        {
            coinsearching = true;
            StartCoroutine(FindcoinText());
        }

        if (player == null && playersearching == false)
        {
            playersearching = true;
            StartCoroutine(FindPlayer());
        }
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
            playersearching = false;
            yield break;
        }

        else if (player.CompareTag("Player") == false)
        {
            print("RETRYING");
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(FindPlayer());
        }
    }

    public IEnumerator FindcoinText()
    {
        coinText = GameObject.Find("CoinsText").GetComponent<TMPro.TextMeshProUGUI>();

        if (coinText != null)
        {
            print("here");
            coinsearching = false;
            yield break;
            print("there");
        }

        else if (coinText = null)
        {
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(FindcoinText());
        }
    }

    public void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        FindPlayer();
    }

}
