using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class StageController : MonoBehaviour
{
    [SerializeField] Brick brickPrefab;
    [SerializeField] int rows = 10;
    [SerializeField] int columns = 6;
    [SerializeField] float spacing = 4f;
    List<GameObject> dots = new List<GameObject>();
    List<int> brickNumber = new List<int>();
    List<Brick> brickList = new List<Brick>();
    List<int> colorsOnStage = new List<int>();

    void Awake()
    {
        SpawnDot();
    }

    private void SpawnDot()
    {
        for (int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                GameObject dot = Instantiate(new GameObject("Dot"), this.transform);
                dot.transform.localPosition = new Vector3(i * spacing, 0.5f, j * spacing);
                dot.transform.localRotation = Quaternion.identity;
                dots.Add(dot);
            }
        }
        for(int i = 0; i < dots.Count; i++)
        {
            brickNumber.Add(i);
        }
    }

    public void CharacterStartStage(int characterColor)
    {
        if (!colorsOnStage.Contains(characterColor))
        {
            colorsOnStage.Add(characterColor);
            SpawnCharacterBricks(characterColor);
        }
    }

    private void SpawnCharacterBricks(int color)
    {
        Debug.Log("work");
        for(int i = 0;i < 10; i++)
        {
            int random = Random.Range(0, brickNumber.Count);
            int bricknumber = brickNumber[random];
            brickNumber.Remove(bricknumber);

            Brick newBrick = Instantiate(brickPrefab, dots[bricknumber].transform);
            newBrick.transform.localPosition = Vector3.zero;
            newBrick.transform.localRotation = Quaternion.Euler(0, 90, 0);
            newBrick.SetStage(this);
            newBrick.SetBrickNumber(bricknumber);
            newBrick.SetBrickColor(color);

            brickList.Add(newBrick);
        }
    }

    public void SpawnNewBrick(int brickNumber)
    {
        StartCoroutine(SpawnBrickSequence(brickNumber));
    }

    IEnumerator SpawnBrickSequence(int brickNumber)
    {
        yield return new WaitForSeconds(2f);

        Brick newBrick = Instantiate(brickPrefab, dots[brickNumber].transform);
        newBrick.transform.localPosition = Vector3.zero;
        newBrick.transform.localRotation = Quaternion.Euler(0, 90, 0);
        newBrick.SetStage(this);
        newBrick.SetBrickNumber(brickNumber);
        newBrick.SetBrickColor(colorsOnStage[Random.Range(0, colorsOnStage.Count)]);

        brickList.Add(newBrick);
    }
}
