using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AddressBook
{
    public partial class Form1 : Form
    {
        int CountLines(int count)
        {
            try
            {
                StreamReader testfile = new StreamReader("data.txt");
                string line = String.Empty;
                while ((line = testfile.ReadLine()) != null)
                {
                    count++;
                }
                testfile.Close();
                return count;
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show(e.Message, "File not found");
                File.Create("data.txt").Close();
                MessageBox.Show("\r\n File data.txt be created", "File be created");
                return 0;
            }
            catch (FileLoadException e)
            {
                MessageBox.Show(e.Message);
                return 0;
            }
        }//метод подсчета строк в файле
        AdressBookMember[] Members = new AdressBookMember[0];//объявления массива объектов класса AdressBookMember
        // Инициализация формы
        public Form1()
        {
            InitializeComponent();
            int count=0;
            count = CountLines(count);
            if (count!=0)
            {
                for (int j = 0; j < count; j++)
                {
                    try
                    {
                        Members[j] = new AdressBookMember();
                    }
                    catch(Exception)
                    {
                        Array.Resize(ref Members, Members.Length + 1);
                        Members[j] = new AdressBookMember();
                    }
                }
                int i = 0;
                var lines = File.ReadLines("data.txt");
                foreach (var line in lines)
                {
                    Members[i].GetDataFromFile(line);
                    dataGridView1.Rows.Add(Members[i].number.ToString(), Members[i].secondname, Members[i].firstname, Members[i].age, Members[i].telnumber);
                    i++;
                }
            }
        }
        //Обработка события нажатия на кнопку "Save"
        private void SaveBut_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 1) {
                File.Delete("data.txt");
                File.Create("data.txt").Close();
                MessageBox.Show("File saved");
                return;
            }
            string str = String.Empty;
            for (int i = 0; i < dataGridView1.RowCount-1; i++)
            {
                for (int j = 0; j != 5; j++)
                {
                    try
                    {
                        str += dataGridView1.Rows[i].Cells[j].Value.ToString() + ";";
                    }
                    catch(Exception)
                    {
                        dataGridView1.Rows[i].Cells[j].Value = "0";
                        str += dataGridView1.Rows[i].Cells[j].Value + ";";
                    }
                }
                try
                {
                    Members[i].GetDataFromFile(str);
                }
                catch(Exception)
                {
                    Array.Resize(ref Members, Members.Length + 1);
                    Members[i] = new AdressBookMember();
                    Members[i].GetDataFromFile(str);
                }
                Array.Resize(ref Members, dataGridView1.RowCount - 1);
                str = String.Empty;
            }
            Members[0].SaveDataInFile(Members);
        }
        //Обработка события нажатия на кнопку "Delete"
        private void DeleteBut_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in dataGridView1.SelectedRows)
            {
                try
                {
                    dataGridView1.Rows.RemoveAt(item.Index);
                }
                catch(Exception e1)
                {
                    MessageBox.Show(e1.Message,"Error");
                    return;
                }
            }
        }
        //Обработка события добавления новой строки в dataGridView1
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dataGridView1.Rows[dataGridView1.RowCount-1].Cells[0].Value = dataGridView1.RowCount;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            dataGridView1.Width = this.Width-120;
            dataGridView1.Height = this.Height-37;
            SaveBut.Location= new Point(this.Width-114, 12);
            DeleteBut.Location = new Point(this.Width - 114, 49);
        }
    }
}
