using System.Collections.Generic;

public class DialogLine
{
    public string Name { get; set; }
    public string Line { get; set; }

    public DialogLine(string name, string line)
    {
        Name = name;
        Line = line;
    }
}

//[System.Serializable]
public class Dialog
{
    public List<DialogLine> DialogLines { get; set; } = new List<DialogLine>();

    public void AddLine(string name, string line)
    {
        DialogLines.Add(new DialogLine(name, line));
    }
}
