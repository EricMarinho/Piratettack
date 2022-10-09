using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSceneManager : MonoBehaviour
{

    [SerializeField] private TMP_InputField nameInputField;

    public void StartGame(int index)
    {
        PlayerPrefs.SetString("PlayerName", nameInputField.textComponent.text);
        SceneManager.LoadScene(index);
    }
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

}
