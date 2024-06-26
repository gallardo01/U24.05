using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : Singleton<StageController>
{
    [SerializeField] List<Transform> listBricksTransform = new List<Transform>();
    [SerializeField] GameObject brickPrefab;

    private List<Brick> listBricks = new List<Brick>();
    // Start Game - Pick mau`
    public List<int> gameColors = new List<int>();
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        // Sinh ra 50 gach
        OnInit();
        // Chon 5 mau se xuat hien trong game
        RandomGameColor();
        // Sinh ra 50 vien gach, moi mau 10 vien
        InitBricksColor();
        // Set up mau cho nhan vat
        SetUpPlayerColor();
    }

    public void CreateNewBrick(int position)
    {
        StartCoroutine(CreateNewBrickAfterDelayTime(position));
    }

    IEnumerator CreateNewBrickAfterDelayTime(int position)
    {
        yield return new WaitForSeconds(5f);
        Brick brick = Instantiate(brickPrefab, listBricksTransform[position].transform).GetComponent<Brick>();
        brick.transform.localPosition = Vector3.zero;
        brick.SetBrickPosition(position);
        brick.SetBrickColor(gameColors[Random.Range(0,5)]);
        listBricks.Add(brick);
    }

    private void SetUpPlayerColor()
    {
        player.SetPlayerColor(gameColors[0]);
    }
    private void RandomGameColor()
    {
        // Chon 5 so trong 0 1 2 3 4 5 6 7 8 9
        for (int i = 0; i < 5; i++) 
        {
            int randColor;
            while (true)
            {
                randColor = Random.Range(0, 10);
                bool sameColor = false;
                for (int j = 0; j < gameColors.Count; j++)
                {
                    if(randColor == gameColors[j])
                    {
                        sameColor = true;
                        break;
                    }
                }
                if (!sameColor)
                {
                    break;
                }
            }
            gameColors.Add(randColor);
        }
    }

    private void OnInit()
    {
        for (int i = 0; i < listBricksTransform.Count; i++)
        {
            Brick brick = Instantiate(brickPrefab, listBricksTransform[i].transform).GetComponent<Brick>();
            brick.transform.localPosition = Vector3.zero;
            brick.SetBrickPosition(i);
            listBricks.Add(brick);
        }
    }

    private void InitBricksColor()
    {
        // Dem' moi mau` co 10 vien
        int[] totalBricksColor = { 10, 10, 10, 10, 10 };
        // 50 vien gach, for tung` vien -> chon mau`
        for (int i = 0; i < listBricks.Count; i++)
        {
            // Chon mau`
            while (true)
            {
                // Chon 1 mau ngau~ nhien
                int brickColor = Random.Range(0, 5);
                // Kiem tra
                if (totalBricksColor[brickColor] > 0)
                {
                    totalBricksColor[brickColor]--;
                    listBricks[i].SetBrickColor(gameColors[brickColor]);
                    break;
                } 
            }
        }
    }
}
