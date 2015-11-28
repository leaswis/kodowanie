using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Concurrent;

namespace kodowanieM
{
    class Program
    {
        static void Main(string[] args)
        {

            string path = @"C:\Users\Laura\Desktop\pr.txt";
            //string path = @"C:\Users\Laura\Desktop\obrazek.bmp";

            byte[] blockInBytes = getBytesFromFile(path);

           // try
            //{
                List<byte> values = new List<byte>();
                for (int i = 0; i < blockInBytes.Length; i++)
                {
                    if (!(values.Contains(blockInBytes[i])))
                    {
                        values.Add(blockInBytes[i]);

                    }
                }

                byte[] checkBlock = values.ToArray();
                int checkSize = values.Count;

                Console.WriteLine("ZAKRES WARTOSCI");
                foreach(byte b in values)
                {

                    Console.Write(b);
                    Console.Write(" , ");
                }
                Console.WriteLine("\n\n\n");
               /*Console.WriteLine("\n\n\n\nZLICZENIE");
                var count = StatisticsBytes.Count(blockInBytes);

                foreach(var c in count)
                {
                    Console.Write(c.Key);
                    Console.Write(" ; ");
                    Console.Write(c.Value);
                    Console.Write("\n");

                }*/

                List<byte> prob = new List<byte>();
                prob.Add(255); prob.Add(1); prob.Add(1); prob.Add(4);
                prob.Add(4); prob.Add(4); prob.Add(4); prob.Add(4);
                prob.Add(200); prob.Add(15); prob.Add(10); prob.Add(4);
                prob.Add(1);  prob.Add(1); prob.Add(1); prob.Add(1);
                prob.Add(1); prob.Add(1); prob.Add(1); prob.Add(1);
                prob.Add(1);
                for (int i = 0; i < 260; i++ )
                {
                    prob.Add(2);
                }
                    prob.Add(9);

                List<byte> values2 = new List<byte>();
                for (int i = 0; i < prob.Count; i++)
                {
                    if (!(values2.Contains(prob.ToArray()[i])))
                    {
                        values2.Add(prob.ToArray()[i]);

                    }
                }

                byte[] checkBlock2 = values2.ToArray();
                int checkSize2 = values.Count;

                /*Console.WriteLine("\n\nZAKRES WARTOSCI");
                foreach (byte b in values2)
                {

                    Console.Write(b);
                    Console.Write(" , ");
                }
                Console.WriteLine("\n\nlista");
                foreach (byte b in prob)
                {

                    Console.Write(b);
                    Console.Write(" , ");
                }*/

                splitBlocks(blockInBytes, checkBlock, checkSize);
               // String test = String.Join(" ", encode(prob));
               // String test2 = String.Join(" ", decode(encode(prob)));

                Console.WriteLine("\n\n\n\n");
               // Console.WriteLine(test);

                Console.WriteLine("\n\n\n\n");
              //  Console.WriteLine(test2);
               // decode(encode(prob));

             
           // }
            //catch(Exception e1)
            //{
              //  throw (e1);
            //}


            Console.ReadLine();
        }

        //wczytanie pliku do tablicy bajt.
        public static byte[] getBytesFromFile(string fullFilePath)
        {

            FileStream fs = null;
            try
            {
                fs = File.OpenRead(fullFilePath);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                return bytes;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }

        //zapisz tablicę b. do pliku
        public static void backToFile(byte[] bytes, string path)
        {
            using (Stream file = File.OpenWrite(path))
            {
                file.Write(bytes, 0, bytes.Length);
            }
        }

        //dzielenie na podciągi
        public static void splitBlocks(byte[] blockInBytes, byte[] checkBlock, int checkSize)
        {
            List<byte> buffer = new List<byte>();
            List<byte> checkValue = new List<byte>();


           int index = 0;

              while (true)
                {
                

                    if ((checkBlock.Contains(blockInBytes[index])))
                    {

                        buffer.Add(blockInBytes[index]);

                        if (!(checkValue.Contains(blockInBytes[index])))
                        {
                            checkValue.Add(blockInBytes[index]);
                        }

                   }

                    index++;
                    
                  if(checkValue.Count >= checkSize) { break; }

                }

              String res = String.Join(" ", encode(buffer));

              Console.WriteLine(res);
              Byte m = 10;
             String res2 = String.Join(" ", decode(encode(buffer), buffer));

              Console.WriteLine("\n\n\n\n");
              Console.WriteLine(res2);

             // compressBlock(buffer);
      
         /* Console.WriteLine("\nBUFOR ile   ");
          Console.Write(buffer.Count);
            Console.WriteLine("\n\n\n\nBUFOR\n");
            foreach(byte v in buffer)
            {
                Console.Write(v);
                Console.Write(" , ");
            }

             Console.WriteLine("\n\n\nCHECKVALUE\n");
            foreach(byte v in checkValue)
            {
                Console.Write(v);
                Console.Write(" , ");
            }*/

        }

        public static IEnumerable<Byte> decode(IEnumerable<byte> source, List<byte> buffer)
        {
            if (null == source)
                throw new ArgumentNullException("source");

            Byte marker = buffer.Last();
            Byte current = 0;
            int count = 0;
            int indeks = 0;

            Byte tmp1 = 0;
            Byte tmp2 = 0;

            List<byte> temp = new List<byte>();
            
                while(true)
                {
                    
                    current = source.ElementAt(indeks);
                    if (source.ElementAt(indeks) == marker)
                    {
                        count = source.ElementAt(indeks + 1);
                        current = source.ElementAt(indeks + 2);
                        tmp1 = source.ElementAt(indeks+2);
                       
                            for (int i = 0; i < count-1; ++i)
                            {

                                yield return current;
                                
                            }

                    }
                    else if (source.ElementAt(indeks) != count /*&& source.ElementAt(indeks) != tmp1*/)
                    {
                        
                        yield return current;
                        
                    
                   }
                    
              

                indeks += 1;
                    if(indeks + 2 > source.Count())
                    {
                        break;
                    }

        }
                
               // Console.WriteLine("\n\n\n");
               /* foreach(var v in temp)
                {
                    Console.Write(v);
                    Console.Write(" ");
                }*/

        }
      
        public static IEnumerable<Byte> encode(IEnumerable<Byte> source)
        {
            if (null == source)
                throw new ArgumentNullException("source");

            const int threshold = 3;

            Byte marker = source.Last();
            Byte current = 0;
            int count = 0;

            foreach (var item in source)
                if ((count == 0) || (current == item))
                {
                    current = item;
                    count += 1;
                }
                else
                {
                    if (count <= threshold)
                        for (int i = 0; i < count; ++i)
                            yield return current;
                    else
                    {
                        for (int i = 0; i < count / Byte.MaxValue; ++i)
                        {
                            yield return marker;
                            yield return Byte.MaxValue;
                            yield return current;
                        }

                        if (count % Byte.MaxValue != 0)
                        {
                            yield return marker;
                            yield return (Byte)(count % Byte.MaxValue);
                            yield return current;
                        }
                    }

                    current = item;
                    count = 1;
                }

            // Tail
            if (count <= threshold)
                for (int i = 0; i < count; ++i)
                    yield return current;
                    
            else
            {
                for (int i = 0; i < count / Byte.MaxValue; ++i)
                {
                    
                    yield return Byte.MaxValue;
                    yield return current;
                }

                if (count % Byte.MaxValue != 0)
                {
                    
                    yield return (Byte)(count % Byte.MaxValue);
                    yield return current;
                }
            }
        }
    }
    }

