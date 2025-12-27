using System;
using UnityEngine;

public class FlappyParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem flapParticles;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float playerParticleSystemXOffset = -0.5f;
    [SerializeField] private float playerParticleSystemYOffset = 0.5f;

    private void Start()
    {
        GameInput.Instance.OnPlayerInteract += GameInput_OnPlayerInteract;

        flapParticles.gameObject.SetActive(false);
    }
    private void GameInput_OnPlayerInteract(object sender, EventArgs e)
    {
        if (GameManager.Instance.GetGameState() != GameManager.GameState.Playing) return;

        flapParticles.gameObject.SetActive(true);
        flapParticles.Clear();
        transform.position = playerTransform.position + new Vector3(playerParticleSystemXOffset, playerParticleSystemYOffset, 0f);
        gameObject.SetActive(true);
        flapParticles.Play();
    }
}
