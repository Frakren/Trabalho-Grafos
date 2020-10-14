using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Grafos
{
    class Program
    {
        static List<Node> charlist = new List<Node>();
        static List<Node> finalchar = null;
        static string phrase="";
        static string temp;
        static void Main(string[] args)
        {
            initList();
            phraseheGenerator();
            calculateOccurrence();
            finalchar = new List<Node>(charlist);
            Console.WriteLine(phrase);
            charlist.Sort((x, y) => x.freq.CompareTo(y.freq));
            makeHuffmanTree();
            huffmanCodeGenerator();
            showCodeHuffman();
            Decode();
            Console.WriteLine(temp);
            Console.ReadKey();
        }
        static void EncodeHuffman(Node node, int index, string temp)
        {
            temp += "";
            if (node == null)
            {
                return;
            }
            if (node.name.Equals(finalchar[index].name))
            {
                finalchar[index].cod = temp;
            }
            EncodeHuffman(node.left, index, temp + "0");
            EncodeHuffman(node.right, index, temp + "1");
        }
        static void Decode()
        {
            foreach(var p in phrase)
            {
                foreach(var item in finalchar)
                {
                    if (item.freq > 0){
                        if(item.name[0]==p)
                        {
                            temp += item.cod;
                        }
                    }
                }
            }

        }
        static void huffmanCodeGenerator()
        {
            for (int i = 0; i < finalchar.Count; i++)
            {
                if (finalchar[i].freq != 0)
                {
                    EncodeHuffman(charlist[0], i, "");
                }
            }
        }
        static void showCodeHuffman()
        {
            foreach (var item in finalchar)
            {
                Console.WriteLine(item.ToString());
            }
        }
        static void makeHuffmanTree()
        {
            while (charlist.Count > 1)
            {
                Node z = new Node(charlist[0].freq + charlist[1].freq, charlist[0], charlist[1]);
                z.binary[0] = 0;
                z.binary[1] = 1;
                charlist.Add(z);
                charlist.RemoveAt(0);
                charlist.RemoveAt(0);
                charlist.Sort((x, y) => x.freq.CompareTo(y.freq));
            }
        }
        static void calculateOccurrence()
        {
            for (int i = 0; i < charlist.Count; i++)
            {
                charlist[i].freq = (int)(((float)(Regex.Matches(phrase, charlist[i].name).Count) / phrase.Length) * 100);
            }
        }

        static void phraseheGenerator()
        {
            for (int i = 0; i < 5; i++)
            {
                phrase += (char)new Random().Next(65, 69);
            }
        }
        static void initList()
        {
            for (int i = 65; i < 69; i++)
            {
                string item = ((char)i).ToString();
                Node aux = new Node(item, 0, "");
                charlist.Add(aux);
            }
        }
    }
    public class Node
    {
        public int[] binary = new int[2] { -99, -99 };
        public string name = "Father";
        public float freq;
        public string cod = "";
        public Node left;
        public Node right;

        public Node(float freq, Node left, Node right)
        {
            this.freq = freq;
            this.left = left;
            this.right = right;
        }
        public bool checkIfHaveLeftSon()
        {
            return left != null;
        }
        public bool checkIfHaveRightSon()
        {
            return right != null;
        }
        public Node(string name, float freq, string cod)
        {
            this.name = name;
            this.freq = freq;
            this.cod = cod;
        }

        public override string ToString()
        {
            return name + "|" + freq.ToString() + "|" + cod;
        }
    }
}
