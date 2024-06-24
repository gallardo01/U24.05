using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StageController : MonoBehaviour
{
    [SerializeField] List<Transform> brickTFs = new List<Transform>();
    [SerializeField] Brick brickPrefab;

    private List<int> brickTFsIndexReady = new List<int>();
    private List<int> brickTFsIndexUsed = new List<int>();

    private List<Brick> bricks = new List<Brick>();

    private int[] totalBricksEachColor; 

    // Start is called before the first frame update
    void Start()
    {
        InitBricks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitBricks()
    {
        totalBricksEachColor = new int[LevelManager.Ins.NumberColors];

        for (int i = 0; i < LevelManager.Ins.NumberColors; i++)
        {
            totalBricksEachColor[i] = LevelManager.Ins.NumberBricksEachColor;
            Debug.Log(totalBricksEachColor[i]);
        }

        for (int i = 0; i < brickTFs.Count; i++)
        {
            Brick brick = Instantiate(brickPrefab, brickTFs[i]);
            bricks.Add(brick);

            while (true)
            {
                int index = Random.Range(0, LevelManager.Ins.NumberColors);
                if (totalBricksEachColor[index] > 0)
                {
                    totalBricksEachColor[index]--;
                    brick.SetBrickColor(ColorController.Ins.colorsIndexUsed[index]);
                    break;
                }
            }

        }
    }
}
