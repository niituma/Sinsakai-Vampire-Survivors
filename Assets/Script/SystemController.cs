using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SystemController : MonoBehaviour
{
    [SerializeField] Slider _expSlider;
    [SerializeField] TextMeshProUGUI _levelText;
    [SerializeField] GameObject _finishPanel;
    [SerializeField] GameObject _pausePanal;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance._expSlider = _expSlider;
        GameManager.Instance._levelText = _levelText;
        GameManager.Instance._finishPanel = _finishPanel;
        GameManager.Instance._pausePanal = _pausePanal;
        GameManager.Instance.Setup();
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.Instance.Pause();
    }

    public void IsPause()
    {
        GameManager.Instance.IsPause();
    }
}
