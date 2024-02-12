using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum GameState
{
    PRE_WAVE,
    ACTIVE_WAVE,
    BOSS_WAVE,
    UPGRADE,
    POST_WAVE,
    DEATH
}

public class GameManager : MonoBehaviour
{

    [SerializeField] GameState state;
    //[SerializeField] private GameObject energyField;

    [SerializeField] private CardChoice cards;
    
    [SerializeField] private float score;
    [SerializeField] private float highscore;
    [SerializeField] private float wave;
    [Header("Wave Parameters")]
    [SerializeField] private float maxWaveTime; // the maximum amount of time in a wave
    [SerializeField] private float maxPreWaveTime; // the maximum amount of time in the prewave phase
    [SerializeField] private float difficultyValue; // the current difficulty modifier for the game
    [SerializeField] private int numOfWavesComplete; // for counting how many waves were completed
    [SerializeField] private float startCountDown; // counting down at the beginning of the game
    private float kills;

    [SerializeField] private float waveTimer; // the timer that counts down the current wave
    [SerializeField] private float preWaveTimer; // the timer that counds down the prewave phase

    [Header("System Stuff")]
    [SerializeField] private List<EntitySpawner> spawners; // a list of spawners that should automatically get filled on start
    [SerializeField] private List<Upgrades> allUpgrades; // a list of all possible upgrades the player is currently able to get
    [SerializeField] private int wavesUntilBoss = 5; // counts down each wave until the boss shows up
    [SerializeField] private int baseWaveUntilBoss; // the int used to reset the above counter
    [SerializeField] private int timesBossWasBeat; // a value keeping track of how many times you beat pi guy for his name change
    [SerializeField] private UIScript ui; // ref for the UI 

    [Header("Misc")]
    [SerializeField] private GameObject piGuyBoss; // the gameobject for pi guy himself
    [SerializeField] private AudioClip piGuyIntroVoice; // the normal pi guy voiceline
    [SerializeField] private AudioClip piGuyRematchVoice; // the voiceline for rematching pi guy
    [SerializeField] private AudioClip piGuyDeath1;
    [SerializeField] private AudioClip piGuyDeath2;
    [SerializeField] private AudioSource piGuySource; // the source of all pi guy audio

    private PlayerStats player;
    public Transform bossSpawn;
    public AudioSource bossMusic;
    public AudioSource mainSFX;

    public float Score
    {
        get { return score; }
    }

    public float Highscore
    {
        get { return  highscore; }
    }

    public float Wave
    {
        get { return wave; }
    }

    public float DifficultyValue
    {
        get { return difficultyValue; }
    }

    public int NumOfWavesComplete
    {
        get { return numOfWavesComplete; }
    }

    public float WaveTimer
    {
        get { return waveTimer; }
    }

    public float PreWaveTimer
    {
        get { return preWaveTimer; }
    }

    public float StartCountDown
    {
        get { return  startCountDown; }
    }

    public int WavesUntilBoss
    {
        get { return wavesUntilBoss; }
        set { wavesUntilBoss = value; }
    }

    public float MaxWaveTime
    {
        get { return maxWaveTime; }
    }

    public GameState State
    {
        get { return state; }
        set { state = value; }
    }

    public int TimesBossWasBeat
    {
        get { return timesBossWasBeat; }
        set { timesBossWasBeat = value; }
    }

    private void Start()
    {
        FindSpawners();
        FindAllUpgrades();
        DeactivateSpawners();
        player = FindObjectOfType<PlayerStats>();
        wave = 1;
        wavesUntilBoss = baseWaveUntilBoss;
        waveTimer = maxWaveTime;
        preWaveTimer = maxPreWaveTime;
        timesBossWasBeat = 1;
    }


    private void Update()
    {
        // debug probably
        /*if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Instantiate(energyField);
        } */

        // timers
        switch (state)
        {
            case GameState.PRE_WAVE:
                PreWaveCounter();
                break;
            case GameState.ACTIVE_WAVE:
                ActiveWaveCounter();
                break;
        }

        /*if (piGuyBoss.GetComponent<Enemy>().EnemyHealth <= 0 && state == GameState.BOSS_WAVE)
        {
            Debug.Log("BOSS DEAD");
            // beat boss
            timesBossWasBeat++;
            ui.HideBossUI();
            EndOfWave();

        } */

        // ^ pretty sure this does nothing lol
    }

