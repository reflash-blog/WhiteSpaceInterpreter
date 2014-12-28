/*
 * @author Ekzaryan Daniil 
 * @2014
 * @website http://refwarlock.blogspot.ru 
 */

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WhiteSpaceInterpretator.Model
{
    public class WhiteSpaceMemory
    {
        Stack<int> _stack = new Stack<int>();
        Stack<int> _callstack = new Stack<int>(); 
        Dictionary<int,int> heap = new Dictionary<int, int>();
        Collection<char> code = new Collection<char>();
        Dictionary<int, int> _labels = new Dictionary<int, int>(); 

        public WhiteSpaceMemory(string code)
        {
            FillCodeMemory(code);
        }

        private void FillCodeMemory(string codeString)
        {
            var i = 0;
            foreach (var opcode in codeString)
            {
                switch (opcode)
                {
                    case ' ':
                        this.code.Add('S');
                        break;
                    case '\t':
                        this.code.Add('T');
                        break;
                    case '\n':
                        this.code.Add('L');
                        break;
                    default:
                        break;
                }
                i++;
            }
        }

        public void AddToHeap(int position, int value)
        {
            heap[position] = value;
        }

        public int GetFromHeap(int position)
        {
            return heap[position];
        }

        public void Push(int value)
        {
            _stack.Push(value);
        }

        public int Pop()
        {
            return _stack.Pop();
        }

        public void AddLabel(int label, int position)
        {
            _labels[label] = position;
        }

        public int GetLabel(int label)
        {
            return _labels[label];
        }

        public int CallProgram(int label,int position)
        {
            var subprogramPosition = GetLabel(label);
            _callstack.Push(position+1);
            return subprogramPosition;
        }

        public int ReturnProgram()
        {
            return _callstack.Pop();
        }

        public Collection<char> GetCode()
        {
            return code;
        }
    }
}
