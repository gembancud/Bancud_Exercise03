
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Exercise03_A
{
    public class LineEditor
    {
        private LinkedList<string> _linkedList;

        private int _pointerIndex
        {
            get
            {
                int i = _linkedList.Size + 1;
                return i;
            }
        }

        private string _originalFileName;

        public LineEditor()
        {
            _linkedList = new LinkedList<string>();
            string input = Console.ReadLine();
            while (input != "E")
            {
                if (input.Contains("EDIT")) OpenSavedFile(input);
                else if (input[0] == 'I') InsertLines(input);
                else if (input[0] == 'D') DeleteLines(input);
                else if (input[0] == 'L') PrintLines();
                
                else Console.WriteLine("Invalid Input");

                Console.Write($"{_pointerIndex}> ");
                input = Console.ReadLine();
            }

            SaveFile();
        }

        private void SaveFile()
        {
            var filePath = @"D:\"+_originalFileName+".txt";
            LinkedList<string> SavedList = new LinkedList<string>();
            foreach (string line in _linkedList) SavedList.AddToTail(line);
           
            try
            {
                File.WriteAllLines(filePath, SavedList);
                Console.WriteLine("File Successfully Saved");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Drive not Accessible, File Not Saved");
                Console.ReadLine();
            }
        }

        private void DeleteLines(string input)
        {
            int n, m, o; // 2nd, first, placeholder
            string crudeInput = input.Remove(0, 2);
            string[] separatedInputs = crudeInput.Split(' ');
            o=m = Convert.ToInt32(separatedInputs[0]);
            if (separatedInputs.Length>1)
            {
                n = Convert.ToInt32(separatedInputs[1]);
                if (n <= m)
                    for (int i = 0; i < n; i++)
                        _linkedList.RemoveAt(m-1);
                else if (n > m)
                    for (int i = 0; i <= n-o; i++)
                        _linkedList.RemoveAt(m-1);
            }
            else _linkedList.RemoveAt(m-1);
            PrintLines();
        }

        private void OpenSavedFile(string fileName)
        {
            if (fileName.Length < 6) throw new Exception("Invalid FileName");
            _originalFileName=fileName = fileName.Remove(0, 5);
            string filePath = @"D:\" + fileName + ".txt";
            String[] SaveFile = null;
            try
            {
                SaveFile = File.ReadAllLines(filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine("File not Found");
            }
            foreach (string line in SaveFile) _linkedList.AddToTail(line);
            
            PrintLines();
        }

        private void InsertLines(string input)
        {
            int startingIndex = Convert.ToInt32(input.Remove(0, 2));
            if (startingIndex == 0) startingIndex = 1;
            Console.Write($"{startingIndex}> ");
            string insertInput = Console.ReadLine();
            while (insertInput != "L")
            {
                _linkedList.InsertAt(startingIndex - 1, insertInput);
                startingIndex++;
                Console.Write($"{startingIndex}> ");
                insertInput = Console.ReadLine();
            }

            PrintLines();
        }

        private void PrintLines()
        {
            int n = 1;
            foreach (string line in _linkedList)
            {
                Console.WriteLine($"{n}> {line}");
                n++;
            }
        }


    }
}