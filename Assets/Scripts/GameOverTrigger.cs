using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Fruit")) {
            var fruit = collision.GetComponent<Fruit>();
            if(fruit != null) {
                if (!fruit.IsInBox) {
                    fruit.SetFruitInBox(true);
                }
                else {
                    Debug.Log("GameOver");
                }
            }
        }
    }
}
