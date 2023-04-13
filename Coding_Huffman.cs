//using System.Xml.Schema;

namespace huffman_coding;

public class Huffman
{
    private static void HeapSortedByFrequency(List<Node> heap)
    {
       
        List<Node> heap_sorted = heap.OrderBy(x => x.Frequency).ToList();
        heap = heap_sorted;
    }
 
    /*
    ---------------   Завдання 3:   ------------------
    Якщо коротко: створіть чергу з вузлів, що представляють символи та їх частоту в тексті,
    далі ітеративно діставайте з черги два найменших елементи та додавайте у чергу один новий елемент,
    що має два попередніх за дочірні та значення, що має суму значень доічрніх елементів.
    Так, крок за кроком ви побудуєте дерево, після чого пронумеруйте для кожної вершини шлях наліво - нулем,
    направо - одиницею (або навпаки, це не так принципово).
    */
    public static List<Node> TreeHuffman(List<Node> heap)
        
    {
        var tree = new List<Node>(heap);
      
        while (tree.Count > 1)
        {
            
            HeapSortedByFrequency(tree);
            
            var node_1 = tree[0];
            tree.Remove(node_1);
            var node_2 = tree[0];
            tree.Remove(node_2);

           
            var node_new = new Node( node_1.Frequency + node_2.Frequency, node_1, node_2);
       
            tree.Add(node_new);
            
            HeapSortedByFrequency(tree);
        }
        return tree;
    }
        
    /*
    -------------------   Завдання 4:   -----------------------
    Маючи дерево, складіть та виведіть на екран таблицю з кодуванням для кожного символа.
    Для того щоб отримати код символу, потрібно поступово спускатись з кореня дерева до потрібної вершини,
    запам'ятовуючи всі 0 та 1 на шляху
    */
    public static Dictionary<char, int> TreeHuffmanCoding(List<Node> heap)
    {
        Dictionary<char, int> dict_code = new Dictionary<char, int>();
        
        var tree_base = TreeHuffman(heap)[0];
        Console.WriteLine("-------------  Encoding table for each character   -----------   ");
        foreach (var node in heap)
        {
            char ch_symbol = node.Symbol;
            
            List<int> list_int = tree_base.Binarization(ch_symbol, new List<int>());

            
            
            Console.Write("ch= "+ch_symbol+ ", int= ");
            foreach (var i in list_int) Console.Write(i);
            Console.WriteLine();
            
            
            
            int int_code = int.Parse(string.Join(",",list_int).Replace(",", ""));


            
            Console.Write("ch= "+ch_symbol+ ", int= " + int_code);
            
            Console.WriteLine();
            
            
            
            dict_code[ch_symbol] = int_code;
        }
        return dict_code;
    }

    
    /*
    ------------------      Додаткових бали   ---------------------------------------
    Більш правдоподібний, але все ще текстовий (1 + 1 додатковий бали).
    Для успішного розшифрування файлу нам потрібно мати таблицю кодування.
    У минулому пункті ми її могли просто перевикористати,
    але у повноцінному архіві вся інформація для разархівування має бути у самому файлі - подумайте як,
    та додайте до файлу таблицю для декодування.
    */
    
    public static void BinaryTextCoding(string text_input_file, string name_file_coded,
            Dictionary<char, int> dict_coding)
    
    {
        
        File.Delete(name_file_coded);
        
        Console.WriteLine("----------   Decoding table   --------------");
        Console.WriteLine("table_code.Key + = + table_code.Value + |");
        
        foreach (var table_code in dict_coding)
        {
            string content = table_code.Key + "=" + table_code.Value + "|";
            File.AppendAllText(name_file_coded,content);
            
            
            Console.WriteLine(content);
            
        }
        /
        File.AppendAllText(name_file_coded, "~");
        
        Console.WriteLine("----------   Сoded text in binary form   --------------");
        foreach (char ch in text_input_file)
        {
            
            string content_binary = dict_coding[ch].ToString()+"|";
            
            File.AppendAllText(name_file_coded, content_binary);
            
            
            Console.Write(content_binary);
            
        }
    }

    /*
    ------------------      Додаткових бали   ---------------------------------------
    Цифровий (1 + 2 додаткових бали). Писати текстовий файл з нулями та одиницями нераціонально - символ 0,
    це такий же символ як і a чи b, і отриманий файл буде тільки більше в розмірі.
    Але ж якщо a кодується як 1100, то це ж просто двійкове представлення числа 12!
    Давайте спробуємо писати у файл не текст, а байти - один байт = код одного символу.
    Не забудьте про таблицю кодування!
    */
    public static void BinaryTextDecoding(string name_file_decoded)
    {
        var file_decoding = File.ReadAllText(name_file_decoded);
        
        var str_decoding = file_decoding.Split("~");
        var str_decoding_table = str_decoding[0].Split("|");
        var str_decoding_text = str_decoding[1].Split("|");
        var dict_decoding = new Dictionary<int, char>();
        for (int i = 0; i < str_decoding_table.Length-1; i++)
        {
            
            var str_temp = str_decoding_table[i].Split("=");
            
            dict_decoding.TryAdd(Convert.ToInt32(str_temp[1]), Convert.ToChar(str_temp[0]));
        }
        
       
        Console.WriteLine();
        Console.WriteLine("--------------   Decoded text    --------------------");
        foreach (var i in str_decoding_text)
        {
            Console.Write(dict_decoding[Convert.ToInt32(i)]);
        }
    }
}