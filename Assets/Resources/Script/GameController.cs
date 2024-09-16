using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Lớp GameController kế thừa từ Singleton<GameController>
public class GameController : Singleton<GameController>
{
    // Tham chiếu đến Canvas của indicator
    public Canvas indicatorCanvas;
    // Tham chiếu đến prefab Bot
    public Bot Bot;
    // Số lượng bot
    public int botNumber = 10;
    // Tham chiếu đến người chơi
    public Player player;
    // Tham chiếu đến prefab TargetIndicator
    public TargetIndicator indicator;
    // Tham chiếu đến TextMeshProUGUI để hiển thị số lượng nhân vật còn sống
    public TextMeshProUGUI aliveText;
    // Tổng số nhân vật
    private int totalCharacter;
    // Danh sách các bot
    public List<Bot> bots;
    // Số lượng vàng
    public int gold;

    // Danh sách các điểm spawn
    public List<Transform> listSpawn = new List<Transform>();

    // Hàm Start() được gọi khi đối tượng được khởi tạo
    void Start()
    {
        // Thiết lập tốc độ thời gian
        Time.timeScale = 1.1f;
        // Tính tổng số nhân vật
        totalCharacter = botNumber + 1;
        // Kiểm tra và điều chỉnh số lượng bot nếu cần
        if (botNumber > listSpawn.Count - 1)
        {
            botNumber = listSpawn.Count - 1;
        }
        // Khởi tạo văn bản hiển thị số lượng nhân vật còn sống
        InitTextAlive();
        // Vô hiệu hóa JoystickControl
        JoystickControl.instance.gameObject.SetActive(false);
        // Khởi tạo indicator cho người chơi
        TargetIndicator playerIndicator = Instantiate(indicator, indicatorCanvas.transform);
        player.indicator = playerIndicator;
        playerIndicator.character = player;
        // Tạo bot cho trò chơi mới
        CreateBotNewGame();
        // Khởi tạo vàng
        InitGold();
    }

    // Hàm InitGold() để khởi tạo vàng
    public void InitGold()
    {
        // Kiểm tra nếu chưa có khóa "Gold" trong PlayerPrefs
        if (!PlayerPrefs.HasKey("Gold"))
        {
            gold = 0;
            PlayerPrefs.SetInt("Gold", 0);
        }
        else
        {
            gold = PlayerPrefs.GetInt("Gold");
        }
    }

    // Hàm GainGold() để tăng số lượng vàng
    public void GainGold(int number)
    {
        gold += number;
        PlayerPrefs.SetInt("Gold", gold);
        UIManager.Ins.InitGold();
    }

    // Hàm ReduceGold() để giảm số lượng vàng
    public void ReduceGold(int number)
    {
        gold -= number;
        PlayerPrefs.SetInt("Gold", gold);
        UIManager.Ins.InitGold();
    }

    // Hàm InitPlayerItems() để khởi tạo các vật phẩm của người chơi
    public void InitPlayerItems()
    {
        player.skin.PlayerEquipItem();
    }

    // Hàm StartGame() để bắt đầu trò chơi
    public void StartGame()
    {
        foreach (Bot bot in bots)
        {
            bot.OnInit();
        }
    }

    // Hàm PlayAgain() để chơi lại trò chơi
    public void PlayAgain()
    {
        botNumber = Random.Range(1, 20);

        // Cập nhật tổng số nhân vật và văn bản hiển thị số lượng nhân vật còn sống
        totalCharacter = botNumber + 1;
        aliveText.text = "Alive: " + totalCharacter;

        // Hủy tất cả các bot và indicator của chúng
        DeleteAllBots();
        Destroy(player.indicator.gameObject);

        // Xóa danh sách bot
        player.OnDespawn();
        CreateBotNewGame();

        // Khởi tạo lại indicator cho người chơi
        TargetIndicator playerIndicator = Instantiate(indicator, indicatorCanvas.transform);
        player.indicator = playerIndicator;
        playerIndicator.character = player;
        player.indicator.InitTarget(Color.black, 1, "Player");

        // Reset UI
        UIManager.Ins.ResetUI();
    }

    // Hàm ReplyGame() để chơi lại trò chơi
    public void ReplyGame()
    {
        DeleteAllBots();
        player.OnDespawn();
        CreateBotNewGame();
    }

    // Hàm DeleteAllBots() để xóa tất cả các bot
    public void DeleteAllBots()
    {
        foreach (Bot bot in bots)
        {
            Destroy(bot.indicator.gameObject);
            Destroy(bot.gameObject);
        }
        bots.Clear();
    }

    // Hàm EndGame() để kết thúc trò chơi
    public void EndGame()
    {
        JoystickControl.direct = Vector3.zero;
        JoystickControl.instance.gameObject.SetActive(false);
    }

    // Hàm InitTextAlive() để khởi tạo văn bản hiển thị số lượng nhân vật còn sống
    public void InitTextAlive()
    {
        aliveText.text = "Alive: " + totalCharacter;
    }

    // Hàm CharacterDead() được gọi khi một nhân vật chết
    public void CharacterDead()
    {
        totalCharacter--;
        aliveText.text = "Alive: " + totalCharacter;
        if (totalCharacter == 1)
        {
            UIManager.Ins.OpenAwardUI(player.level);
        }
    }

    // Hàm CreateBotNewGame() để tạo bot cho trò chơi mới
    public void CreateBotNewGame()
    {
        player.transform.position = listSpawn[listSpawn.Count - 1].position;
        player.OnInit();
        List<Transform> spawnPoints = new List<Transform>(listSpawn); // Sao chép danh sách các điểm spawn
        spawnPoints.RemoveAt(spawnPoints.Count - 1); // Loại bỏ điểm spawn của người chơi

        for (int i = 0; i < botNumber; i++)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Count); // Chọn một điểm spawn ngẫu nhiên
            Bot bot = Instantiate(Bot);
            bot.transform.position = spawnPoints[spawnIndex].position;
            spawnPoints.RemoveAt(spawnIndex); // Loại bỏ điểm spawn đã sử dụng

            TargetIndicator botIndicator = Instantiate(indicator, indicatorCanvas.transform);
            botIndicator.character = bot;
            bot.indicator = botIndicator;
            Color color = UnityEngine.Random.ColorHSV();
            string botName = Constant.PlayerName[Random.Range(0, Constant.PlayerName.Length)];
            botIndicator.InitTarget(color, 1, botName);
            bots.Add(bot);
            bot.targetCircle.SetActive(false);
        }
    }
}

