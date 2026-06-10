public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
    {
        // TODO Start Problem 1
        if (value == Data)
            return;

        if (value < Data)
        {
            // Insert to the left
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        }
        else
        {
            // Insert to the right
            if (Right is null)
                Right = new Node(value);
            else
                Right.Insert(value);
        }
    }

    public bool Contains(int value)
    {
        if (value == Data)
            return true;

        if (value < Data)
        {
            if (Left is not null)
                return Left.Contains(value);
        }
        else
        {
            if (Right is not null)
                return Right.Contains(value);
        }
        
        return false;
    }

    public int GetHeight()
    {
        // TODO Start Problem 4
        int height;
        int leftHeight = 0;
        int rightHeight = 0;
        if (Left is not null)
            leftHeight = Left.GetHeight()+1;
        else
            leftHeight++;
        
        if (Right is not null)
            rightHeight = Right.GetHeight()+1;
        else
            rightHeight++;

        height = leftHeight >= rightHeight ? leftHeight : rightHeight;

        return height; // Replace this line with the correct return statement(s)
    }
}