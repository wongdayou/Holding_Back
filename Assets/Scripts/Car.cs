using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{   
    public float speed = 1f;
    [SerializeField] private float direction = 90.0f;
    [SerializeField] private float slowDownTime = 2f;

    public ContactFilter2D contactFilter;
    Vector3 velocityVector;
    Quaternion rotationAngle;

    public bool autoStart = false;
    public bool questStarted = false;
    bool isDecelerating = false;
    List<RaycastHit2D> results = new List<RaycastHit2D>();

    private void Start() {
        if (autoStart){
            SetCarMovement(direction, speed);
        }
    }

    private void Update() {
        
        if (questStarted){
            int hit = Physics2D.Raycast(this.transform.position + velocityVector * 1.2f, velocityVector, contactFilter, results, 0.1f);
            if (hit > 0){
                Debug.Log("Stopping");
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }    
        }

        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Collision");
        PlayerControls playerScript = other.transform.GetComponent<PlayerControls>();
        if (playerScript != null) {
            playerScript.enabled = false;

            Transform cam = other.transform.Find("Main Camera");
            if (cam != null){
                Debug.Log("Removing camera parent");
                cam.SetParent(null);
            }
            // get players rigidbody and apply force to it
            Rigidbody2D playersRB = other.transform.GetComponent<Rigidbody2D>();
            if (playersRB != null){
                Debug.Log("Pushing player");
                Vector2 pushVector = new Vector2(velocityVector.x == 0 ? 0 : velocityVector.x * 5.0f, velocityVector.y == 0 ? 0 : velocityVector.y * 5.0f);
                playersRB.velocity = pushVector;
            }
            Debug.Log("Player Hit. Lost game");
            GameMaster.instance.PlayerLose();
        }

        // TODO call gamemaster to enable lost game process

    }

    public void SetCarMovement(float _direction, float _speed){
        speed = _speed;    
        switch(_direction){
            case 0.0f:
                velocityVector = new Vector2(-1,0);
                break;

            case 90.0f:
                velocityVector = new Vector2(0,1);
                break;
            case -90.0f:
                velocityVector = new Vector2(0,-1);
                break;

            case 180.0f:
                velocityVector = new Vector2(1,0);
                break;
            
        }
        GetComponent<Rigidbody2D>().velocity = velocityVector * speed;
    }

    public void QuestEnded(){
        questStarted = false;
        GetComponent<Rigidbody2D>().velocity = velocityVector * speed;
    }
}
