using HomeWork14;
using System.Security;
using System.Text.Json;
using System.Text.RegularExpressions;
/*
 * Создать консольное приложение, которое будет считывать данные из файла (фамилия, имя, телефон) из текстового файла doc.txt и конвертирует в файл doc.json
 */

class Program
{
    static string fileName = @"doc.txt";
    static string jsonFileName = @"doc.json";
    //Создаем список, в который будем помещать объекты из файла
    static List<Abonent> abonent = new List<Abonent>();

    public static void Main()
    {
        ReadFile(fileName);
        WriteJson(jsonFileName);
    }

    static void ReadFile(string fileName)
    {
        try
        {
            // Очищаем список, чтобы при повторном чтении файла не записать те же строки еще раз
            abonent.Clear();

            // Создаем массив строк и построчно выгружаем в него весь справочник
            string[] lines = File.ReadAllLines(fileName);

            // Перебираем каждую строку полученного массива и парсим из нее данные в поля объектов списка abonent
            foreach (var line in lines)
            {
                // разбиваем каждую строчку на отдельные слова и помещаем ее в массив, в котором должно оказаться 3 слова
                string[] sLine = line.Split(" ");

                // Проверяем, если в массиве меньше 3 слов - просто пропускаем эту строку и переходим к следующей 
                if (sLine.Length < 3)
                {
                    Console.WriteLine($"В строке {line} всего {sLine.Length} слова вместо 3х");
                    continue;
                }

                // Передаем полученные элементы массива полям объекта (по порядку: сначала Фамилия, потом Имя, потом телефон) и сразу добавляем его в список
                // На null не проверяем - класс к конструкторе проверит, если там ничего нет - задаст пустое поле
                abonent.Add(new Abonent(surname: sLine[0], name: sLine[1], tel: sLine[2]));

            }
        }
        // Ловим ошибки, если они были, и выводим на экран консоли
        catch (Exception ex)
        { Console.WriteLine($"Ошибка: {ex.Message}"); }

        // (не обязательно) читаем данные, полученные из файла
        Console.WriteLine($"Из файла {fileName} получены следующие данные:\n");
        foreach (var item in abonent)
        {
            Console.WriteLine($"Фамилия: {item.Surname}");
            Console.WriteLine($"Имя: {item.Name}");
            Console.WriteLine($"Телефон: {item.Tel}");
            Console.WriteLine();
        }
    }

    static void WriteJson(string fileName)
    {
        // создаем список строк, в который будем передавать строки, которые будут записаны  в json-файл
        List<string> jsonLine = new List<string>();
        // перебираем все строки нашего списка абонентов, который мы получили из файла .txt
        foreach (var item in abonent)
            // добавляем новую строку в список строк для json файла, которую сразу Сериализуем и Преобразуем в читаемый вид
            jsonLine.Add(Regex.Unescape(JsonSerializer.Serialize<Abonent>(item)));

        // Записываем  в файл json весь полученный нами список
        File.WriteAllLines(jsonFileName, jsonLine);
    }
}



