using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    PRE_WAVE,
    ACTIVE_WAVE,
    POST_WAVE,
    DEATH
}

public class GameManager : MonoBehaviour
{

    [SerializeField] GameState state;
    [SerializeField] private GameObject energyField;

    
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

    private float waveTimer; // the timer that counts down the current wave
    private float preWaveTimer; // the timer that counds down the prewave phase

    [SerializeField] private List<EntitySpawner> spawners; // a list of spawners that should automatically get filled on start

    private Player player;

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

    private void Start()
    {
        FindSpawners();
        player = FindObjectOfType<Player>();
        wave = 1;

    }


    private void Update()
    {
        // debug probably
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Instantiate(energyField);
        }

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

    private void PreWaveCounter()
    {
        preWaveTimer -= Time.deltaTime;

        if (preWaveTimer <= 0)
        {
            // START wave
        }
    }

    private void ActiveWaveCounter()
    {
        waveTimer += Time.deltaTime;

        if (waveTimer >= maxWaveTime)
        {
            // END WAVE
        }
    }

    private void EndOfWave()
    {

    }

}
