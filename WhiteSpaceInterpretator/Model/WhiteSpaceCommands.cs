/*
 * @author Ekzaryan Daniil 
 * @2014
 * @website http://refwarlock.blogspot.ru 
 */

using System;
using System.Collections.Generic;

namespace WhiteSpaceInterpretator.Model
{
    public class WhiteSpaceCommands
    {
    
    //Space-Space-Number : добавить число в стек
    //Space-LF-Space : продублировать число на вершине стека
    //Space-LF-Tab : поменять местами два верхних элемента стека
    //Space-LF-LF : извлечь из стека верхний элемент и выбросить его
    //Space-Tab-Space-Number : скопировать N-ый элемент стека (индекс задается аргументом) на вершину стека
    //Space-Tab-LF-Number : “сдвинуть” со стека N элементов, сохраняя при этом верхний

        public static void Space_Space_Number(WhiteSpaceMemory memory, int number)
        {
            memory.Push(number);
        }

        public static void Space_LF_Space(WhiteSpaceMemory memory)
        {
            var number = memory.Pop();
            memory.Push(number);
            memory.Push(number);
        }

        public static void Space_LF_Tab(WhiteSpaceMemory memory)
        {
            var number1 = memory.Pop();
            var number2 = memory.Pop();
            memory.Push(number1);
            memory.Push(number2);
        }

        public static void Space_LF_LF(WhiteSpaceMemory memory)
        {
            memory.Pop();
        }

        public static void Space_Tab_Space_Number(WhiteSpaceMemory memory, int number)
        {
            var tempStack = new Stack<int>();
            for (var i = 0; i < number; i++)
            {
                tempStack.Push(memory.Pop());
            }
            var nNumber = tempStack.Pop();
            memory.Push(nNumber);
            for (var i = 0; i < number-1; i++)
            {
                memory.Push(tempStack.Pop());
            }
            memory.Push(nNumber);
        }

        public static void Space_Tab_LF_Number(WhiteSpaceMemory memory, int number)
        {
            var tempStack = new Stack<int>();
            for (var i = 0; i < number; i++)
            {
                tempStack.Push(memory.Pop());
            }
            memory.Push(tempStack.Pop());
        }

    //        Tab-Space-Space-Space : сложение
    //        Tab-Space-Space-Tab : вычитание
    //        Tab-Space-Space-LF : умножение
    //        Tab-Space-Tab-Space : деление (целочисленное)
    //        Tab-Space-Tab-Tab : остаток от деления

        public static void Tab_Space_Space_Space(WhiteSpaceMemory memory)
        {
            var number1 = memory.Pop();
            var number2 = memory.Pop();
            number2 = number1 + number2;
            memory.Push(number2);
            memory.Push(number1);
        }
        public static void Tab_Space_Space_Tab(WhiteSpaceMemory memory)
        {
            var number1 = memory.Pop();
            var number2 = memory.Pop();
            number2 = number1 - number2;
            memory.Push(number2);
            memory.Push(number1);
        }
        public static void Tab_Space_Space_LF(WhiteSpaceMemory memory)
        {
            var number1 = memory.Pop();
            var number2 = memory.Pop();
            number2 = number1 * number2;
            memory.Push(number2);
            memory.Push(number1);
        }
        public static void Tab_Space_Tab_Space(WhiteSpaceMemory memory)
        {
            var number1 = memory.Pop();
            var number2 = memory.Pop();
            number2 = number1 / number2;
            memory.Push(number2);
            memory.Push(number1);
        }
        public static void Tab_Space_Tab_Tab(WhiteSpaceMemory memory)
        {
            var number1 = memory.Pop();
            var number2 = memory.Pop();
            number2 = number1 % number2;
            memory.Push(number2);
            memory.Push(number1);
        }

        
    //Tab-Tab-Space : сохранить верхний элемент стека в ячейке памяти, адрес которой задан во втором сверху элементе
    //Tab-Tab-Tab : извлечь содержимое ячейки памяти, адрес которой задан в верхнем элементе стека, и поместить его на вершину стека

        public static void Tab_Tab_Space(WhiteSpaceMemory memory)
        {
            var number = memory.Pop();
            var position = memory.Pop();
            memory.AddToHeap(position, number);
        }
        public static void Tab_Tab_Tab(WhiteSpaceMemory memory)
        {
            var position = memory.Pop();
            memory.Push(memory.GetFromHeap(position));
        }

        
    //LF-Space-Space-Label : создать метку
    //LF-Space-Tab-Label : вызвать подпрограмму
    //LF-Space-LF-Label : перейти к метке
    //LF-Tab-Space-Label : перейти к метке, если верхний элемент стека равен нулю
    //LF-Tab-Tab-Label : перейти к метке, если верхний элемент стека отрицателен
    //LF-Tab-LF : закончить подпрограмму и передать управление вызвавшей ее программе
    //LF-LF-LF : закончить программу
        public static void LF_Space_Space_Label(WhiteSpaceMemory memory, int label, int position)
        {
            memory.AddLabel(label,position);
        }
        public static int LF_Space_Tab_Label(WhiteSpaceMemory memory, int label, int position)
        {
            return memory.CallProgram(label,position);
        }

        public static int LF_Space_LF_Label(WhiteSpaceMemory memory, int label)
        {
            return memory.GetLabel(label);
        }

        public static int LF_Tab_Space_Label(WhiteSpaceMemory memory, int label, int position)
        {
            var topStackElement = memory.Pop();
            if (topStackElement == 0)
                position = memory.GetLabel(label);
            else
            {
                position++;
            }
            memory.Push(topStackElement);
            return position;
        }
        public static int LF_Tab_Tab_Label(WhiteSpaceMemory memory, int label, int position)
        {
            var topStackElement = memory.Pop();
            if (topStackElement < 0)
                position = memory.GetLabel(label);
            memory.Push(topStackElement);
            return position;
        }
        public static int LF_Tab_LF(WhiteSpaceMemory memory)
        {
            return memory.ReturnProgram();
        }
        public static void LF_LF_LF(WhiteSpaceMemory memory)
        {

        }

        
    //Tab-LF-Space-Space/Tab : вывести верхний элемент стека на печать как символ/число
    //Tab-LF-Tab-Space/Tab : прочитать из потока символ/число и сохранить его в ячейку памяти, адрес которой задан в верхнем элементе стека

        public static char Tab_LF_Space_Space(WhiteSpaceMemory memory)
        {
            var number = memory.Pop();
            var symbol = Convert.ToChar(number);
            memory.Push(number);
            return symbol;
        }
        public static int Tab_LF_Space_Tab(WhiteSpaceMemory memory)
        {
            var number = memory.Pop();
            memory.Push(number);
            return number;
        }
        public static void Tab_LF_Tab_Space(WhiteSpaceMemory memory, char symbol)
        {
            var number = Convert.ToInt32(symbol);
            var topStackElement = memory.Pop();
            memory.AddToHeap(topStackElement,number);
            memory.Push(topStackElement);
        }
        public static void Tab_LF_Tab_Tab(WhiteSpaceMemory memory, int number)
        {
            var topStackElement = memory.Pop();
            memory.AddToHeap(topStackElement, number);
            memory.Push(topStackElement);
        }
    }
}
