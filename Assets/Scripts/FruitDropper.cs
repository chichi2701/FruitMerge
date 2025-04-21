using UnityEngine;
using System.Collections;

public class FruitDropper : MonoBehaviour {
    [Header("Fruit Settings")]
    public GameObject[] fruitPrefabs;
    public float spawnCooldown = 1.5f;

    [Header("References")]
    public SpawnBarController spawnBar;
    public Transform spawnBarPosition;

    private GameObject currentFruit;
    private GameObject currentPrefab;
    private bool isDragging = false;
    private bool canSpawn = true;

    private void Start() {
        currentPrefab = GetRandomFruit();
        SpawnFruit(currentPrefab);
    }

    void Update() {
        HandleInput();

        if (isDragging)
            FollowBarPosition();
    }

    void HandleInput() {
        if (UniversalInput.GetInputDown(out Vector2 _) && canSpawn && currentFruit == null) {
            StartDrag();
        }

        if (UniversalInput.GetInputUp(out Vector2 _) && currentFruit != null) {
            DropFruit();
        }
    }

    void StartDrag() {
        isDragging = true;
        
    }

    GameObject GetRandomFruit() {
        int index = Random.Range(0, fruitPrefabs.Length);
        return fruitPrefabs[index];
    }

    void SpawnFruit(GameObject prefab) {
        float offset = prefab.transform.localScale.x;
        Vector3 spawnPos = spawnBarPosition.transform.position + Vector3.down * offset;

        currentFruit = Instantiate(prefab, spawnPos, Quaternion.identity);
        currentFruit.transform.SetParent(spawnBarPosition);
        currentFruit.gameObject.SetActive(true);
        currentFruit.GetComponent<Rigidbody2D>().simulated = false;
    }

    void DropFruit() {
        isDragging = false;

        if (currentFruit != null) {
            currentFruit.GetComponent<Rigidbody2D>().simulated = true;
            currentFruit.transform.SetParent(null);
            currentFruit = null;

            canSpawn = false;
            StartCoroutine(SpawnCooldown());
        }
    }

    void FollowBarPosition() {
        if (currentFruit == null) return;

        Vector3 pos = currentFruit.transform.position;
        pos.x = spawnBar.CurrentPosition.x;
        currentFruit.transform.position = pos;
    }

    IEnumerator SpawnCooldown() {
        yield return new WaitForSeconds(spawnCooldown);
        currentPrefab = GetRandomFruit();
        SpawnFruit(currentPrefab);
        canSpawn = true;
    }
}
