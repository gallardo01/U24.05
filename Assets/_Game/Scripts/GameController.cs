using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [SerializeField] List<ColorMaterial> colorMaterials = new List<ColorMaterial>();
    [SerializeField] List<GameColor> colorsSystem = new List<GameColor>();
    private HashSet<GameColor> systemColorsSet;

    private Dictionary<GameColor, Material> materials = new Dictionary<GameColor, Material>();

    public List<GameColor> colorsUsed = new List<GameColor>();
    public List<GameColor> colorsReady = new List<GameColor>();

    public GameColor playerColor;

    [SerializeField] Player playerPrefab;
    [SerializeField] Enemy enemyPrefab;

    private List<Character> characters = new List<Character>();

    void Start()
    {
        InitMaterial();
        UIManager.Ins.OpenUI<UIMainmenu>();
    }

    public void InitCharacter(List<Transform> startPos)
    {
        ClearCharacter();

        Player player = Instantiate(playerPrefab);
        player.OnInit(colorsUsed[0], startPos[0]);
        characters.Add(player);

        for (int i = 0; i < Constants.QUANTITY_ENEMY; i++)
        {
            Enemy enemy = Instantiate(enemyPrefab);
            enemy.OnInit(colorsUsed[i + 1], startPos[i + 1]);
            characters.Add(enemy);
        }
    }

    public void ClearCharacter()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            Destroy(characters[i].gameObject);
        }
        characters.Clear();
    }

    public void ClearCharacterBrick()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            characters[i].ClearBrick();
        }
    }

    public Material GetMaterialColor(GameColor color)
    {
        return materials[color];
    }

    private void InitMaterial()
    {
        for (int i = 0; i < colorMaterials.Count; i++)
        {
            materials.Add(colorMaterials[i].color, colorMaterials[i].material);           
        }
        systemColorsSet = new(colorsSystem);
    }

    public void GenerateColor()
    {
        colorsUsed.Clear();
        colorsReady.Clear();

        playerColor = DataManager.Ins.GetPlayerSkin();
        colorsUsed.Add(playerColor);

        for (int i = 0; i < colorMaterials.Count; i++)
        {           
            if (!systemColorsSet.Contains(colorMaterials[i].color) && colorMaterials[i].color != playerColor)
            {
                colorsReady.Add(colorMaterials[i].color);
            }
        }

        for (int i = 0; i < Constants.QUANTITY_ENEMY; i++)
        {
            if (colorsReady.Count > 0)
            {
                int temp = Random.Range(0, colorsReady.Count);
                colorsUsed.Add(colorsReady[temp]);
                colorsReady.RemoveAt(temp);
            }
        }
    }
}

[System.Serializable]
public class ColorMaterial
{
    public GameColor color;
    public Material material;
}
