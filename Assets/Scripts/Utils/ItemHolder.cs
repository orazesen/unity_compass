using UnityEngine;
using UnityEngine.UI;

public class ItemHolder : MonoBehaviour
{
    [HideInInspector]
    public Item item;

    public Image image;
    public Text mainTitle;
    public Text title;
    public Text description;

    private void Start()
    {
        image.sprite = Resources.Load<Sprite>("Images/" + item.image[0]);
    }

    public void Show()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("GameManager");
        if(obj != null)
        {
            GameManager manager = obj.GetComponent<GameManager>();
            if(manager != null)
            {
                manager.UpdateUI(item);
            }
        }
    }
}
