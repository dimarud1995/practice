using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.IO;

namespace tritemius
{
    static class transport
    {
       static public string temp;
        static public string T;
        static public string P;
    } //я б радила зробити відступ між класами
    class Bag : BaseClass
    {
        public int m, t;// цікаві імена для змінних, а головне - несуть велику інформованість :) 
        int t1;
        int[] tempKeyS;
        int[] tempKeyP;
        public string st;
        public override string Key
        {

            get
            {//я б прибрали пустий рядок нижче 
            
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


        public override string decrypt()//Діма, допиши коментарії до свої методів (що кожен робить)
        {
            string tempString = "";
            string[] tempDec = Text.Split(' ');
            int[] tempDecInt = new int[tempDec.Length];
            int i = 0;
            tempKeyS = getSecretKey();
            int len = tempKeyS.Length;
            tempKeyS = getSecretKey();
           // m = getM(tempKeyS);
         
            transport.P = m.ToString();
            t1 = getT1(m, t);// + змінити назву змінної
            transport.T = t1.ToString();
            Console.WriteLine("P={0}  T={1}", m, t1);
            try
            {
                foreach (string item in tempDec)
                {

                    tempDecInt[i] = (int.Parse(item) * t1) % m;
                    i++;

                }
            }
            catch (Exception ex)
            {
            }

            int tempValue;
            string tString = "";
            byte[] tempByte = new byte[tempString.Length / 8];
            for (int j = 0; j < tempDecInt.Length; j++)
            {
                tString = "";
                tempValue = tempDecInt[j];
                for (int k = len - 1; k >= 0; k--)
                {
                    if (tempKeyS[k] <= tempValue)
                    {
                        tempValue -= tempKeyS[k];
                        tString = "1" + tString;
                    }
                    else
                    {
                        tString = "0" + tString;
                    }
                }
                tempString += tString;
            }
           

           string a = substitution(tempString);
            return a;
        }

        private static string substitution(string tempString)
        {
            tempString = tempString.Substring(0, tempString.Length - (tempString.Length % 8));
            int l = 0;
            string tempChar = "";
            int tempDecChar = 0;
            string tempTempString = "";
            int edge=0;
            byte[] tempByte = new byte[tempString.Length];
            if (tempString.Length<=8)
            {
                edge = 9;
            }
            else
            {
                edge = tempString.Length;
            }
            for (int j = 0; j < edge; j++)
            {
                if (l < 8)
                {
                    tempChar += tempString[j];
                    l++;
                }
                else
                {
                      tempDecChar = 0;
                      for (int i = 0; i < 8; i++)
                      {
                          tempDecChar += int.Parse((Math.Pow(2, 7-i) * double.Parse(tempChar[i].ToString())).ToString());
                      }
                      tempTempString += Convert.ToChar(tempDecChar).ToString();
                   // tempByte[j] = Convert.ToByte(tempChar,2);
                    if (edge==9)
                    {
                        break;
                    }
                    tempChar = "";
                    
                    l = 0;
                    j--;
                }

            }
           
              //  tempTempString = Encoding.GetEncoding(1251).GetString(tempByte);
           
            return tempTempString;
        }

        public override string encrypt()
        {
            tempKeyS = getSecretKey();
           // m = getM(tempKeyS);
            tempKeyP = getKey(tempKeyS, m, t);
            
            st = "";
            foreach (var item in tempKeyP)
            {
                st += " " + item.ToString();
            }
            transport.temp = st;
            string tempString = "";
            StringBuilder sb = new StringBuilder();
            foreach (byte b in System.Text.Encoding.GetEncoding(1251).GetBytes(Text))
                sb.Append(Convert.ToString(b, 2).PadLeft(8, '0'));

            string binaryStr = sb.ToString();
            int len = tempKeyP.Length;
            int endZero = binaryStr.Length % len;
            for (int i = 0; i < len - endZero; i++)
            {
                binaryStr += "0";
            }
            int amountBlocks;
            if (binaryStr.Length % len != 0)
            {
                amountBlocks = binaryStr.Length / len + 1;
            }
            else
            {
                amountBlocks = binaryStr.Length / len;
            }

            string[] masBlockText = new string[amountBlocks];
            int bcount = 0;
            int dcount = 0;
            Console.WriteLine("{0}", binaryStr);
            for (int i = 0; i < binaryStr.Length; i++)
            {
                if (bcount != len)
                {
                    masBlockText[dcount] += binaryStr[i];
                    bcount++;
                }
                else
                {
                    Console.WriteLine("{0}", masBlockText[dcount]);
                    dcount++;
                    bcount = 0;
                    i--;

                }
            }


            return substitution(tempString, len, masBlockText);
        }

        public int getM(int[] key)
        {
            for (int i = 0; i < key.Length; i++)
            {
                m += key[i];
            }
            for (;;)
            {
                if (m % t == 1)
                {
                    m += 1;
                }
                else
                {
                    break;
                }

            }
            return m;
        }

        private string substitution(string tempString, int len, string[] masBlockText)
        {
            for (int i = 0; i < masBlockText.Length; i++)
            {

                int tempNumber = 0;
                for (int j = 0; j < len; j++)
                {
                    if (masBlockText[i][j] == '1')
                    {
                        tempNumber += tempKeyP[j];
                    }
                }
                tempString += tempNumber.ToString() + " ";
            }

            return tempString;
        }

        public int[] getSecretKey()
        {
           string[] tempKey = Key.Split(',');
            int[] tempTempKey=new int[tempKey.Length];

            for (int i = 0; i < tempKey.Count(); i++)
            {
                tempTempKey[i]=int.Parse(tempKey[i]);
            }
            return tempTempKey;
        }
        public int[] getKey(int[] key, int m,int t)
        {
            int[] tempTempKey = new int[key.Length];
            for (int i = 0; i < key.Length; i++)
            {
                tempTempKey[i] = (key[i] * t) % m;
                Console.WriteLine("{0} {1}", tempTempKey[i], key[i]);
            }
        
            return tempTempKey;
        }

        private int getT1(int m,int t)
        {
           int t1 = 1;
            for (; 
                (t * t1) % m != 1; t1++) ;
            Console.WriteLine("{0} {1} {2}", m, t, t1);
            return t1;
        }

     
    }
}

// там де є пусті непотрібні рядочки - повидаляй)
