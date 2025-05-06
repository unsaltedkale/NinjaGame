using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.Mathematics;

public class Enemy : MonoBehaviour
{

    public GameObject waypoint1;
    public GameObject waypoint2;
    public GameObject currentwp;
    public float speed = 2f;
    public float closest_distance;
    public GameObject player;
    private Vector3 playercurrentposition;
    public States state;
    private bool PlayerInTrigger;
    public float waitTime;
    public float maxwaitTime = 3;
    public GameObject wp1;
    public GameObject wp2;
    public Vector3 prevPosition;

    public enum States
    {
        Patrol,
        Chasing,
        Wait,
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        prevPosition = transform.position;

        Vector3 vector1 = new Vector3(transform.position.x - 3, transform.position.y, transform.position.z);
        waypoint1 = Instantiate(wp1, vector1, Quaternion.identity);

        Vector3 vector2 = new Vector3(transform.position.x + 3, transform.position.y, transform.position.z);
        waypoint2 = Instantiate(wp2, vector2, Quaternion.identity);

        currentwp = waypoint1;
        waitTime = maxwaitTime;

        state = States.Patrol;

        StartCoroutine(FindPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x - prevPosition.x) > 0.1f)
        {
            if (transform.position.x > prevPosition.x)
            {
                Vector3 lTemp = transform.localScale;
                lTemp.x = -1;
                transform.localScale = lTemp;
            }
            
            else if (transform.position.x < prevPosition.x)
            {
                Vector3 lTemp = transform.localScale;
                lTemp.x = 1;
                transform.localScale = lTemp;
            }

            prevPosition = transform.position;
        }

        if (state == States.Patrol)
        {
    
            // Check if the position of the enemy and target are approximately equal.
            if ((Mathf.Abs(transform.position.x - currentwp.transform.position.x)) < 0.1f)
            {
                waitTime -= Time.deltaTime;
            
                if (waitTime <= 0)
                {
                    if (currentwp == waypoint1)
                    {
                        currentwp = waypoint2;
                    }

                    else if (currentwp == waypoint2)
                    {
                        currentwp = waypoint1;
                    }

                    waitTime = maxwaitTime;
                }
            }

            transform.position = Vector2.MoveTowards(transform.position, new Vector2 (currentwp.transform.position.x, transform.position.y),  speed * Time.deltaTime);
            
        }

        else if (state == States.Chasing)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position,  speed * Time.deltaTime);
        }
    }

    public IEnumerator FindPlayer()
    {
        print("TRYING");
        player = GameObject.Find("Player Ninja(Clone)");

        if (player.CompareTag("Player") == false)
        {
            print("RETRYING");
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(FindPlayer());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            state = States.Chasing;
            PlayerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInTrigger = false;
            StartCoroutine(ChasingStop());
        }
    }

    private IEnumerator ChasingStop()
    {
        yield return new WaitForSeconds(2);
            if (PlayerInTrigger == true);
            {
                state = States.Chasing;
                yield return state;
            }

            if (PlayerInTrigger == false);
            {
                state = States.Wait;

                yield return new WaitForSeconds(1);
                
                if (PlayerInTrigger == true);
                {
                    state = States.Chasing;
                    yield return state;
                }
                
                if (PlayerInTrigger == false)
                {
                    state = States.Patrol;
                }
            }
            // if player out of trigger set to wait and then patrol
    }

}
