using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScript : MonoBehaviour
{
    private Image _myLogo;
    private bool _loadFinish;
    private bool _endLogo;

    private void Awake()
    {
        _myLogo = GetComponent<Image>();
        _loadFinish = false;
        _endLogo = false;
        _myLogo.color = new Color(_myLogo.color.r, _myLogo.color.g, _myLogo.color.b,0f);
    }
    private void Start()
    {
        _loadFinish = true;
    }
    private void Update()
    {
        if(_loadFinish && _endLogo)
        {
          SceneManager.LoadScene("Menu");
        }
    }

    public void EndAnimationLogo()
    {
        _endLogo = true;
    }
}
