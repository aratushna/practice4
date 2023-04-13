namespace huffman_coding;

public class Node
{
    public char Symbol {get; set;}
    public int Frequency {get; set;}
    public Node Left_offset {get; set;}
    public Node Right_offset {get; set;}
    
    public Node (char symbol, int frequency)
    {
        Symbol = symbol;
        Frequency = frequency;
    }
    
    public Node (int frequency, Node left_offset, Node right_offset)
    {
        Frequency = frequency;
        Left_offset = left_offset;
        Right_offset = right_offset;
    }
    
    
    /*
    ------------------   Завдання 4:   ---------------------------
    Маючи дерево, складіть та виведіть на екран таблицю з кодуванням для кожного символа.
    Для того щоб отримати код символу, потрібно поступово спускатись з кореня дерева до потрібної вершини,
    запам'ятовуючи всі 0 та 1 на шляху
    */
    

    public List<int> Binarization(char ch_search, List<int> input_record_int)
    {
        if (Right_offset == null && Left_offset == null)
        {
        
            return (ch_search == this.Symbol) ? input_record_int : null;
        }
        else
        {
            List<int> record_left = null;
            List<int> record_right = null;

            if (Left_offset != null)
            {
                List<int> temp_left = new List<int>();
                temp_left.AddRange(input_record_int);
                
                temp_left.Insert(0, 1);
                
                record_left = Left_offset.Binarization(ch_search, temp_left);
            }

            if (Right_offset != null)
            {
                List<int> temp_right = new List<int>();
                temp_right.AddRange(input_record_int);
                
                temp_right.Insert(0, 0);
                
                record_right = Right_offset.Binarization(ch_search, temp_right);
            }
            return (record_left != null) ? record_left : record_right;
        }
    }
}