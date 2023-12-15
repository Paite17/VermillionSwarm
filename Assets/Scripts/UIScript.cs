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
            bossHealthPercentage = Mathf.Round((100 * bossRef.EnemyHealth) / maxBossHealth) / 100;
            bossHealthBar.fillAmount = bossHealthPercentage;
        }
    }

    public void ShowBossUI()
    {
        bossUI.SetActive(true);
        // first get the digit for the boss name
        int nameNum = gameManager.TimesBossWasBeat;
        int last = nameNum % 10;

        
        if (last < 1)
        {
            last = 1;
        }

        switch (last)
        {

            case 1:
                bossName.text = "Pi Guy";
                    break;
            case 2:
                bossName.text = "Pi Guy the " + last + "nd";
                break;
            case 3:
                bossName.text = "Pi Guy the " + last + "rd";
                break;
            default:
                bossName.text = "Pi Guy the " + last + "th";
                break;
        }
        

    }

    public void HideBossUI()
    {
        bossUI.SetActive(false);
    }
}
