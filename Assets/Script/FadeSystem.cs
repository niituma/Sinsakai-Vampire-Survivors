using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

public class FadeSystem : MonoBehaviour
{
    [SerializeField] float _fadeInSpeed = 0.8f;
    [SerializeField] float _fadeOutSpeed = 0.5f;
    [Tooltip("��ʂ��^���ÂɂȂ��Ă���V�[���J�ڂ�҂���"), SerializeField]
    float _sceneDlaytime = 3f;
    float red, green, blue, alfa;
    [SerializeField] bool _isFadeOut = default;
    [SerializeField] bool _isFadeIn = default;
    public string _scenename { get; private set; }
    Image fadeImage;
    AddOrignalMethod Method = new AddOrignalMethod();

    private void Start()
    {
        fadeImage = GetComponent<Image>();
        red = fadeImage.color.r;
        green = fadeImage.color.g;
        blue = fadeImage.color.b;
        alfa = fadeImage.color.a;
    }
    private void Update()
    {
        if (_isFadeOut)
        {
            StartFadeOut();
        }
        else if (_isFadeIn)
        {
            StartFadeIn();
        }
    }
    public void IsFadeIn()
    {
        this.gameObject.SetActive(true);
        _isFadeIn = true;
    }
    public void IsFadeOut(string num)
    {
        this.gameObject.SetActive(true);  // a)�p�l���̕\�����I���ɂ���
        _isFadeOut = true;
        _scenename = num;
    }
    void StartFadeIn()
    {
        if (alfa == 0)
        {
            alfa = 1;
            fadeImage.color = new Color(red, green, blue, alfa);
        }
        alfa -= _fadeInSpeed * Time.deltaTime;                //a)�s�����x�����X�ɉ�����
        fadeImage.color = new Color(red, green, blue, alfa);    //b)�ύX�����s�����x�p�l���ɔ��f����
        if (alfa <= 0)
        {
            _isFadeIn = false;
            this.gameObject.SetActive(false);
        }
    }
    void StartFadeOut()
    {
        if (alfa == 1)
        {
            alfa = 0;
            fadeImage.color = new Color(red, green, blue, alfa);
        }
        alfa += _fadeOutSpeed * Time.deltaTime;         // b)�s�����x�����X�ɂ�����
        fadeImage.color = new Color(red, green, blue, alfa);    // c)�ύX���������x���p�l���ɔ��f����
        if (alfa >= 1)
        {
            StartCoroutine(Method.DelayMethod(_sceneDlaytime, () =>
            {
                _isFadeOut = false;  //d)�p�l���̕\�����I�t�ɂ���
                SceneManager.LoadScene(_scenename);
            }));
        }
    }
}

