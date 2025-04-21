using UnityEngine;

public class SpawnBarController : MonoBehaviour {
    public float moveSpeed = 5f;
    public float limitX = 2.5f;

    private Camera cam;
    bool isDragging;
    public Vector3 CurrentPosition => transform.position;

    void Start() {
        cam = Camera.main;
    }

    void Update() {
        if (UniversalInput.GetInputDown(out Vector2 _)) StatDragging();
        if (UniversalInput.GetInputUp(out Vector2 _)) StopDragging();
        if (isDragging)
            MoveWithInput();
    }
    void StatDragging() => isDragging = true;
    void StopDragging() => isDragging = false;
    void MoveWithInput() {
        if (cam == null) return;

        Vector3 mousePos = Input.mousePosition;

        // Kiểm tra input hợp lệ
        if (float.IsNaN(mousePos.x) || float.IsNaN(mousePos.y) || float.IsInfinity(mousePos.x) || float.IsInfinity(mousePos.y)) {
            return;
        }

        // Đặt z để đảm bảo convert đúng
        mousePos.z = Mathf.Abs(cam.transform.position.z); // camera đang ở z = -10 chẳng hạn

        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);

        // Clamp vị trí X trong giới hạn
        float clampedX = Mathf.Clamp(worldPos.x, -limitX, limitX);

        // Lerp mượt mà theo thời gian
        Vector3 targetPos = new Vector3(clampedX, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);
    }
}
