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
    class DES : BaseClass
    {
        public string fileName;
        public override string Key
        {
           
            get
            {
                //лишній пустий рядок
                return _key;
            }

            set
            {
           string  tempkey=value;//змісти код рядка вправо на декілька символів
                if (tempkey.Length==8)
                {
                        _key = value;
                }
                else
                {
                    MessageBox.Show("Key length must be 8 symbols!");
                }
               //лишні пусті рядки


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

        public override string decrypt()//Опиши в коментаріях що робить кожен метод
        {
            string tmp = "";
            try
            {

                DESCryptoServiceProvider cryptic = new DESCryptoServiceProvider();
                cryptic.Key = Encoding.GetEncoding(1251).GetBytes(Key);
                cryptic.IV = Encoding.GetEncoding(1251).GetBytes(Key);
                FileStream fs = new FileStream(@fileName, FileMode.Open, FileAccess.Read);
                CryptoStream crStream = new CryptoStream(fs, cryptic.CreateDecryptor(), CryptoStreamMode.Read);

                StreamReader reader = new StreamReader(crStream, Encoding.GetEncoding(1251));
                 tmp = reader.ReadToEnd();
                fs.Close();
                reader.Close();
             }
            catch (Exception ex)
            {
                           
            }
           
            return tmp;

//прибери лишній рядок :)
        }

        public override string encrypt()//чекаю коментарів))
        {
            try
            {
                DESCryptoServiceProvider cryptic = new DESCryptoServiceProvider();
                cryptic.Key = Encoding.GetEncoding(1251).GetBytes(Key);
                cryptic.IV = Encoding.GetEncoding(1251).GetBytes(Key);
                FileStream fs = new FileStream(@fileName, FileMode.Create, FileAccess.Write);

                CryptoStream crStream = new CryptoStream(fs, cryptic.CreateEncryptor(), CryptoStreamMode.Write);
                byte[] data = Encoding.GetEncoding(1251).GetBytes(Text);
                crStream.Write(data, 0, data.Length);
                crStream.Close();
                fs.Close();
            }
            catch (Exception ex)
            {

                
            }
           
            return File.ReadAllText(@fileName, Encoding.GetEncoding(1251)); 
           
            //видали пусті рядки
           
        }
    }
}
