using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject coverPanel;
    public GameObject itemPrefab;
    public Transform itemHolder;
    private List<GameObject> objects = new List<GameObject>();

    public Text mainTitle;
    public Text title;
    public Text description;

    public GameObject canvas;

    public GameObject closeButton;

    private Item item;

    public GameObject popup;
    
    // Start is called before the first frame update
    void Start()
    {
        coverPanel.SetActive(false);
        if(SceneManager.GetActiveScene().name == Scenes.Start)
        {
            StartGame();
        }
        else
        {
            foreach (var i in Items.items)
            {
                GameObject obj = Instantiate(itemPrefab, itemHolder);
                ItemHolder holderScript = obj.GetComponent<ItemHolder>();
                if(holderScript != null)
                {
                    holderScript.item = i;
                    obj.SetActive(true);
                    objects.Add(obj);
                }
                else
                {
                    Destroy(obj);
                }
            }

            UpdateUI(Items.items[0]);
        }
    }

    IEnumerator ISTartGame()
    {
        yield return new WaitForSeconds(4f);
        coverPanel.SetActive(true);
        yield return new WaitForSeconds(0.6f);        
        SceneManager.LoadScene(Scenes.Main);
    }

    public void StartGame()
    {
        StartCoroutine(ISTartGame());
    }


    public void UpdateUI(Item item)
    {
        this.item = item;
        mainTitle.text = item.name;
        title.text = item.fullName;
        description.text = item.description;
        StartCoroutine(IEnableContentSizeFitter());

        GameObject holder = GameObject.FindGameObjectWithTag("Holder");
        if (holder != null)
        {
            if (holder.transform.childCount > 0)
            {
                foreach (Transform i in holder.transform)
                {
                    Destroy(i.gameObject);
                }
            }

            GameObject obj = Resources.Load<GameObject>("Models/" + item.image[0]);

            if (obj != null)
            {
                Instantiate(obj, holder.transform);
            }
            else
            {
                Debug.Log("Model is null");
            }
        }
        else
        {
            Debug.Log("Holder is null");
        }
    }

    private IEnumerator IEnableContentSizeFitter()
    {
        yield return new WaitForSeconds(0.1f);
        description.GetComponent<ContentSizeFitter>().enabled = false;
        yield return new WaitForSeconds(0.3f);
        description.GetComponent<ContentSizeFitter>().enabled = true;
    }

    public void MagnifyObject()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Rotate rotator = player.GetComponentInParent<Rotate>();

        rotator.Magnify();
        canvas.SetActive(false);
        closeButton.SetActive(true);


    }

    public void CloseMagnifier()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Rotate rotator = player.GetComponentInParent<Rotate>();

        rotator.DeMagnify();
        canvas.SetActive(true);
        closeButton.SetActive(false);
    }

    public void OpenPopup()
    {
        popup.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
