using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor.SceneManagement;

public class Unblock : MonoBehaviour
{
    public struct FileLine
    {
        public string nameDoor;
        public string state;

        public FileLine(string Name, string Unlock)
        {
            nameDoor = Name;
            state = Unlock;
        }
    }

    List<FileLine> lines;

    // Start is called before the first frame update
    void Start()
    {
        lines = LoadData();

        foreach (FileLine door in lines)
        {
            GameObject currentDoor = GameObject.Find(door.nameDoor);

            if (door.state == "lock")
                currentDoor.SetActive(false);
            else
                currentDoor.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static List<FileLine> LoadData()
    {
        List<FileLine> lines = new List<FileLine>();
        string line;
        StreamReader r = new StreamReader("Assets/Data/Doors.txt");

        using (r)
        {
            do
            {
                line = r.ReadLine();
                if (line != null)
                {
                    string[] lineData = line.Split(';');
                    FileLine lineEntry = new FileLine(lineData[0], lineData[1]);
                    lines.Add(lineEntry);
                }
            }
            while (line != null);
            r.Close();
        }

        return lines;
    }
}
