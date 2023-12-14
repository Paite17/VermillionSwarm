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

        }
    }

    public void ShowBossUI()
    {
        bossUI.SetActive(true);
        // first get the digit for the boss name
        int nameNum = gameManager.TimesBossWasBeat;
        int last = nameNum % 10;

        int theNumber = last - 1;
        if (theNumber < 1)
        {
            theNumber = 1;
        }

        switch (theNumber)
        {

            case 1:
                bossName.text = "Pi Guy";
                    break;
            case 2:
                bossName.text = "Pi Guy the " + theNumber + "nd";
                break;
            case 3:
                bossName.text = "Pi Guy the " + theNumber + "rd";
                break;
            default:
                bossName.text = "Pi Guy the " + theNumber + "th";
                break;
        }
        

    }

    public void HideBossUI()
    {
        bossUI.SetActive(false);
    }
}
