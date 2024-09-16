using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lớp CameraFollower kế thừa từ Singleton<CameraFollower>
public class CameraFollower : Singleton<CameraFollower>
{
    // Tham chiếu đến Transform của camera
    public Transform TF;
    // Tham chiếu đến Transform của người chơi
    public Transform playerTF;
    // Tham chiếu đến Camera của game
    public Camera gameCamera;
    // Các biến offset cho các trạng thái khác nhau
    [SerializeField] Vector3 offset;
    [SerializeField] Vector3 offsetMainMenu;
    [SerializeField] Vector3 offsetShop;

    // Các biến rotation cho các trạng thái khác nhau
    [SerializeField] Quaternion rotation;
    [SerializeField] Quaternion rotationMainMenu;

    // Biến lưu trữ offset và rotation hiện tại
    private Vector3 currentOffset;
    private Quaternion currentRotation;

    // Hàm Start() được gọi khi đối tượng được khởi tạo
    private void Start()
    {
        // Lấy component Camera từ đối tượng
        gameCamera = GetComponent<Camera>();
        // Thay đổi trạng thái camera ban đầu
        ChangeState(1);
    }

    // Hàm LateUpdate() được gọi mỗi frame sau khi tất cả các Update() đã được gọi
    private void LateUpdate()
    {
        // Kiểm tra nếu playerTF không null
        if (playerTF != null)
        {
            // Tính toán hệ số lerp
            float lerpFactor = Time.deltaTime * 5f;
            // Kiểm tra nếu hệ số lerp hợp lệ
            if (lerpFactor > 0 && lerpFactor <= 1)
            {
                // Lerp vị trí và rotation của camera
                TF.position = Vector3.Lerp(TF.position, playerTF.position + currentOffset, lerpFactor);
                TF.rotation = Quaternion.Lerp(TF.rotation, currentRotation, lerpFactor);
            }
        }
    }

    // Hàm SetCameraSize() để thiết lập kích thước camera
    public void SetCameraSize(float size)
    {
        // Kiểm tra nếu camera là orthographic
        if (gameCamera.orthographic)
        {
            // Thiết lập orthographicSize
            gameCamera.orthographicSize = size;
        }
        else
        {
            // Thiết lập fieldOfView
            gameCamera.fieldOfView = size;
        }
    }

    // Hàm ChangeState() để thay đổi trạng thái của camera
    public void ChangeState(int state)
    {
        // Kiểm tra trạng thái và thiết lập offset, rotation và kích thước camera tương ứng
        if (state == 1)
        {
            currentOffset = offsetMainMenu;
            currentRotation = rotationMainMenu;
            SetCameraSize(2);
        }
        else if (state == 2)
        {
            currentOffset = offset;
            currentRotation = rotation;
            SetCameraSize(10);
        }
        else if (state == 3)
        {
            currentOffset = offsetShop;
            SetCameraSize(2.42f);
        }
    }
}