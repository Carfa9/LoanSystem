using System.ComponentModel.DataAnnotations;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public int OwnerId { get; set; }
    public int BorrowerId { get; set; }
}