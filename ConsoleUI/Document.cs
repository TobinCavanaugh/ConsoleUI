namespace ConsoleUI;

public class Document
{
    public List<TextLine> lines = new();

    public Document()
    {
        lines.Add(new TextLine());
    }
}