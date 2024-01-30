using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    Button button;
    [SerializeField] string sceneName;
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(LoadSceneWithName);
    }
    
    private void LoadSceneWithName()
    {
        SceneManager.LoadScene(sceneName);
    }
}
