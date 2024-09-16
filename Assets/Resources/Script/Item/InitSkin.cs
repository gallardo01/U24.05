using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// Lớp InitSkin để khởi tạo và trang bị các vật phẩm cho nhân vật
public class InitSkin : MonoBehaviour
{
    // Tham chiếu đến Transform của vũ khí
    [SerializeField] Transform weapons;
    // Tham chiếu đến Transform của khiên
    [SerializeField] Transform shield;
    // Tham chiếu đến Transform của mũ
    [SerializeField] Transform head;
    // Tham chiếu đến SkinnedMeshRenderer của quần
    [SerializeField] SkinnedMeshRenderer pants;
    // ID của vũ khí
    public int weaponsId = 0;
    // Đối tượng vũ khí
    public GameObject weaponItem;

    // Hàm Start() được gọi khi đối tượng được khởi tạo
    void Start()
    {
    }

    // Hàm RandomEquipItem() để trang bị ngẫu nhiên các vật phẩm
    public void RandomEquipItem()
    {
        // Khởi tạo vũ khí ngẫu nhiên
        InitWeapon(Random.Range(0, ItemDatabase.Ins.weapons.Count));
        // Khởi tạo khiên ngẫu nhiên
        InitShield(Random.Range(0, ItemDatabase.Ins.shields.Count));
        // Khởi tạo mũ ngẫu nhiên
        InitHead(Random.Range(0, ItemDatabase.Ins.heads.Count));
        // Khởi tạo quần ngẫu nhiên
        InitPants(Random.Range(0, ItemDatabase.Ins.pants.Count));
    }

    // Hàm PlayerEquipItem() để trang bị các vật phẩm cho người chơi
    public void PlayerEquipItem()
    {
        // Xóa các vật phẩm cũ

        foreach (Transform child in weapons)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in shield)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in head)
        {
            Destroy(child.gameObject);
        }

        // Lấy ID của vũ khí từ ItemJsonDatabase và khởi tạo vũ khí nếu ID hợp lệ
        int idWeapon = ItemJsonDatabase.Ins.GetIdOfItemsEquiped("Weapons");
        if (idWeapon > 0)
        {
            InitWeapon(idWeapon - 1);
        }
        // Lấy ID của khiên từ ItemJsonDatabase và khởi tạo khiên nếu ID hợp lệ
        int idShield = ItemJsonDatabase.Ins.GetIdOfItemsEquiped("Shield");
        if (idShield > 0)
        {
            InitShield(idShield - 1);
        }
        // Lấy ID của mũ từ ItemJsonDatabase và khởi tạo mũ nếu ID hợp lệ
        int idHat = ItemJsonDatabase.Ins.GetIdOfItemsEquiped("Hat");
        if (idHat > 0)
        {
            InitHead(idHat - 1);
        }
        // Lấy ID của quần từ ItemJsonDatabase và khởi tạo quần nếu ID hợp lệ
        int idPants = ItemJsonDatabase.Ins.GetIdOfItemsEquiped("Pants");
        if (idPants > 0)
        {
            InitPants(idPants - 1);
        }
    }

    // Hàm InitWeapon() để khởi tạo vũ khí
    public void InitWeapon(int index)
    {
        // Gán ID của vũ khí
        weaponsId = index;
        // Lấy đối tượng vũ khí từ ItemDatabase
        GameObject weapon = ItemDatabase.Ins.GetWeapon(index);
        // Khởi tạo vũ khí và gán vào Transform của weapons
        weaponItem = Instantiate(weapon, weapons);
    }

    // Hàm InitShield() để khởi tạo khiên
    public void InitShield(int index)
    {
        // Lấy đối tượng khiên từ ItemDatabase
        GameObject shield = ItemDatabase.Ins.GetShield(index);
        // Khởi tạo khiên và gán vào Transform của shield
        Instantiate(shield, this.shield);
    }

    // Hàm InitHead() để khởi tạo mũ
    public void InitHead(int index)
    {
        // Lấy đối tượng mũ từ ItemDatabase
        GameObject head = ItemDatabase.Ins.GetHead(index);
        // Khởi tạo mũ và gán vào Transform của head
        Instantiate(head, this.head);
    }

    // Hàm InitPants() để khởi tạo quần
    public void InitPants(int index)
    {
        // Lấy vật liệu của quần từ ItemDatabase
        Material pants = ItemDatabase.Ins.GetPants(index);
        // Gán vật liệu cho SkinnedMeshRenderer của quần
        this.pants.material = pants;
    }
}