using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Tickets")]
    public Button ticket2Button;
    public Button ticket3Button;

    [Header("Audio")]
    public AudioClip sceneLoop;
    private AudioSource audioSource;

    [Header("Notes (Parent Objects)")]
    public GameObject[] notes;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource != null && sceneLoop != null)
        {
            audioSource.clip = sceneLoop;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    void Start()
    {
        // Hide all notes when the menu scene starts
        for (int i = 0; i < notes.Length; i++)
        {
            notes[i].SetActive(false);
        }

        // Ticket unlock checks
        bool level1Completed = PlayerPrefs.GetInt("Level1Completed", 0) == 1;
        bool level2Completed = PlayerPrefs.GetInt("Level2Completed", 0) == 1;

        ticket2Button.interactable = level1Completed;
        ticket3Button.interactable = level2Completed;
    }

    public void GoToStartScreen()
    {
        CloseAllNotes();
        SceneManager.LoadScene("TitleScreen");
    }

    public void OpenNote(int noteIndex)
    {
        if (noteIndex < 0 || noteIndex >= notes.Length)
        {
            Debug.LogWarning("Invalid note index!");
            return;
        }

        CloseAllNotes();
        notes[noteIndex].SetActive(true);
    }

    public void StartLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void CloseAllNotes()
    {
        for (int i = 0; i < notes.Length; i++)
        {
            notes[i].SetActive(false);
        }
    }
}