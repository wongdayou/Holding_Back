using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civilian : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float minTimeToShuffle = 2f;
    [SerializeField] private float maxTimeToShuffle = 4f;

    [SerializeField] private int movingDistance = 4;
    [SerializeField] private float timeOutTime = 3f;
    [SerializeField] private bool isStatic = false;

    public float timeToShuffle;
    public float timeSinceShuffle;

    float timeToTimeOut = 0.0f;
    public bool isShuffling = false;
    public int facingDirection = 0; // 0: front, 1: Left, 2: Up, 3: Right
    public Vector3 dir;
    public Vector3 newPos;
    Rigidbody2D m_Rigidbody;

    private void Start() {
        timeToShuffle = 0.0f;
        dir = new Vector3();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        if (m_Rigidbody == null){
            Debug.LogError("There is no rigidbody on civilian");
        }
    }

    private void Update() {
        if (!isShuffling){
            timeSinceShuffle += Time.deltaTime;
            if (timeSinceShuffle >= timeToShuffle){
                isShuffling = true;
                timeSinceShuffle = 0.0f;
                timeToShuffle = Random.Range(minTimeToShuffle, maxTimeToShuffle);
                facingDirection = Random.Range(0,4);
                    
                if (!isStatic){
                    //move towards facing direction.
                    switch(facingDirection){
                        case (0):
                            dir = Vector3.down * Random.Range(-movingDistance, movingDistance);
                            break;
                        case (1):
                            dir = Vector3.left * Random.Range(-movingDistance, movingDistance);
                            break;
                        case (2):
                            dir = Vector3.up * Random.Range(-movingDistance, movingDistance);
                            break;
                        case (3):
                            dir = Vector3.right * Random.Range(-movingDistance, movingDistance);
                            break;
                    }
                    newPos = transform.position + dir;
                }
                else{
                    isShuffling = false;
                }
                
            }
        }
        else{
            timeToTimeOut += Time.deltaTime;
        }

    }

    private void FixedUpdate() {
        if (isShuffling){
            // transform.position = Vector3.MoveTowards(transform.position, dir, speed*Time.deltaTime);
            m_Rigidbody.MovePosition(transform.position + dir * Time.deltaTime * speed);
            if (transform.position == newPos || (timeToTimeOut >= timeOutTime)){
                isShuffling = false;
                timeToTimeOut = 0f;
            }
        }
    }

}
