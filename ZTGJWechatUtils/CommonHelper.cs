using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTGJWechatUtils
{
    public static class CommonHelper
    {
        /// <summary>
        /// 根据文件地址解析json数据
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string Jsonstr(String filePath)
        {
            string strData = "";
            try
            {
                string line;
                // 创建一个 StreamReader 的实例来读取文件 ,using 语句也能关闭 StreamReader
                using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath, GetEncoding(filePath)))
                {
                    // 从文件读取并显示行，直到文件的末尾
                    while ((line = sr.ReadLine()) != null)
                    {
                        //Console.WriteLine(line);
                        strData += line;
                    }
                }
            }
            catch (Exception e)
            {
                // 向用户显示出错消息
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return strData;
        }

        /// <summary>
        /// 获取文件的编译方式
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Encoding GetEncoding(string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException("filePath");
            }
            Encoding encoding1 = Encoding.Default;

            try
            {
                using (FileStream stream1 = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    if (stream1.Length > 0)
                    {
                        using (StreamReader reader1 = new StreamReader(stream1, true))
                        {
                            char[] chArray1 = new char[1];
                            reader1.Read(chArray1, 0, 1);
                            encoding1 = reader1.CurrentEncoding;
                            reader1.BaseStream.Position = 0;
                            if (encoding1 == Encoding.UTF8)
                            {
                                byte[] buffer1 = encoding1.GetPreamble();
                                if (stream1.Length >= buffer1.Length)
                                {
                                    byte[] buffer2 = new byte[buffer1.Length];
                                    stream1.Read(buffer2, 0, buffer2.Length);
                                    for (int num1 = 0; num1 < buffer2.Length; num1++)
                                    {
                                        if (buffer2[num1] != buffer1[num1])
                                        {
                                            encoding1 = Encoding.Default;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    encoding1 = Encoding.Default;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.ErrorLog(ex.Message + "," + ex.StackTrace);
            }
            if (encoding1 == null)
            {
                encoding1 = Encoding.UTF8;
            }

            return encoding1;
        }


    }
}
