using UnityEngine;

public class PlayerCollect : MonoBehaviour
{
    private int coinsCollected = 0;

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, что вошли в монетку
        if (other.CompareTag("Coin"))
        {
            coinsCollected++;
            Debug.Log("Монетка подобрана! Всего: " + coinsCollected);

            AudioSource audio = other.GetComponent<AudioSource>();
            if (audio != null)
            {
                audio.Play();
            }

            Destroy(other.gameObject, 0.2f);
        }
    }
}
