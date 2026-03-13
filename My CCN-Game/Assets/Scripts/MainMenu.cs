using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour

{
    
    [Header("Ticket 2")]
    public Button ticket2Button;
    
    [Header("Audio")]
    public AudioClip sceneLoop;
private AudioSource audioSource;
    
    [Header("Notes (Parent Objects)")]
    public GameObject[] notes;
void Awake()
{
    audioSource = GetComponent<AudioSource>();
    audioSource.clip = sceneLoop;
    audioSource.loop = true;
    audioSource.Play();
}

    void Start()
    {
        // Hide all notes when the menu scene starts
        for (int i = 0; i < notes.Length; i++)
        {
            notes[i].SetActive(false);
        }
        
        // Unlock Ticket 2 if Level 1 is done
        bool level1Completed = PlayerPrefs.GetInt("Level1Completed", 0) == 1;
        ticket2Button.interactable = level1Completed;
        
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