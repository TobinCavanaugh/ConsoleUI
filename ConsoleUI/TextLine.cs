namespace ConsoleUI;

public class TextLine
{
    public string text = "";
    public string guid = "";

    public TextLine(string text = "")
    {
        this.text = text;
        guid = Guid.NewGuid().ToString();
    }
}