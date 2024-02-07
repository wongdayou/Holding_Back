using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Competitor : MonoBehaviour
{
    public Transform player;
    public float speed = 1f;

    public float overTakeThreshold = 2f;

    bool avoidingObstacle = false;

    public ContactFilter2D contactFilter;

    List<RaycastHit2D> results = new List<RaycastHit2D>();

    Vector2 dir;

    // Start is called before the first frame update
    void Start()
    {
        dir = Vector2.up * speed;
        GetComponent<Rigidbody2D>().velocity = dir;
    }

    // Update is called once per frame
    void Update()
    {
        int hit = Physics2D.Raycast(this.transform.position + Vector3.up * 1.2f, Vector2.up, contactFilter, results, 2f);
        if (hit > 0){
            Debug.Log("THere is obstacle up ahead");
            // if the obstacle is to the right of the road, we go left
            if (results[0].transform.position.x > 0){
                GetComponent<Rigidbody2D>().velocity = new Vector2(-1*speed, dir.y);
            }
            else if (results[0].transform.position.x <= 0){
                GetComponent<Rigidbody2D>().velocity = new Vector2(1*speed, dir.y);
            }
            avoidingObstacle = true;
        }

        else{
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, dir.y);
            avoidingObstacle = false;
        }

        if (!avoidingObstacle){
            if (this.transform.position.y - player.position.y <= overTakeThreshold){
                if (this.transform.position.x > player.position.x){
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-1*speed, dir.y);
                }
                else if (this.transform.position.x < player.position.x){
                    GetComponent<Rigidbody2D>().velocity = new Vector2(1*speed, dir.y);
                }
                else{
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0, dir.y);
                }

            }
        }
        
    }
}
