using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Notes (Parent Objects)")]
    public GameObject[] notes;

    void Start()
    {
        // Hide all notes when the menu scene starts
        for (int i = 0; i < notes.Length; i++)
        {
            notes[i].SetActive(false);
        }
    }
    // Quit button hooked to lead players to title/start screen 
    public void GoToStartScreen()
    {
        CloseAllNotes();
        SceneManager.LoadScene("TitleScreen");
    }

    // Called by clicking a file icon
    public void OpenNote(int noteIndex)
    {
        // Safety check
        if (noteIndex < 0 || noteIndex >= notes.Length)
        {
            Debug.LogWarning("Invalid note index!");
            return;
        }

        // Close all notes first
        for (int i = 0; i < notes.Length; i++)
        {
            notes[i].SetActive(false);
        }

        // Open the selected note (children included)
        notes[noteIndex].SetActive(true);
    }

    // Called by the Start Dream button
    public void StartLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Optional: Close all notes (useful if you add an X button later)
    public void CloseAllNotes()
    {
        for (int i = 0; i < notes.Length; i++)
        {
            notes[i].SetActive(false);
        }
    }
}