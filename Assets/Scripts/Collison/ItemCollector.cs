using TMPro;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private int _cherriesCounter = 0;

    [SerializeField] private TextMeshProUGUI cherriesText;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Cherry"))
        {
            Destroy(other.gameObject);
            _cherriesCounter += 1;
            cherriesText.text = "Cherries: " + _cherriesCounter; 
        }
    }
}
