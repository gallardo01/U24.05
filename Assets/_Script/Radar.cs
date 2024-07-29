using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Radar : MonoBehaviour
{
    public static Radar instance;
    public Transform player;
    public float radarRadius = 100f;
    public LayerMask targetLayer;
    public RectTransform Canvas;
    public GameObject radarIconPrefab;
    //[SerializeField] TMP_Text levelPlayer;

    private List<Transform> targets = new List<Transform>();
    private List<GameObject> radarIcons = new List<GameObject>();
    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        FindTargets();
        UpdateRadar();
    }

    void FindTargets()
    {
        targets.Clear();
        Collider[] hits = Physics.OverlapSphere(player.position, radarRadius, targetLayer);
        foreach (Collider hit in hits)
        {
            targets.Add(hit.transform);
        }
    }

    void UpdateRadar()
    {
        foreach (GameObject icon in radarIcons)
        {
            Destroy(icon);
        }
        radarIcons.Clear();

        foreach (Transform target in targets)
        {
            Vector3 direction = player.position - target.position;
            float distance = direction.magnitude;
            if (distance <= radarRadius)
            {
                Vector3 radarPos = direction.normalized * (distance / radarRadius) * (Canvas.rect.width);
                GameObject icon = Instantiate(radarIconPrefab, Canvas);
                icon.GetComponent<Image>().color = target.GetComponent<Character>().namePlayer.color;
                //levelPlayer.GetComponent<TMP_Text>().text = target.GetComponent<Character>().level.ToString();
                icon.GetComponent<RectTransform>().localPosition = new Vector3(radarPos.x, radarPos.z, 0);
                radarIcons.Add(icon);
            }
        }
    }
}
