using System;
using System.Text;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Program
{
    partial class Lab_6
    {
        static Order[] FillStructs(int numberOfStructs)
        {
            Order[] ord = new Order[numberOfStructs];
            for (int i = 0; i < numberOfStructs; i++)
            {
                Console.WriteLine("Enter payment account of the payer and reciever and enter the amount");
                string line = Console.ReadLine();
                ord[i] = new Order(line);
            }
            return ord;
        }
        //static Order[] SortStructs(Order[] array)
        //{
        //    Array.Sort<Order>(array, (x, y) => x.paymentAccPay.CompareTo(y.paymentAccPay));
        //    return array;
        //}
        static void WriteStructsInFile(Order[] array)
        {
            try
            {
                StreamWriter sw = new StreamWriter("H:\\uni\\c#\\Lab6\\Lab6\\bin\\Debug\\net6.0\\file.txt");
                for (int i = 0; i < array.Length; i++)
                {
                    sw.WriteLine($"{array[i].paymentAccPay} {array[i].paymentAccRec} {array[i].amount}");
                }
                sw.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }            
        }
        static void WriteStructsInJSON(Order[] array)
        {
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Order[]));

            using (FileStream fs = new FileStream("order.json", FileMode.OpenOrCreate))
            {
                js.WriteObject(fs, array);
            }
        }
        static void WriteStructsInXml(Order[] array)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Order[]));

            using (FileStream fs = new FileStream("order.xml", FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, array);
            }
            
            //using (FileStream fs = new FileStream("order.xml", FileMode.OpenOrCreate))
            //{
            //    for (int i = 0; i < array.Length; i++)
            //    {
            //        xmlSerializer.Serialize(fs, array[i]);
            //    }
            //}
        }
        static Order[] ReadXmlData()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Order[]));

            using (FileStream fs = new FileStream("order.xml", FileMode.OpenOrCreate))
            {
                Order[] order = xmlSerializer.Deserialize(fs) as Order[];

                return order;
            }
        }
        static Order[] ReadJsonData()
        {
            using (FileStream fs = new FileStream("order.json", FileMode.OpenOrCreate))
            {
                DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(Order[]));

                Order[] order = ds.ReadObject(fs) as Order[];
                return order;
            }
        }
        static Order[] ReadTxtData()
        {
            string[] lines = File.ReadAllLines("file.txt");
            Order[] order = new Order[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {                                
                order[i] = new Order(lines[i]);
            }
            return order;
        }
        static void FindingPayAcc(Order[] order)
        {
            Console.WriteLine("Enter payment account of the payer you want to find");
            string acc = Console.ReadLine();

            bool flag = true;
            for (int i = 0; i < order.Length; i++)
            {
                if (order[i].paymentAccPay == acc)
                {
                    Console.WriteLine($"From {order[i].paymentAccPay} to {order[i].paymentAccRec} the amount of {order[i].amount / 100} hrn {order[i].amount % 100} cop");
                    flag = false;
                }
            }
            if (flag)
            {
                Console.WriteLine("Payment account of the payer have not been fould :(");
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("What file would you like to read?(1 - .xml, 2 - .txt, 3 - .json, 4 - write new data, 0 - exit)");
            int choice = int.Parse(Console.ReadLine());
            do
            {
                switch (choice)
                {
                    case 1:
                        FileStream fsx = new FileStream("order.xml", FileMode.OpenOrCreate);
                        if (fsx.Length == 0)
                        {
                            fsx.Close();
                            Console.WriteLine("File is empty");
                        }
                        else
                        {
                            fsx.Close();
                            FindingPayAcc(ReadXmlData());                            
                        }
                        break;
                    case 2:
                        FileStream fst = new FileStream("file.txt", FileMode.OpenOrCreate);
                        if (fst.Length == 0)
                        {
                            fst.Close();
                            Console.WriteLine("File is empty");
                        }
                        else
                        {
                            fst.Close();
                            FindingPayAcc(ReadTxtData());
                        }                        
                        break;
                    case 3:
                        FileStream fsj = new FileStream("order.json", FileMode.OpenOrCreate);
                        if (fsj.Length == 0)
                        {
                            fsj.Close();
                            Console.WriteLine("File is empty");
                        }
                        else
                        {
                            fsj.Close();
                            FindingPayAcc(ReadJsonData());
                        }                        
                        break;
                    case 4:
                        Console.WriteLine("Enter the number of structs:");
                        int structs = int.Parse(Console.ReadLine());
                        Order[] orders = FillStructs(structs);

                        Array.Sort(orders);
                        //orders = SortStructs(orders);
                        WriteStructsInFile(orders);
                        WriteStructsInXml(orders);
                        WriteStructsInJSON(orders);
                        break;
                    default:
                        Console.WriteLine("Wrong input format");
                        break;
                }
                Console.WriteLine("What file would you like to read?(1 - .xml, 2 - .txt, 3 - .json, 4 - write new data, 0 - exit)");
                choice = int.Parse(Console.ReadLine());
            } while (choice != 0);          
        }
    }
}