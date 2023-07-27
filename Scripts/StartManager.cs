using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StartManager : MonoBehaviour
{
    [SerializeField] Button _play_Button;
    [SerializeField] TMP_Text _play_Text;

    private void Awake()
    {
        _play_Button.onClick.AddListener(() =>
        {
            var x = PlayerPrefs.GetInt("Level");
            if (x < 10) SceneManager.LoadScene("GameScene");
        });
    }

    private void Start()
    {
        var x = PlayerPrefs.GetInt("Level");
        _play_Text.text = "Level : " + x.ToString();
    }
}
