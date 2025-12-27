using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FlappyPhysics : MonoBehaviour
{
    public static FlappyPhysics Instance;

    public event EventHandler OnPlayerDeathAnimationEnd;

    [SerializeField] private float forceScalar = 4500f;
    [SerializeField] private float gravityScale = 7.5f;
    private Coroutine playerDeadCoroutine;
    private Rigidbody2D rb;
    private float rotationalAngleUp;

    private void Awake()
    {
        Instance = this;

        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rotationalAngleUp = 0;
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
                ResetTransform();
                break;
            case GameManager.GameState.Playing:
                rb.gravityScale = gravityScale;
                break;
            case GameManager.GameState.PlayerDeathAnimation:
                playerDeadCoroutine = StartCoroutine(PlayerDeadCoroutineAnimation());
                break;
        }   
    }

    private void ResetTransform()
    {
        float startingRotation = 90f;
        rb.gravityScale = 0;
        rb.linearVelocityY = 0;
        rb.angularVelocity = 0f;
        rb.rotation = startingRotation;
        rb.position = Vector3.zero;
    }

    private IEnumerator PlayerDeadCoroutineAnimation()
    {
        yield return null;
        // Make the velocity < 0
        while (true)
        {
            float fallVelocity = -12.5f;
            float playerDeadYPosition = -9f;
            rb.linearVelocityY = fallVelocity;
            if (rb.position.y <= playerDeadYPosition || !(GameManager.Instance.GetGameState() == GameManager.GameState.PlayerDeathAnimation))
            {
                rb.linearVelocityY = 0f;
                rb.gravityScale = 0;
                OnPlayerDeathAnimationEnd.Invoke(this, EventArgs.Empty);
                yield break;
            }
            yield return null;   
        }
    }

    private void FixedUpdate()
    {
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        if (GameManager.Instance.GetGameState() == GameManager.GameState.Ready) return;

        float velocityY = rb.linearVelocityY;
        if (velocityY > 0)
        {
            rb.angularVelocity = 0f;
            float flapFacingRotationDirection = 135;
            rb.rotation = Mathf.SmoothDamp(rb.rotation, flapFacingRotationDirection, ref rotationalAngleUp, 0.02f);
        }
        else if (velocityY <= 0)
        {
            rotationalAngleUp = 0f;
            // If our velocity is down then we rotate downwards while clamping to z = 0 degrees
            float minClampAngularRotation = 45f;
            if (rb.rotation <= minClampAngularRotation)
            {
                rb.angularVelocity = 0;
                rb.rotation = minClampAngularRotation;
            }
            else
            {
                float turningAngularRate = 100f;
                rb.angularVelocity -= turningAngularRate;
            }
        }
    }

    public void Flap()
    {
        rb.AddForceY(forceScalar);
    }
}
