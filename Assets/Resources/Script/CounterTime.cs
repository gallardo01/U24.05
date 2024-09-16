using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



// Lớp CounterTime để quản lý thời gian đếm ngược và thực thi hành động khi hết thời gian
public class CounterTime
{
    // Hành động của người chơi sẽ được gọi khi hết thời gian
    private UnityAction playerAction;
    // Thời gian đếm ngược
    private float time;

    // Hàm Start() để khởi tạo hành động và thời gian đếm ngược
    public void Start(UnityAction playerAction, float time)
    {
        // Gán hành động của người chơi
        this.playerAction = playerAction;
        // Gán thời gian đếm ngược
        this.time = time;
    }

    // Hàm Excute() để thực thi đếm ngược thời gian
    public void Excute()
    {
        // Kiểm tra nếu thời gian còn lại lớn hơn 0
        if (time > 0)
        {
            // Giảm thời gian theo thời gian trôi qua mỗi frame
            time -= Time.deltaTime;
            // Kiểm tra nếu thời gian đã hết
            if (time <= 0)
            {
                // Gọi hàm Exit() để thực thi hành động của người chơi
                Exit();
            }
        }
    }

    // Hàm Exit() để thực thi hành động của người chơi khi hết thời gian
    public void Exit()
    {
        // Gọi hành động của người chơi nếu không null
        playerAction?.Invoke();
    }

    // Hàm Cancel() để hủy hành động của người chơi
    public void Cancel()
    {
        // Đặt hành động của người chơi thành null
        playerAction = null;
    }
}