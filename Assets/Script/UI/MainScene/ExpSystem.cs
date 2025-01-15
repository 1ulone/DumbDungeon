using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpSystem : MonoBehaviour
{
    
    public static ExpSystem Instance { get; private set; }

    [SerializeField] private Image maskEXP;
    [SerializeField] private TextMeshProUGUI textLVL;

    public int maximumEXP;
    private float currentEXP;

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

    void Start()
    {
        currentEXP = maximumEXP;
        UpdateFillAmount();
    }



    //Update Gambar Aja
    private void UpdateFillAmount()
    {
        float fillAmount = currentEXP / maximumEXP;
        maskEXP.fillAmount = Mathf.Clamp01(fillAmount); 
    }

    public void UpdateEXP(float currentEXP)
    {
        this.currentEXP = Mathf.Clamp(currentEXP, 0, maximumEXP); 
        UpdateFillAmount();
    }

    public void UpdateLevel(int currentLvl)
    {
        textLVL.text = currentLvl.ToString();
    }
}
