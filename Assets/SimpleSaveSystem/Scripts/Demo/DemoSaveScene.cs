using Betoraf.SimpleSaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DemoSaveScene : MonoBehaviour
{
    [Header("Input Fields")]
    public TMP_InputField intInputField; // Input field for integer values
    public TMP_InputField floatInputField; // Input field for float values
    public TMP_InputField stringInputField; // Input field for string values

    [Header("Display Texts")]
    public TMP_Text intText; // Text field to display the saved integer value
    public TMP_Text floatText; // Text field to display the saved float value
    public TMP_Text stringText; // Text field to display the saved string value

    [Header("Save Buttons")]
    public Button saveIntButton; // Button to save integer value
    public Button saveFloatButton; // Button to save float value
    public Button saveStringButton; // Button to save string value

    [Header("Load Buttons")]
    public Button loadIntButton; // Button to load saved integer value
    public Button loadFloatButton; // Button to load saved float value
    public Button loadStringButton; // Button to load saved string value

    // Start is called before the first frame update
    private void Start()
    {
        // Add listeners to buttons for saving and loading values
        saveIntButton.onClick.AddListener(SaveInt);
        saveFloatButton.onClick.AddListener(SaveFloat);
        saveStringButton.onClick.AddListener(SaveString);

        loadIntButton.onClick.AddListener(LoadInt);
        loadFloatButton.onClick.AddListener(LoadFloat);
        loadStringButton.onClick.AddListener(LoadString);
    }

    // Method to save an integer value entered in the intInputField
    private void SaveInt()
    {
        // Try to parse the input field text as a long integer
        if (long.TryParse(intInputField.text, out long longValue))
        {
            // Save the value using SimpleSaveSystem
            SimpleSaveSystem.Set("playerInt", longValue.ToString());
            Debug.Log($"Int saved: {longValue}"); // Log the saved integer value
        }
        else
        {
            Debug.LogWarning("Invalid Int input"); // Log a warning if input is invalid
        }
    }

    // Method to save a float value entered in the floatInputField
    private void SaveFloat()
    {
        // Try to parse the input field text as a float
        if (float.TryParse(floatInputField.text, out float floatValue))
        {
            // Save the value using SimpleSaveSystem
            SimpleSaveSystem.Set("playerFloat", floatValue.ToString());
            Debug.Log($"Float saved: {floatValue}"); // Log the saved float value
        }
        else
        {
            Debug.LogWarning("Invalid Float input"); // Log a warning if input is invalid
        }
    }

    // Method to save a string value entered in the stringInputField
    private void SaveString()
    {
        // Get the string from the input field and save it
        string stringValue = stringInputField.text;
        SimpleSaveSystem.Set("playerString", stringValue);
        Debug.Log($"String saved: {stringValue}"); // Log the saved string value
    }

    // Method to load and display the saved integer value
    private void LoadInt()
    {
        // Get the saved value from SimpleSaveSystem
        string loadedString = SimpleSaveSystem.Get<string>("playerInt", string.Empty);

        // If the value is valid, display it; otherwise, show a message
        if (!string.IsNullOrEmpty(loadedString) && long.TryParse(loadedString, out long loadedInt))
        {
            intText.text = loadedInt.ToString();
        }
        else
        {
            intText.text = "No saved value for Int."; // Display message if no value found
        }
    }

    // Method to load and display the saved float value
    private void LoadFloat()
    {
        // Get the saved value from SimpleSaveSystem
        float loadedFloat = SimpleSaveSystem.Get<float>("playerFloat", default);

        // Display the value if it's valid, otherwise show a message
        floatText.text = loadedFloat != default ? $"{loadedFloat}" : "No saved value for Float.";
    }

    // Method to load and display the saved string value
    private void LoadString()
    {
        // Get the saved value from SimpleSaveSystem
        string loadedString = SimpleSaveSystem.Get<string>("playerString", string.Empty);

        // Display the value if it's valid, otherwise show a message
        stringText.text = !string.IsNullOrEmpty(loadedString) ? $"{loadedString}" : "No saved value for String.";
    }
}