using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace AddressBook
{
    class AdressBookMember
    {
        //свойства принято писать большой буквы
        public int number { get; set; }
        public string firstname { get; set; }
        public string secondname { get; set; }
        public int age { get; set; }
        public string telnumber { get; set; }

        public AdressBookMember()
        {

        }
        //параметры  лучше называть без кодовых значений 1,2 итд
        public AdressBookMember(int number1, string firstname1, string secondname1, int age1, string telnumber1)
        {
            number = number1;//Number=number
            firstname = firstname1;
            secondname = secondname1;
            age = age1;
            telnumber = telnumber1;
        }
        public void SaveDataInFile(AdressBookMember[] abm)
        {
            //когда есть вероятность ошибки, лучше использовать try
            File.Delete("data.txt");
            var sw = new StreamWriter("data.txt");
            string text = String.Empty;
            foreach (var item in abm)
            {
                text = item.number+";"+ item.firstname + ";" + item.secondname + ";" + item.age + ";" + item.telnumber + ";\r\n";
                sw.Write(text);
            }
            sw.Close();
            MessageBox.Show("File saved");
        }
        public void GetDataFromFile(string line)//данные читаются из строки, а метод назвается будто из файла
        {
            //слишком запутанно, много вложенностей это не очень хорошо.
            //по моему, можно все это было заменить на string[] arr = line.Split(";")
            int i = 0;
            int x = 0;
            string buf = String.Empty;
            while (i!=line.Length)
            {
                if(line[i] != ';')
                {
                    buf += line[i];
                    i++;
                }
                else
                {
                    switch(x)
                    {
                        case 0:
                            try {
                                this.number = Convert.ToInt32(buf);
                            }
                            catch(Exception)
                            {
                                MessageBox.Show("Wrong format data in number.\r\n Autochange to 0 in file", "Error");
                                this.number = 0;
                            }
                            buf = String.Empty;
                            x++;
                            i++;
                            break;
                        case 1:
                            this.firstname = buf;
                            buf = String.Empty;
                            x++;
                            i++;
                            break;
                        case 2:
                            this.secondname = buf;
                            buf = String.Empty;
                            x++;
                            i++;
                            break;
                        case 3:
                            try
                            {
                                this.age = Convert.ToInt32(buf);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Wrong format data in age.\r\n Autochange to 0 in file", "Error");
                                this.age = 0;
                            }
                            buf = String.Empty;
                            x++;
                            i++;
                            break;
                        case 4:
                            this.telnumber = buf;
                            buf = String.Empty;
                            x++;
                            i++;
                            break;
                    }
                }
            }
        }
    }
}
