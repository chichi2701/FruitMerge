using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraResizer2D : MonoBehaviour {
    [Header("Design Setting")]
    public float defaultOrthographicSize = 5f;           // Chiều cao bạn dùng để thiết kế
    public float designAspectRatio = 9f / 16f;           // Tỷ lệ thiết kế gốc (ví dụ 9:16 cho dọc, 16:9 cho ngang)

    private Camera cam;

    void Awake() {
        cam = GetComponent<Camera>();
        ResizeCamera();
    }

    void ResizeCamera() {
        float screenAspect = (float)Screen.width / Screen.height;

        // Giữ nguyên chiều cao (orthographicSize gốc), chỉ scale theo chiều ngang
        float adjustedSize = defaultOrthographicSize;

        if (screenAspect < designAspectRatio) {
            // Màn hình hẹp hơn (ví dụ iPad 4:3), cần tăng chiều cao để nội dung không bị cắt
            float scaleFactor = designAspectRatio / screenAspect;
            adjustedSize *= scaleFactor;
        }

        cam.orthographicSize = adjustedSize;
    }
}
