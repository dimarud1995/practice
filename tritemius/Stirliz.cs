using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace tritemius
{
    class Stirliz : BaseClass
    {
        private string tempString;
        private string[] tempKey;
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
            getKey();
            tempString = "";
            string[] tmpST=Text.Split(' ');
            int[] tmpInt =new int[tmpST.Count()-1];
            for (int i = 0; i < tmpInt.Count(); i++)
            {
                tmpInt[i] = int.Parse(tmpST[i]);
            }
            foreach (int item in tmpInt)
            {
                  tempString += substitution(item).ToString();
             }
            return tempString;
        }

        public override string encrypt()
        {
            getKey();
            tempString = "";
            int tmp;
            foreach (var item in Text)
            {
                tmp = substitution(item);
                if (tmp!=0)
                {
                    tempString += tmp.ToString() + " ";
                }
                
            }
            return tempString;
        }
        public void getKey()
        {
            tempKey = Key.Split(new char[] { '\n' },100);

            for (int i = 0; i < tempKey.Count(); i++)
            {

                if (tempKey[i].Count()>100)
                {
                    tempKey[i] = tempKey[i].Substring(0, 99);
                }
               
             //   Console.WriteLine(tempKey[i]);
            }
        }
        public int substitution(char character)
        {
            int code=0;
            List<string> codes = new List<string>();
            List<int> codeI = new List<int>();
            List<int> codeJ = new List<int>();
            for (int i = 0; i < tempKey.Length; i++)
            {
                for (int j = 0; j < tempKey[i].Length; j++)
                {
                    if (character==tempKey[i][j])
                    {
                        codes.Add(tempKey[i][j].ToString());
                        codeI.Add(i);
                        codeJ.Add(j);
                    }
                    
                }
            }
            
            if (codes.Count!=0)
            {
                Random rand = new Random();
                int k = int.Parse(rand.Next(codes.Count).ToString());
              //  Console.WriteLine("{0} {1}", int.Parse(rand.Next(100).ToString()) % codes.Count(), int.Parse(rand.Next(100).ToString()) % codes.Count());
                code = codeI[k] *100 + codeJ[k];
            }
            else
            {
                MessageBox.Show("Символ '" + character + "' не кодується"); 
            }
            
            if (code < 1000&&code!=0)
            {
                tempString += "0";
            }
            return code;
        }
        public string substitution(int code)
        {
             return tempKey[code/100][code%100].ToString();
        }
    }
}
