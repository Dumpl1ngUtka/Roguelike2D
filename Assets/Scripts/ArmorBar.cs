using UnityEngine;
using UnityEngine.UI;

public class ArmorBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private Image _barImage;

    private void Awake()
    {
        _playerHealth.OnArmorChange += UpdateBar;
    }
    private void UpdateBar(float value)
    {
        _barImage.fillAmount = value;
    }
}
