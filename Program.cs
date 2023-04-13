namespace huffman_coding;

internal class Program
{
    public static void Main()
    {
        /*
        ------------------   Завдання 1:   --------------------
        Прочитайте текстовий файл з диску та підрахуйте частоту символів у ньому.
        Можете взяти для цього цей файл - оповідання про Шерлока Холмса.
        Це  має бути зовсім не складно, для підрахунку вам може стати в нагоді
        словник char -> int (символ -> кількість його входжень)
        */
        var text_input_file = File.ReadAllText("C:/Users/rsp/RiderProjects/ConsoleApp7/ConsoleApp7/sherlock.txt");
     
        var dict_frequency = new Dictionary<char, int>();
   
        foreach (var ch in text_input_file)
        {

            dict_frequency[ch] = dict_frequency.ContainsKey(ch) ? dict_frequency[ch]+1 : 1;
        }

        List<Node> list_node = dict_frequency.Select(i => 
            new Node(i.Key, i.Value)).OrderBy(j => j.Frequency).ToList();
        
        

        Console.WriteLine("---------------   Frequency table   ------------");
        foreach (var i in list_node) Console.WriteLine("ch= "+i.Symbol+", freq= "+i.Frequency);
        
        /*
        --------------------   Завдання 2:   ------------------- 
        На основі отриманої таблиці частотності побудуйте дерево Хафмана. 
        Ми розглядали детально цей процес на лекції, можете також звернутись до цієї статті за описом,
        також достатньо гарно написано тут або на вікіпедії.
        */
        Dictionary<char, int> dict_code = new Dictionary<char, int>();
        dict_code = Huffman.TreeHuffmanCoding(list_node);
        
        /*
        Викоання завдання до цього етапу принесе вам 3 бали.
        Якщо замість черги ви зможете вплести в 3 пункт min heap, то отримаєте +2 додаткових бали.
        Маючи таблицю для кодування, ми тепер маємо її використати! Тут у вас є три варіанти:
        Простий (1 бал) Закодуйте оригінальний текст у новий текстовий файл,
        записуючи в нього безпосередньо 0 та 1 у текстовому форматі (зміст файлу буде мати
        вигляд типу 01010101101001010111). Прочитайте цей файл та розшифруйте його назад
        */
        string name_file_coded = @"C:/Users/rsp/RiderProjects/ConsoleApp7/ConsoleApp7/coded_sherlock.txt";
        Huffman.BinaryTextCoding(text_input_file, name_file_coded, dict_code);
        
        Huffman.BinaryTextDecoding(name_file_coded);

    }
}


