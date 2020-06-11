using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public class Intro : MonoBehaviour
    {
        [SerializeField]
        private GameObject introPanelPrefab;
        [SerializeField]
        private Transform canvas;
        
        private Image[] _images;
        private Text _text;
        private GameObject _introPanel;
    
        private void Start()
        {
            _introPanel = Instantiate(introPanelPrefab, canvas);
            _images = _introPanel.GetComponentsInChildren<Image>();
            _text = _introPanel.GetComponentInChildren<Text>();
            _introPanel.SetActive(true);
            StartCoroutine(Fade());
        }
        
        private IEnumerator Fade()
        {
            yield return new WaitForSeconds(3f);
            var currentAlpha = 1f;
            float velocity = 0;
            while (currentAlpha > 0.01f)
            {
                currentAlpha = Mathf.SmoothDamp(currentAlpha, 0, ref velocity, 0.04f);
                foreach (var image in _images)
                {
                    var imgColor = image.color;
                    image.color = new Color(imgColor.r, imgColor.g, imgColor.b, currentAlpha);
                    var textColor = _text.color;
                    _text.color = new Color(textColor.r, textColor.g, textColor.b, currentAlpha);
                }
                yield return new WaitForSeconds(0.1f);
            }
            
            _introPanel.SetActive(false);
            Destroy(_introPanel);
        }
    }
}