    // adds every spawner present on the map to the list
    private void FindSpawners()
    {
        var findings = FindObjectsOfType<EntitySpawner>();

        foreach (var current in findings)
        {
            spawners.Add(current);
        }
    }

    // for turning off spawner objects during prewave sequences
    public void DeactivateSpawners()
    {
        for (int i = 0; i < spawners.Count; i++)
        {
            spawners[i].Activated = false;
            spawners[i].ResetTimer();
        }
    }

    
    // activates all the spawners because :)
    public void ActivateSpawners()
    {
        for (int i = 0; i < spawners.Count; i++)
        {
            spawners[i].Activated = true;
        }
    }

    private void PreWaveCounter()
    {
        preWaveTimer -= Time.deltaTime;

        if (preWaveTimer <= 0)
        {
            // START wave
            if (wavesUntilBoss > 0)
            {
                Debug.Log("Wave should start");
                WaveStart();
            }
            else
            {
                BossWaveStart();
            }
        }
    }

    private void ActiveWaveCounter()
    {
        //Debug.Log("guh");
        waveTimer -= Time.deltaTime;

        if (waveTimer <= 0)
        {
            // END WAVE
            EndOfWave();
        }
    }

    public void EndOfWave()
    {
        Debug.Log("Wave Ended");
        //StartCoroutine(EndOfWaveLogic());

        // boss
        if (state == GameState.BOSS_WAVE)
        {
            bossMusic.Stop();
            mainSFX.Play();

            // pi guy dies.mp3
            if (Random.Range(0, 1) == 1)
            {
                piGuySource.clip = piGuyDeath1;
            }
            else
            {
                piGuySource.clip = piGuyDeath2;
            }
            piGuySource.Play();
        }

        // logic
        DeactivateSpawners();
        bool d = false;
        waveTimer = maxWaveTime;

        if (!d)
        {
            wave++;
            wavesUntilBoss--;
            d = true;
        }
        DespawnEnemies();

        state = GameState.UPGRADE;
        UpgradeState();

    }

   /* private IEnumerator EndOfWaveLogic()
    {
       
        // logic
        DeactivateSpawners();
        bool d = false;
        waveTimer = maxWaveTime;

        if (!d)
        {
            wave++;
            wavesUntilBoss--;
            d = true;
        }
        DespawnEnemies();

        yield return new WaitForSeconds(2f);

        state = GameState.UPGRADE;
        UpgradeState();
    } */

    // pause stuff during upgrade phase
    public void UpgradeState()
    {
        Debug.Log("Upgrading");
        Time.timeScale = 0;
        cards.PickRandomObjects();
        //wavesUntilBoss--;

        if (wavesUntilBoss > 0)
        {
            WaveStart();
            ui.HideBossUI();
        }
        else
        {
            BossWaveStart();
        }
    }

    private void WaveStart()
    {
        state = GameState.ACTIVE_WAVE;
        ActivateSpawners();

        // any UI related things
        waveTimer = maxWaveTime;

        // start music
    }

    private void BossWaveStart()
    {
        state = GameState.BOSS_WAVE;
        // set UI things active
        ui.ShowBossUI();
        // set music
        mainSFX.Stop();
        bossMusic.Play();
        // pi guy voiceline
        if (timesBossWasBeat > 1)
        {
            piGuySource.clip = piGuyRematchVoice;
        }
        else
        {
            piGuySource.clip = piGuyIntroVoice;
        }
        piGuySource.Play();

        // spawn boss
        Instantiate(piGuyBoss, bossSpawn.position, Quaternion.identity);
    }

    public void GameOver()
    {
        // will probably use a coroutine
    }

    // make enemies retreat
    public void DespawnEnemies()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var current in enemies)
        {
            //Destroy(current);
            current.GetComponent<Enemy>().retreating = true;
        }
    }

    public void ResetBossCount()
    {
        wavesUntilBoss = baseWaveUntilBoss;
    }

    private void FindAllUpgrades()
    {
        // get them
        var upgrade = Resources.LoadAll("Upgrades", typeof(Upgrades)).Cast<Upgrades>();

        // look through them
        foreach (var current in upgrade)
        {
            allUpgrades.Add(current);
        }
    }

}
