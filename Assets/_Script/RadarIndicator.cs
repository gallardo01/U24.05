using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;
using static UnityEngine.GraphicsBuffer;

public class RadarIndicator : MonoBehaviour
{
    [SerializeField] GameObject markPlayerPrebs;
    public List<GameObject> targetList = new List<GameObject>();
    public Player player;
    Quaternion rotationParent;
    private void Start()
    {
        rotationParent = this.transform.rotation;
    }
    private void LateUpdate()
    {
        if (!player.isDead)
        {
            List<GameObject> list = GameController.instance.countBots;
            this.transform.rotation = rotationParent;
            //Delete mark when player die
            for (int i = 0; i < targetList.Count; i++)
            {
                if (targetList[i] == null)
                {
                    targetList.Remove(targetList[i]);
                }
            }
            //Update mark position
            for (int i = 0; i < list.Count; i++)
            {
                Vector3 distance = player.transform.position - list[i].transform.position;
                Vector3 direction = new Vector3(distance.normalized.x, 0f, distance.normalized.z);
                Quaternion rotation = Quaternion.LookRotation(direction);
                Vector3 newPos = direction * 0.5f;
                targetList[i].transform.localPosition = new Vector3(newPos.x, 0, newPos.z);
                targetList[i].transform.localRotation = rotation;
            }
        }
    }
    public void CreatAllMark()
    {
        List<GameObject> list = GameController.instance.countBots;
        for (int i = 0; i < list.Count; i++)
        {            
            GameObject markPlayer = Instantiate(markPlayerPrebs,this.transform);
            Color markColor = list[i].GetComponent<Bot>().namePlayer.color;
            markPlayer.GetComponent<Mark>().self = list[i].GetComponent<Bot>();
            markPlayer.GetComponent<Mark>().markPlayer.GetComponent<SpriteRenderer>().color = markColor;
            Debug.Log(markColor);
            targetList.Add(markPlayer);
        }
    }
}
