using Bases;
using TMPro;
using UnityEngine;

namespace UI.Bases
{
    public class BaseViewStatistics : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _countText;
        [SerializeField] private BaseStorage _storage;

        private const string InitialText = "Количество ресурсов:";

        private void Start() =>
            _countText.text = InitialText;


        private void OnEnable() =>
            _storage.CountChanged += UpdateCount;

        private void OnDisable() =>
            _storage.CountChanged -= UpdateCount;

        private void UpdateCount(int value) =>
            _countText.text = $"{InitialText} {value}";
    }
}