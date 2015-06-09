using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace tritemius
{
    public static class TypeKey
    {
        public static int radioValue = 1;
    }
    class Tritemius : BaseClass
    {
        private int[] Key1 = new int[500];
        private string tempString;
        private const int AMOUNT_EN_CHAR = 52;
        private const int AMOUNT_RU_CHAR = 66;
        private const int AMOUNT_DEX = 29;
        private const string EN = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"; //52;
        private const string RU = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя"; // 66;
        private const string DEX = "0123456789!?><@#$%^&*(){}-+_="; //29;
        public override string Key
        {
            get
            {

                return _key;
            }

            set
            {

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

        public override string decrypt()
        {
            tempString = "";
            getKey(Key);
            int j = 0;
            foreach (char item in Text)
            {
                if (!substitution(item, AMOUNT_EN_CHAR, EN, false, j))
                {
                    if (!substitution(item, AMOUNT_RU_CHAR, RU, false, j))
                    {
                        if (!substitution(item, AMOUNT_DEX, DEX, false, j))
                        {
                            tempString += item;
                        }
                    }
                }

                j++;
            }
            Text = tempString;
            return Text;
        }

        public override string encrypt()
        {
            tempString = "";
            getKey(Key);
            int j = 0;
            bool bool1;
            foreach (char item in Text)
            {
                if (Key1[j]<0)
                {
                    bool1 = false;
                }
                else
                {
                    bool1 = true;
                }
                if (!substitution(item, AMOUNT_EN_CHAR, EN, bool1, j))
                {
                    if (!substitution(item, AMOUNT_RU_CHAR, RU, bool1, j))
                    {
                        if (!substitution(item, AMOUNT_DEX, DEX, bool1, j))
                        {
                            tempString += item;
                            
                        }
                    }
                }
                
                j++;
               
            }
            Text = tempString;
            return Text;
        }

        private bool substitution(char item, int amount, string listLanguage,bool encription,int iterator)
        {
            if (listLanguage.Contains(item))
            {
                if (encription)
                {


                    for (int i = 0; i < listLanguage.Length; i++)
                    {
                        if (item == System.Convert.ToChar(listLanguage[i]))
                        {
                            int z = (i + Key1[iterator]) % amount;
                            tempString += listLanguage[z];
                            Console.WriteLine("{0} {1} {2} {3} {4} {5}", item, Key1[iterator], i, tempString, z, listLanguage[z]);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < listLanguage.Length; i++)
                    {
                        if (item == System.Convert.ToChar(listLanguage[i]))
                        {
                            int z = (i - Key1[iterator]) % amount;
                            if (z < 0)
                            {
                                if (EN.Contains(item))
                                {
                                    z += 52;
                                }
                                if (RU.Contains(item))
                                {
                                    z += 66;
                                }
                                if (DEX.Contains(item))
                                {
                                    z += 29;
                                }
                            }
                            tempString += listLanguage[z];
                            Console.WriteLine("{0} {1} {2} {3} {4} {5}", item, Key1[iterator], i, tempString, z, listLanguage[z]);
                        }
                    }
                  
                }
                return true;

            }
            else
            {
                return false;
            }
        }




        private void getKey(string Key)
        {
            for (int j = 0; j < 500; j++)
            {
                Key1[j] = 0;
            }


            if (TypeKey.radioValue == 1)
            {
                while (Key.Length < Text.Length)
                {
                    Key += Key;
                }
                for (int i = 0; i < Text.Length; i++)
                {
                    for (int j = 0; j < EN.Length; j++)
                    {
                        if (Key[i]==EN[j])
                        {
                            Key1[i] = j;
                        }
                        
                    }
                    for (int j = 0; j < RU.Length; j++)
                    {
                        if (Key[i] == RU[j])
                        {
                            Key1[i] = j;
                        }

                    }
                }
            }
            if (TypeKey.radioValue == 2)
            {
                String[] st = new String[2];
                st = Key.Split(',');
                int k = int.Parse(st[0]);
                int b = int.Parse(st[1]);

                for (int i = 0; i < Text.Length; i++)
                {
                    Key1[i] = i * k + b;
                }
            }
            if (TypeKey.radioValue == 3)
            {
                string[] st = new string[3];
                st = Key.Split(',');
                int a = int.Parse(st[0]);
                int b = int.Parse(st[1]);
                int c = int.Parse(st[2]);
                for (int i = 0; i < Text.Length; i++)
                {
                    Key1[i] = a * i * i + b * i + c;
                }
            }
        }//Key1;
    }
}
