using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class UpgradeSystem : MonoBehaviour
{
    public TextMeshProUGUI deskripsiText;
    public Image[] Gambars;

    public static UpgradeSystem Instance { get; private set; }
    private Gun[] guns ;
    [SerializeField]
	private List<Gun> typesLeft = new List<Gun>();


	public Gun[] getGuns()
	{
		Gun[] gt = new Gun[typesLeft.Count];


		for (int i = 0; i < gt.Length; i++)
		{
			int rand = Random.Range(0, typesLeft.Count);
			gt[i] = typesLeft[rand]; 
			typesLeft.RemoveAt(rand);
		}

		return gt;
	}


    private void Awake()
    {
  
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); 
    }
    [SerializeField] GameObject rootUpgrade;
    private CanvasGroup rootCG;

    public void  Start()
    {
        rootCG = rootUpgrade.GetComponent<CanvasGroup>();
    }
    public void Upgrade(int i)
    {
        
        
        Debug.Log("Disini masbro");
    }

    public void showUpgradeMenu()
    {
        if(!rootUpgrade.activeSelf)
        {
            guns = getGuns();
            for(int i =  0; i<Gambars.Length; i++)
            {
                Gambars[i].sprite = guns[i].sprite;
            }
            rootUpgrade.SetActive(true);
        }
        else
            Debug.Log("Udah nyala");
    }

    public void hideUpgradeMenu()
    {
        if(rootUpgrade.activeSelf)
            rootUpgrade.SetActive(false);
        else
            Debug.Log("Udah Mati");
    }


}
