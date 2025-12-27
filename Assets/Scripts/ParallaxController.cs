using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private float parallaxEffectSpeed;
    
    private Transform backgroundParentTransform;
    private float spriteWidth;
    private float originalYPosition;
    private Transform nextGameObject;
    private Coroutine movementCoroutine;
    private bool instantiatedGameObject = false;

    private void Awake()
    {
        spriteWidth = GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2f * transform.localScale.x;
        originalYPosition = transform.position.y;
        backgroundParentTransform = transform.parent;
    }

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;

        if (GameManager.Instance.GetGameState() == GameManager.GameState.Playing)
        {
            movementCoroutine = StartCoroutine(StartMovement());
        }
    }

    void OnDestroy()
    {
        GameManager.Instance.OnGameStateChanged -= GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(object sender, GameManager.GameStateChangedEventArgs e)
    {
        switch (e.gameState)
        {
            case GameManager.GameState.Ready:
                instantiatedGameObject = false;
                transform.position = new Vector3(0f, originalYPosition, 0f);
                if (nextGameObject) Destroy(nextGameObject.gameObject);
                break;
            case GameManager.GameState.Playing:
                movementCoroutine = StartCoroutine(StartMovement());
                break;
            case GameManager.GameState.PlayerDeathAnimation:
                StopCoroutine(movementCoroutine);
                break;
        }  
    }

    private IEnumerator StartMovement()
    {
        yield return null;
        while (true)
        {
            transform.position -= new Vector3(parallaxEffectSpeed, 0f, 0f) * Time.deltaTime;
            yield return null;
        }
    }

    private void Update()
    {
        // Calculate distance background move based on cam movement
        if (transform.position.x <= 0 && !instantiatedGameObject)
        {
            instantiatedGameObject = true;
            nextGameObject = Instantiate(transform, new Vector3(spriteWidth * 2, transform.position.y, 0f), Quaternion.Euler(Vector3.zero));

            nextGameObject.parent = backgroundParentTransform;
        }

        float despawnPointX = -spriteWidth * 2;
        if (transform.position.x <= despawnPointX)
        {
            StopCoroutine(movementCoroutine);
            Destroy(gameObject);
        }
        
    }
}
