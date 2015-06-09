using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tritemius
{
    class Gamma :BaseClass //опиши клас
    {
        private string tempString;
        private int[] Key1 = new int[500];
        private List<int> decText = new List<int>();
        private const int AMOUNT_EN_CHAR = 148;

        private const string EN = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя0123456789!?><@#$%^&*(){}-+_= "; //29;
        public override string Key
        {
            get
            {
//прибери рядок
                return _key;
            }

            set
            {
//прибери пусті рядки
                _key = value;


            }
        }

        public override string Text
        {
            get
            {
                return _text;
            }

            set
            {
                _text = (String)value;
            }
        }

        public override string decrypt()//опиши що виконує даний метод
        {
            tempString = "";
            getKey(Key);
            int j = 0;
            string[] a =Text.Split(' ');
            decText.Clear() ;
            for(int qw=0;qw<a.Length-1;qw++)
            {
                decText.Add(int.Parse(a[qw]));
            }
            foreach (int item in decText)
            {
                
                if (!substitution1(item, AMOUNT_EN_CHAR, EN, j))
                {

                       //tempString += item;
                            MessageBox.Show("Символ не кодується: " + item);
//прибери лишні рядки

                }

                j++;

            }
            Text = tempString;
           

            return Text;
        }

        public override string encrypt()//опиши метод
        {

            tempString = "";
            getKey(Key);
            int j = 0;

            foreach (char item in Text)
            {
                if (!substitution(item, AMOUNT_EN_CHAR, EN,j))
                {
                   
                            //tempString += item;
                            MessageBox.Show("Символ не кодується: " + item);//проструктуруй рядок
     
                }

                j++;

            }
            Text = null;
            foreach (var item in decText)
            {
                Text += item.ToString()+' ';
            }
            
            return Text;
        }

        private void getKey(string key)//опиши
        {

            int st;
            if (Key.Length>5)
            {
                st = int.Parse(Key.Substring(0,5));
            }
            else
            {
                st = int.Parse(Key);
            }
            
//багато пропущено рядків

            Random rand;
            string st1 =null;
            for (int i = 0; i < Text.Length; i++)
                {
                    rand = new Random(st + i);
                    st1 = null;
                    st1 += rand.Next().ToString();
                    Key1[i] = int.Parse(st1.Substring(0,2));
                    Console.WriteLine("{0}",Key1[i]);
                }
            
        }

        private bool substitution(char item, int amount, string listLanguage,int iterator)// опиши що робить
        {
            if (listLanguage.Contains(item))
            {
                 for (int i = 0; i < listLanguage.Length; i++)
                    {
                        if (item == System.Convert.ToChar(listLanguage[i]))
                        {
                      
                            int z = i^Key1[iterator]; //не подобається мені назва змінної
                            decText.Add(z);
                      
                      //      Console.WriteLine("{0} {1} {2} {3} {4} {5}", i, iterator,Key1[iterator], z, tempString, listLanguage[z]);
                        }
                    }
               
                return true;

            }
            else
            {
                return false;
            }
        }
        private bool substitution1(int item, int amount, string listLanguage, int iterator)
        {
            int z = item ^ Key1[iterator];//назва змінної

            tempString += System.Convert.ToChar(listLanguage[z]);
                return true;

        }

    }
}
