public class Item
{
    public int Id { get; private set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public User Owner { get; private set; } = null!;
    public int OwnerId { get; private set; }
    public User? Borrower { get; private set; }
    public int? BorrowerId { get; private set; }

    private bool isAvailable => BorrowerId == null;

    private Item() { } // EF Core

    public Item(string name, string description, int ownerId)
    {
        Name = name;
        Description = description;
        OwnerId = ownerId;
    }

    public bool Borrow(User user)
    {
        if (!isAvailable)
        {
            return false;
        }

        Borrower = user;
        BorrowerId = user.Id;
        return true;
    }

    public bool Return(int borrowerId)
    {
        if (BorrowerId != borrowerId)
        {
            return false;
        }

        Borrower = null;
        BorrowerId = null;
        return true;
    }
}