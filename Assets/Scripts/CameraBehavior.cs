using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;
    public bool firstTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        firstTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (firstTime)
        {
            player = GameObject.FindWithTag("Player");

            if (player != null)
            {
                offset = player.transform.position - transform.position;
                offset.z = -5;
                offset.y = 2;
                firstTime = false;
            }
            
            else if (player = null)
            {
                print("Player not Found");
            }
        }

        else if (firstTime == false)
        {
            transform.position = player.transform.position + offset;
        }
        
    }
}
