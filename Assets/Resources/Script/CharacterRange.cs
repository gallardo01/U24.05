using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// Lớp CharacterRange kế thừa từ MonoBehaviour
public class CharacterRange : MonoBehaviour
{
    // Danh sách các bot trong tầm bắn
    public List<Character> botInRange = new List<Character>();
    // Mục tiêu hiện tại
    public Bot currentTarget = null;

    // Hàm RemoveNullTarget() để loại bỏ các mục tiêu null khỏi danh sách botInRange
    public void RemoveNullTarget()
    {
        for (int i = botInRange.Count - 1; i >= 0; i--)
        {
            // Kiểm tra nếu bot là null hoặc không có tag "Bot"
            if (botInRange[i] == null || !botInRange[i].CompareTag("Bot"))
            {
                // Loại bỏ bot khỏi danh sách
                botInRange.RemoveAt(i);
            }
        }
    }

    // Hàm GetNearestTarget() để lấy mục tiêu gần nhất
    public Transform GetNearestTarget()
    {
        // Kiểm tra nếu danh sách botInRange rỗng
        if (botInRange.Count == 0)
        {
            return null;
        }
        else
        {
            // Khởi tạo khoảng cách nhỏ nhất
            float distanceMin = float.MaxValue;
            int index = 0;
            for (int i = 0; i < botInRange.Count; i++)
            {
                // Bỏ qua nếu bot là null
                if (botInRange[i] == null) continue;
                // Tính toán khoảng cách từ nhân vật đến bot
                float distance = (transform.position - botInRange[i].transform.position).sqrMagnitude;
                // Cập nhật khoảng cách nhỏ nhất và chỉ số của bot gần nhất
                if (distance < distanceMin)
                {
                    distanceMin = distance;
                    index = i;
                }
            }
            // Trả về Transform của bot gần nhất
            return botInRange[index]?.transform;
        }
    }

    // Hàm SetNearestTarget() để thiết lập mục tiêu gần nhất
    public void SetNearestTarget()
    {
        // Loại bỏ các mục tiêu null khỏi danh sách botInRange
        RemoveNullTarget();
        // Kiểm tra nếu danh sách botInRange không rỗng
        if (botInRange.Count > 0)
        {
            // Lấy Transform của mục tiêu gần nhất
            Transform nearestTargetTransform = GetNearestTarget();
            // Kiểm tra nếu Transform của mục tiêu không null
            if (nearestTargetTransform == null) return;
            // Lấy Bot từ Transform của mục tiêu
            Bot nearestBot = nearestTargetTransform.GetComponent<Bot>();
            // Kiểm tra nếu Bot không null
            if (nearestBot != null)
            {
                // Kiểm tra nếu currentTarget không null và khác với nearestBot
                if (currentTarget != null && currentTarget != nearestBot)
                {
                    // Loại bỏ chỉ thị mục tiêu từ currentTarget cũ
                    currentTarget.RemoveTarget();
                }
                // Cập nhật currentTarget thành nearestBot
                currentTarget = nearestBot;
                // Hiển thị chỉ thị mục tiêu trên currentTarget mới
                currentTarget.SetTarget();
            }
        }
    }

    // Hàm OnTriggerEnter() được gọi khi Collider khác đi vào trigger
    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra nếu đối tượng va chạm có tag là "Bot"
        if (other.gameObject.CompareTag("Bot"))
        {
            // Lấy Bot từ đối tượng va chạm
            Bot bot = other.GetComponent<Bot>();
            // Kiểm tra nếu Bot không null
            if (bot != null)
            {
                // Hiển thị chỉ thị mục tiêu
                bot.SetTarget();
            }
            // Thêm bot vào danh sách botInRange
            botInRange.Add(other.GetComponent<Character>());
            // Thiết lập mục tiêu gần nhất
            SetNearestTarget();
        }
    }

    // Hàm OnTriggerExit() được gọi khi Collider khác rời khỏi trigger
    private void OnTriggerExit(Collider other)
    {
        // Kiểm tra nếu đối tượng va chạm có tag là "Bot"
        if (other.gameObject.CompareTag("Bot"))
        {
            // Lấy Bot từ đối tượng va chạm
            Bot bot = other.GetComponent<Bot>();
            // Kiểm tra nếu Bot không null
            if (bot != null)
            {
                // Kiểm tra nếu bot là currentTarget
                if (bot == currentTarget)
                {
                    // Đặt currentTarget thành null
                    currentTarget = null;
                    // Thiết lập mục tiêu gần nhất
                    SetNearestTarget();
                    // Vô hiệu hóa chỉ thị mục tiêu
                    bot.targetCircle.SetActive(false);
                }
            }
            // Loại bỏ bot khỏi danh sách botInRange
            botInRange.Remove(other.GetComponent<Character>());
        }
    }
}