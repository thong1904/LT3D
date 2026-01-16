using UnityEngine;
using UnityEngine.InputSystem;

public class InputRebindManager : MonoBehaviour
{
    public static InputRebindManager Instance;

    public InputActionAsset inputActions;

    private const string REBIND_SAVE_KEY = "InputRebinds";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadRebinds();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveRebinds()
    {
        string rebinds = inputActions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString(REBIND_SAVE_KEY, rebinds);
        PlayerPrefs.Save();
    }

    public void LoadRebinds()
    {
        if (!PlayerPrefs.HasKey(REBIND_SAVE_KEY)) return;

        string rebinds = PlayerPrefs.GetString(REBIND_SAVE_KEY);
        inputActions.LoadBindingOverridesFromJson(rebinds);
    }

    public void ResetRebinds()
    {
        inputActions.RemoveAllBindingOverrides();
        PlayerPrefs.DeleteKey(REBIND_SAVE_KEY);
    }
}
