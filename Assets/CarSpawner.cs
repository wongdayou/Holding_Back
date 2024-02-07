using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private float carSpeed = 3f;
    [SerializeField] private float minSpawnTime = 0.5f;
    [SerializeField] private float maxSpawnTime = 2f;


    enum Direction {Left, Right, Up, Down};

    [SerializeField] private Direction direction;

    float timeSinceSpawn = 0f;
    float timeToSpawn = 0f;

    bool isSpawning = false;
    float directionMagnitude = 0.0f;

    private void Start() {
        if (direction == Direction.Left){
            directionMagnitude = 0.0f;
        }
        else if (direction == Direction.Right){
            directionMagnitude = 180.0f;
        }
        else if (direction == Direction.Up){
            directionMagnitude = 90.0f;
        }
        else {
            directionMagnitude = -90.0f;
        }
    }

    private void Update() {
        if (!isSpawning){
            timeSinceSpawn += Time.deltaTime;
            if (timeSinceSpawn >= timeToSpawn){
                isSpawning = true;
                SpawnCar();
                return;
            }
        }
    }

    void SpawnCar(){

        GameObject car = Instantiate(carPrefab, transform.position, Quaternion.identity);
        Car _car = car.GetComponent<Car>();
        if (_car == null){
            Debug.LogError("Car has no car script");
        }
        else{
            _car.SetCarMovement(directionMagnitude, carSpeed);
        }
        timeSinceSpawn = 0.0f;
        timeToSpawn = Random.Range(minSpawnTime, maxSpawnTime);
        isSpawning = false;


    }


}
