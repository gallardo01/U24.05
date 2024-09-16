using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Lớp Character kế thừa từ AbstractCharacter
public class Character : AbstractCharacter
{
    // Tham chiếu đến Transform của body
    public Transform body;
    // Tốc độ di chuyển của nhân vật
    public float speed = 5.0f;
    // Tham chiếu đến Animator của nhân vật
    public Animator animator;
    // Tên animation hiện tại
    private string currentAnim = "idle";
    // Tham chiếu đến CharacterRange
    public CharacterRange characterRange;
    // Prefab của đạn
    private Bullet bulletPrefab;
    // Trạng thái tấn công của nhân vật
    public bool isAttack = false;
    // Tham chiếu đến TargetIndicator
    public TargetIndicator indicator;
    // Cấp độ của nhân vật
    public int level = 1;
    // Trạng thái chết của nhân vật
    public bool isDead = false;
    // Tham chiếu đến InitSkin
    public InitSkin skin;
    // Tham chiếu đến Transform của indicator
    public Transform indicatorPoint;

    // Hàm OnInit() được gọi khi khởi tạo nhân vật
    public override void OnInit()
    {
        // Đặt cấp độ ban đầu
        level = 1;
        // Thiết lập kích thước của body
        SetBodyScale();
        // Khởi tạo indicator với cấp độ hiện tại
        indicator.InitTarget(level);
        // Lấy bulletPrefab từ ItemDatabase dựa trên weaponsId của skin
        bulletPrefab = ItemDatabase.Ins.bullets[skin.weaponsId];
    }

    // Hàm OnAttack() được gọi khi nhân vật tấn công
    public override void OnAttack()
    {
        // Gọi hàm Throw để bắn đạn
        Throw();
    }

    // Hàm OnDeath() được gọi khi nhân vật chết
    public override void OnDeath()
    {
        // Đặt trạng thái chết
        isDead = true;
        // Gọi hàm CharacterDead() từ GameController
        GameController.Ins.CharacterDead();
        // Thay đổi animation thành "death"
        ChangeAnim("death");
        // Đặt tag của gameObject thành "Untagged"
        gameObject.tag = "Untagged";
    }

    // Hàm SetBodyScale() để thiết lập kích thước của body dựa trên cấp độ
    private void SetBodyScale()
    {
        // Tính toán kích thước mới và đặt cho body
        body.localScale = (1 + 0.1f * (level - 1)) * Vector3.one;
    }

    // Hàm GainLevel() để tăng cấp độ của nhân vật
    public void GainLevel()
    {
        // Kiểm tra nếu nhân vật chưa chết
        if (!isDead)
        {
            // Tăng cấp độ
            level++;
            // Thiết lập lại kích thước của body
            SetBodyScale();
            // Khởi tạo lại indicator với cấp độ mới
            indicator.InitTarget(level);
        }
    }

    // Hàm Throw() để bắn đạn
    public void Throw()
    {
        // Loại bỏ các mục tiêu null khỏi characterRange
        characterRange.RemoveNullTarget();
        // Kiểm tra nếu có bot trong tầm bắn
        if (characterRange.botInRange.Count > 0)
        {
            // Lấy mục tiêu gần nhất
            Transform nearestTarget = characterRange.GetNearestTarget();
            // Kiểm tra nếu mục tiêu không null
            if (nearestTarget != null)
            {
                // Vô hiệu hóa weaponItem của skin
                skin.weaponItem.SetActive(false);
                // Tạo một viên đạn mới từ bulletPrefab
                Bullet bullet = Instantiate(bulletPrefab);
                // Đặt vị trí của viên đạn
                bullet.transform.position = transform.position;
                // Gán self cho viên đạn
                bullet.self = this;
                // Tính toán hướng bắn
                Vector3 direction = (nearestTarget.position - transform.position).normalized;
                // Đặt hướng của viên đạn
                bullet.transform.forward = direction;
                // Thêm lực cho viên đạn
                bullet.GetComponent<Rigidbody>().AddForce(300f * direction);
                // Đặt hướng của nhân vật
                transform.forward = direction;
                // Gọi hàm EnableWeapons() sau 1 giây
                Invoke(nameof(EnableWeapons), 1f);
            }
        }
    }

    // Hàm EnableWeapons() để kích hoạt lại weaponItem của skin
    private void EnableWeapons()
    {
        skin.weaponItem.SetActive(true);
    }

    // Hàm ChangeAnim() để thay đổi animation của nhân vật
    public void ChangeAnim(string animName)
    {
        // Kiểm tra nếu animation hiện tại khác với animation mới
        if (currentAnim != animName)
        {
            // Reset trigger của animation hiện tại
            animator.ResetTrigger(animName);
            // Đặt animation hiện tại thành animation mới
            currentAnim = animName;
            // Kích hoạt trigger của animation mới
            animator.SetTrigger(currentAnim);
        }
    }
}
   