using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public Player player;

    private void Start() {
        GenerateFood();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        GenerateFood();
    }

    private void GenerateFood(){
        float x;
        float y;
        do{
            x = Mathf.Round(Random.Range(-18, 19));
            y = Mathf.Round(Random.Range(-10, 8));
            y += 0.5f;
        }while(x == transform.position.x && y == transform.position.y || InsideSnake(x, y));

        transform.position = new Vector3(x, y, 0.0f);
    }

    private bool InsideSnake(float x, float y){
        foreach(Transform t in player.body){
            if(x == t.position.x && y == t.position.y) return true;
        }
        return false;
    }
}
