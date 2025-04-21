using UnityEngine;
using static EventDefine;

public class Fruit : MonoBehaviour {
    [SerializeField] private FruitType type;
    [SerializeField] private bool isInBox = false;
    public FruitType Type => type;

    public bool IsInBox => isInBox;

    private void OnCollisionEnter2D(Collision2D collision) {
        Fruit otherFruit = collision.gameObject.GetComponent<Fruit>();
        if (otherFruit == null) return;

        if (this.type == otherFruit.Type && transform.position.y > otherFruit.transform.position.y) {
            Vector3 spawnPos = collision.contacts[0].point;

            var mergeInfo = new OnFruitMerge {
                Type = this.type,
                SpawnPosition = spawnPos,
                FruitA = this,
                FruitB = otherFruit
            };

            EventDispatcher.Dispatch(mergeInfo);
        }
    }

    public void SetFruitInBox(bool isInBox) {
        this.isInBox = isInBox;
    }
}


public enum FruitType : int {
    Cherry = 0,
    Blueberry = 1,
    Lemon = 2,
    Orange = 3,
    Watermelon = 4,
    Length = 5
}