using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiarrheaPill : MonoBehaviour
{
    public Timer timer;
    [SerializeField] private float timeAmt = 2f;

    private void OnTriggerEnter2D(Collider2D other) {
        PlayerControls _p = other.gameObject.GetComponent<PlayerControls>();
        if (_p != null){
            timer.IncreaseTime(timeAmt);
            _p.IncreaseTimeNotif(timeAmt);
            Destroy(this.gameObject);
        }
    }
}
