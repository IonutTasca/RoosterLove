using UnityEngine;
using UnityEngine.UI;

public class Fps : MonoBehaviour
{
    [SerializeField] private Text _fpsText;
    private float _deltaTime;

    void Update()
    {
        _deltaTime += (Time.deltaTime - _deltaTime) * 0.1f;
        float fps = 1.0f / _deltaTime;
        _fpsText.text = Mathf.Ceil(fps).ToString();
    }
}
