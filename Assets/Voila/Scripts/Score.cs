using UnityEngine;
using UnityEngine.UI;

namespace Voila.Scripts
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private Text _text;
        private int _score = 0;

        // Start is called before the first frame update
        void Start()
        {
            Collectable.OnCollect += IncreaseTheScore;
            UpdateText();
        }

        private void UpdateText()
        {
            _text.text = _score.ToString();
        
        }

        private void IncreaseTheScore()
        {
            _score++;
            UpdateText();
        }
    }
}