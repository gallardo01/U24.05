using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Tham chiếu đến đối tượng Character đã bắn viên đạn này
    public Character self;

    // Start is called before the first frame update
    // Hàm Start() được gọi khi đối tượng được khởi tạo
    void Start()
    {
        // Hủy đối tượng đạn sau 1 giây cộng thêm 0.1 giây nhân với cấp độ của Character
        Destroy(gameObject, 1f + (0.1f * self.level));
    }

    // OnTriggerEnter() được gọi khi đạn va chạm với một Collider khác
    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra nếu đối tượng va chạm có tag là "Bot" và không phải là Character đã bắn đạn
        if (other.gameObject.CompareTag("Bot") && other.GetComponent<Character>() != self)
        {
            // Gọi phương thức OnDeath() của đối tượng va chạm
            other.GetComponent<Character>().OnDeath();
            // Tăng cấp độ của Character đã bắn đạn
            self.GainLevel();
            // Hủy đối tượng đạn
            Destroy(gameObject);
        }
    }
}