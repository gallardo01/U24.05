using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Lớp Player kế thừa từ Character
public class Player : Character
{
    // Đối tượng CounterTime để quản lý thời gian đếm ngược
    private CounterTime counter = new CounterTime();

    // Hàm Start() được gọi khi đối tượng được khởi tạo
    void Start()
    {
    }

    // Hàm Update() được gọi mỗi frame
    void Update()
    {
        // Lấy hướng di chuyển từ JoystickControl
        Vector3 direction = JoystickControl.direct;
        direction = direction.normalized;

        // Kiểm tra nếu có hướng di chuyển
        if (direction != Vector3.zero)
        {
            // Hủy đếm ngược
            counter.Cancel();
            // Xoay body theo hướng di chuyển
            body.rotation = Quaternion.LookRotation(direction);
            // Di chuyển body theo hướng di chuyển
            body.Translate(direction * speed * Time.deltaTime, Space.World);
            // Thay đổi animation thành "run"
            ChangeAnim("run");
        }
        else if (!isAttack)
        {
            // Thực thi đếm ngược
            counter.Excute();
            // Thay đổi animation thành "idle"
            ChangeAnim("idle");
            // Loại bỏ các mục tiêu null khỏi characterRange
            characterRange.RemoveNullTarget();
            // Kiểm tra nếu có bot trong tầm bắn
            if (characterRange.botInRange.Count > 0)
            {
                // Tấn công mục tiêu
                AttackTarget();
            }
        }
        else
        {
            // Thực thi đếm ngược
            counter.Excute();
        }
    }

    // Hàm AttackTarget() để tấn công mục tiêu
    public void AttackTarget()
    {
        // Đặt trạng thái tấn công
        isAttack = true;
        // Gọi hàm ChangeIsAttack() sau 1.5 giây
        Invoke("ChangeIsAttack", 1.5f);
        // Thay đổi animation thành "attack"
        ChangeAnim("attack");
        // Bắt đầu đếm ngược để thực thi hành động tấn công
        counter.Start(OnAttack, 0.5f);
    }

    // Hàm ChangeIsAttack() để thay đổi trạng thái tấn công
    private void ChangeIsAttack()
    {
        isAttack = false;
    }

    // Hàm OnDeath() được gọi khi nhân vật chết
    public override void OnDeath()
    {
        // Hủy đếm ngược
        counter.Cancel();
        // Kết thúc trò chơi
        GameController.Ins.EndGame();
        // Vô hiệu hóa script này
        this.enabled = false;
        // Mở giao diện nhận thưởng
        UIManager.Ins.OpenAwardUI(level);
        // Dừng tất cả các bot
        foreach (Bot bot in GameController.Ins.bots)
        {
            bot.Stop();
        }
        // Gọi hàm OnDeath() của lớp cha
        base.OnDeath();
    }

    // Hàm OnAttack() được gọi khi nhân vật tấn công
    public override void OnAttack()
    {
        // Gọi hàm OnAttack() của lớp cha
        base.OnAttack();
    }

    // Hàm OnInit() được gọi khi khởi tạo nhân vật
    public override void OnInit()
    {
        // Đặt trạng thái chết thành false
        isDead = false;
        // Gọi hàm OnInit() của lớp cha
        base.OnInit();
        // Trang bị vật phẩm cho người chơi
        skin.PlayerEquipItem();
        // Thay đổi animation thành "idle"
        ChangeAnim("idle");
        // Đặt tag của gameObject thành "Bot"
        gameObject.tag = "Bot";
        // Khởi tạo indicator cho người chơi
        indicator.InitTarget(Color.black, 1, "Player");
        // Kích hoạt script này
        this.enabled = true;
    }

    // Hàm OnDespawn() để hủy nhân vật
    public void OnDespawn()
    {
        // Hủy đếm ngược
        counter.Cancel();
    }
}