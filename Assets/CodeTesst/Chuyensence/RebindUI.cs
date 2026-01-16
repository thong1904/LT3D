using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class RebindUI : MonoBehaviour
{
    public InputActionReference actionToRebind;

    [Header("Binding Name (up / down / left / right / jump ...)")]
    public string bindingName;

    public TextMeshProUGUI bindingText;

    private int bindingIndex = -1;

    void Start()
    {
        FindBindingIndex();
        UpdateBindingText();
    }

    void FindBindingIndex()
    {
        var action = actionToRebind.action;

        for (int i = 0; i < action.bindings.Count; i++)
        {
            if (action.bindings[i].name == bindingName)
            {
                bindingIndex = i;
                return;
            }
        }

        Debug.LogError($"❌ Không tìm thấy binding name: {bindingName}");
    }

    public void StartRebind()
    {
        if (bindingIndex == -1) return;

        var action = actionToRebind.action;
        action.Disable();

        bindingText.text = "Press a key...";

        action.PerformInteractiveRebinding(bindingIndex)
            .WithControlsExcluding("Mouse")
            .OnComplete(op =>
            {
                op.Dispose();
                action.Enable();

                UpdateBindingText();
                InputRebindManager.Instance.SaveRebinds();
            })
            .Start();
    }

    void UpdateBindingText()
    {
        if (bindingText == null || bindingIndex == -1) return;

        var action = actionToRebind.action;

        bindingText.text = InputControlPath.ToHumanReadableString(
            action.bindings[bindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice
        );
    }
}
