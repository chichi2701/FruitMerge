using UnityEngine;

public class FruitMergeManager : MonoBehaviour {
    public GameObject[] fruitPrefabs;

    private void OnEnable() {
        EventDispatcher.AddListener<EventDefine.OnFruitMerge>(HandleMerge);
    }

    private void OnDisable() {
        EventDispatcher.RemoveListener<EventDefine.OnFruitMerge>(HandleMerge);
    }

    private void HandleMerge(EventDefine.OnFruitMerge info) {       
        int nextIndex = (int)info.Type + 1;
        if (nextIndex >= (int)FruitType.Length) return;

        GameObject prefab = fruitPrefabs[nextIndex];
        var newFruit = Instantiate(prefab, info.SpawnPosition, Quaternion.identity);
        var rig = newFruit.GetComponent<Rigidbody2D>();
        var fruit = newFruit.GetComponent<Fruit>();
        newFruit.gameObject.SetActive(true);
        if (rig != null) {
            Vector2 explosionPoint = new Vector2(0, 0); // Hoặc vị trí spawn/merge
            rig.AddExplosionForce(5f, info.SpawnPosition, 3f);

        }
        if(fruit != null) {
            fruit.SetFruitInBox(true);
        }
        Destroy(info.FruitA.gameObject);
        Destroy(info.FruitB.gameObject);
    }
}


public static class Rigidbody2DExtensions {
    public static void AddExplosionForce(this Rigidbody2D body, float explosionForce, Vector2 explosionPosition, float explosionRadius) {
        Vector2 direction = (Vector2)body.transform.position - explosionPosition;
        float wearoff = 1 - (direction.magnitude / explosionRadius);
        if (wearoff <= 0) return;

        body.AddForce(direction.normalized * explosionForce * wearoff, ForceMode2D.Impulse);
    }
}