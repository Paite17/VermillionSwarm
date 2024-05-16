using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIScript : MonoBehaviour
{
    [Header("Wave HUD")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Image waveTimeProgressBar;
    [SerializeField] private GameObject bossUI;
    [SerializeField] private Image bossHealthBar;
    
    

    private float bossHealthPercentage;
    public float maxBossHealth;
    public Enemy bossRef;
    private float waveTimePercentage;

    [Header("Systems")]
    [SerializeField] private GameManager gameManager;

    [Header("Text Values")]
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text bossName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.State == GameState.ACTIVE_WAVE)
        {
            waveTimePercentage = Mathf.Round((100 * gameManager.WaveTimer) / gameManager.MaxWaveTime) / 100;
            waveTimeProgressBar.fillAmount = waveTimePercentage;
        }

        if (gameManager.State == GameState.BOSS_WAVE)
        {
            ShowBossUI();
        }

        if (gameManager.State == GameState.BOSS_WAVE)
        {
            if (bossRef != null)
            {
                bossHealthPercentage = Mathf.Round((100 * bossRef.EnemyHealth) / maxBossHealth) / 100;
                bossHealthBar.fillAmount = bossHealthPercentage;
            }
            
        }
    }

    public void ShowBossUI()
    {
        // could just use FindObjectOfType or whatever
        bossRef = gameManager.piGuyCurrentInstance;
        bossUI.SetActive(true);
        // first get the digit for the boss name
        int nameNum = gameManager.TimesBossWasBeat;
        int last = nameNum % 10;

        
        if (last < 1)
        {
            last = 1;
        }

        if (nameNum != 1)
        {
            bossName.text = "Pi Guy the " + nameNum + GetOrdinalSuffix(nameNum);
        }
        else
        {
            bossName.text = "Pi Guy";
        }

    }

    public void HideBossUI()
    {
        bossUI.SetActive(false);
    }

    public string GetOrdinalSuffix(int number)
    {
        if ((number % 100 >= 11 && number % 100 <= 13) || number % 10 == 0)
        {
            return "th"; // Special case for 11, 12, 13 and numbers ending in 0 - they use "th"
        }
        else
        {
            switch (number % 10)
            {
                case 1:
                    return "st";
                case 2:
                    return "nd";
                case 3:
                    return "rd";
                default:
                    return "th";
            }
        }
    }

}
