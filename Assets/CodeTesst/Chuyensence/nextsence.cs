using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SceneSwitcher_InputSystem : MonoBehaviour
{
    public string sceneToLoad = "Scene2";

    PlayerInputActions input;

    void Awake()
    {
        input = new PlayerInputActions();
    }

    void OnEnable()
    {
        input.Player.Enable();
        input.Player.Test.performed += OnChangeScene;
    }

    void OnDisable()
    {
        input.Player.Test.performed -= OnChangeScene;
        input.Player.Disable();
    }

    void OnChangeScene(InputAction.CallbackContext ctx)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
