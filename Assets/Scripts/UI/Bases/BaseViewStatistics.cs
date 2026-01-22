using Bases;
using TMPro;
using UnityEngine;

namespace UI.Bases
{
    public class BaseViewStatistics : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _countText;
        
        private BaseStorage _storage;

        private const string InitialText = "Количество ресурсов:";

        private void Start() =>
            _countText.text = InitialText;
        
        public void UpdateCount(int value) =>
            _countText.text = $"{InitialText} {value}";
    }
}