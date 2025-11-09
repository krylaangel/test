using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    [SerializeField] private AudioSource coinAudio;
    private int coinsCollected = 0;

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, что вошли именно в монетку
        if (!other.CompareTag("Coin")) return;

        coinsCollected++;
        Debug.Log($"Монетка подобрана! Всего: {coinsCollected}");

        if (GetComponent<AudioSource>() != null)
        {
            GetComponent<AudioSource>().Play();
        }
        coinAudio.Play();

        coinAudio.Play();

        Destroy(other.gameObject);
    }
}
