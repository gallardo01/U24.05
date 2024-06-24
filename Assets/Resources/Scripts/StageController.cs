using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] List<Transform> listBricksTranform = new List<Transform>();    
    [SerializeField] Brick brickPrefabs;

    public List<int> gameColors = new List<int>();
    public Player player;

    private List<Brick> listBricks = new List<Brick>();  

    // Start is called before the first frame update
    void Start()
    {
        OnInit();   
        RandomGameColor();  
        InitBrickColor();   
        SetUpPlayerColor(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnInit()
    {
        for (int i = 0; i < listBricksTranform.Count; i++)
        {
            Brick brick = Instantiate(brickPrefabs, listBricksTranform[i].transform);
            //colorIndex = Random.Range(0, 10);
            //brickColor.material = ColorController.Instance.GetMaterialColor(colorIndex); 
            brick.transform.localPosition = Vector3.zero;
            listBricks.Add(brick);
        }
    }

    private void SetUpPlayerColor()
    {
        player.SetPlayerColor(gameColors[0]);
    }

    private void RandomGameColor()
    {
        for(int i = 0; i < 5; i++)
        {
            int randColor;
            while (true)
            {
                randColor = Random.Range(0, 10);
                bool sameColor = false;
                for(int j = 0; j < gameColors.Count; j++)
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

    private void InitBrickColor()
    {
        int[] totalBrickColor = { 6, 6, 6, 6, 6 };
        for(int i = 0; i < listBricks.Count; i++)
        {
            while (true)
            {
                int brickColor = Random.Range(0, 5);

                if (totalBrickColor[brickColor] > 0)
                {
                    totalBrickColor[brickColor]--;
                    listBricks[i].SetBrickColor(gameColors[brickColor]);
                    break;
                }
            }
        }
    }
}
