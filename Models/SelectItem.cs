namespace grade_tracker.Models;

public class SelectItem
{
    public SelectItem(int id, string label)
    {
        Id = id;
        Label = label;
    }

    private int Id { get; set; }
    private string Label { get; set; }
}