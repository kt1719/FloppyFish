using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    public static PipeController Instance { get; private set; }

    public event EventHandler OnPlayerScore;
    public event EventHandler OnPlayerHit;

    [SerializeField] private Transform pipePrefab;
    [SerializeField] private float pipeSpawnInterval = 1f;
    [SerializeField] private float pipeMaxYSpawnOffset = 3.5f;
    [SerializeField] private float pipeMinYSpawnOffset = -3.5f;

    [SerializeField] private float maxYSineOffset = 4f;
    [SerializeField] private float pipeSineTimeScalar = 5f;

    private Coroutine spawnPipesCoroutine;
    private Coroutine movePipesCoroutine;
    private List<PipeComposite> pipeCompositeList;
    private int totalPipeCount;


    private void Awake()
    {
        Instance = this;

        pipeCompositeList = new List<PipeComposite>();
    }

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(object sender, GameManager.GameStateChangedEventArgs e)
    {
        switch (e.gameState)
        {
            case GameManager.GameState.Ready:
                CleanupPipes();
                break;
            case GameManager.GameState.Playing:
                spawnPipesCoroutine = StartCoroutine(SpawnPipes());
                movePipesCoroutine = StartCoroutine(MovePipes());
                break;
            case GameManager.GameState.PlayerDeathAnimation:
                StopCoroutine(spawnPipesCoroutine);
                StopCoroutine(movePipesCoroutine);
                break;
        }   
    }

    private void CleanupPipes()
    {
        foreach (PipeComposite pipeComposite in pipeCompositeList)
        {
            Destroy(pipeComposite.gameObject);
        }

        pipeCompositeList.Clear();
    }

    private IEnumerator SpawnPipes()
    {
        while (true) // Change later for "while game is playing"
        {   
            float yOffset = UnityEngine.Random.Range(pipeMinYSpawnOffset, pipeMaxYSpawnOffset);
            Vector2 pipeSpawnPoint = PipeSpawnPoint.Instance.GetPosition() + new Vector2(0f, yOffset);
            Transform pipeGameObject = Instantiate(pipePrefab, pipeSpawnPoint, Quaternion.Euler(0, 0, 0));
            pipeGameObject.parent = transform;

            // Subscribe to the pipe event
            PipeComposite pipeComposite = pipeGameObject.GetComponent<PipeComposite>();
            pipeComposite.OnPlayerHit += Pipe_OnPlayerHit;
            pipeComposite.OnPlayerScore += PipeComposite_OnPlayerScore;
            pipeComposite.OnPipeCompositeDespawned += PipeComposite_OnPipeCompositeDespawned;
            pipeCompositeList.Add(pipeComposite);

            yield return new WaitForSeconds(pipeSpawnInterval);
        }
    }

    private IEnumerator MovePipes()
    {
        while (true)
        {
            // at 200 we should be moving very fast
            float yOffset = maxYSineOffset * Mathf.Sin(Time.time * pipeSineTimeScalar);
            transform.position = new Vector3(0, yOffset, 0);

            yield return null;
        }
    }

    private void PipeComposite_OnPipeCompositeDespawned(object sender, EventArgs e)
    {
        pipeCompositeList.Remove((PipeComposite)sender);
    }

    private void PipeComposite_OnPlayerScore(object sender, EventArgs e)
    {
        OnPlayerScore.Invoke(this, EventArgs.Empty);
    }

    private void Pipe_OnPlayerHit(object sender, EventArgs e)
    {
        OnPlayerHit.Invoke(this, EventArgs.Empty);
    }

    public int GetPipeCount()
    {
        return totalPipeCount;
    }
}
