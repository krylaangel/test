using UnityEngine;
using System;

public class PlayerCollect : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] private AudioSource _coinAudioSource;
    
    [Header("Collection Settings")]
    [SerializeField] private string _coinTag = "Coin";
    
    private int _coinsCollected;
    
    public event Action<int> OnCoinCollected;
    public int CoinsCollected => _coinsCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (!IsValidCoin(other)) return;
        
        CollectCoin(other.gameObject);
    }

    private bool IsValidCoin(Collider other)
    {
        return other.CompareTag(_coinTag);
    }

    private void CollectCoin(GameObject coin)
    {
        _coinsCollected++;
        
        PlayCollectionSound();
        NotifyCoinCollected();
        DestroyCoin(coin);
    }

    private void PlayCollectionSound()
    {
        if (_coinAudioSource != null)
        {
            _coinAudioSource.Play();
        }
    }

    private void NotifyCoinCollected()
    {
        Debug.Log($"Coin collected! Total: {_coinsCollected}");
        OnCoinCollected?.Invoke(_coinsCollected);
    }

    private void DestroyCoin(GameObject coin)
    {
        Destroy(coin);
    }
}
