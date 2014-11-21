using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WhiteSpaceInterpretator
{
    public class WhitespaceInterpretator : IInterpretator
    {
        enum Command
        {
            SS,SLS,SLT,SLL,STS,STL,
            TSSS,TSST,TSSL,TSTS,TSTT,
            TTS,TTT,
            LSS,LST,LSL,LTS,LTT,LTL,LLL,
            TLSS,TLST,TLTS,TLTT,
            NONE
        }

        public string input = "";
        public string output = "";
        public async Task Execute(string source)
        {
            await Task.Run(() =>
            {
                try
                {
                    var memory = new WhiteSpaceMemory(source);
                    var code = memory.GetCode();
                    var command = "";
                    var number = 0;
                    for (var i = 0; i < code.Count; i++)
                    {
                        command += code[i];
                        switch (CheckCommand(command))
                        {
                            case Command.SS:
                                number = 0;
                                i++;
                                CalculateNumber(code, ref i, ref number);
                                WhiteSpaceCommands.Space_Space_Number(memory, number);
                                command = "";
                                break;
                            case Command.SLS:
                                WhiteSpaceCommands.Space_LF_Space(memory);
                                command = "";
                                break;
                            case Command.SLT:
                                WhiteSpaceCommands.Space_LF_Tab(memory);
                                command = "";
                                break;
                            case Command.SLL:
                                WhiteSpaceCommands.Space_LF_LF(memory);
                                command = "";
                                break;
                            case Command.STS:
                                number = 0;
                                i++;
                                CalculateNumber(code, ref i, ref number);
                                WhiteSpaceCommands.Space_Tab_Space_Number(memory, number);
                                command = "";
                                break;
                            case Command.STL:
                                number = 0;
                                i++;
                                CalculateNumber(code, ref i, ref number);
                                WhiteSpaceCommands.Space_Tab_LF_Number(memory, number);
                                command = "";
                                break;
                            case Command.TSSS:
                                WhiteSpaceCommands.Tab_Space_Space_Space(memory);
                                command = "";
                                break;
                            case Command.TSST:
                                WhiteSpaceCommands.Tab_Space_Space_Tab(memory);
                                command = "";
                                break;
                            case Command.TSSL:
                                WhiteSpaceCommands.Tab_Space_Space_LF(memory);
                                command = "";
                                break;
                            case Command.TSTS:
                                WhiteSpaceCommands.Tab_Space_Tab_Space(memory);
                                command = "";
                                break;
                            case Command.TSTT:
                                WhiteSpaceCommands.Tab_Space_Tab_Tab(memory);
                                command = "";
                                break;
                            case Command.TTS:
                                WhiteSpaceCommands.Tab_Tab_Space(memory);
                                command = "";
                                break;
                            case Command.TTT:
                                WhiteSpaceCommands.Tab_Tab_Tab(memory);
                                command = "";
                                break;
                            case Command.LSS:
                                number = 0;
                                i++;
                                CalculateLabel(code, ref i, ref number);
                                WhiteSpaceCommands.LF_Space_Space_Label(memory, number, i);
                                command = "";
                                break;
                            case Command.LST:
                                number = 0;
                                i++;
                                CalculateLabel(code, ref i, ref number);
                                i = WhiteSpaceCommands.LF_Space_Tab_Label(memory, number, i);
                                i--;
                                command = "";
                                break;
                            case Command.LSL:
                                number = 0;
                                i++;
                                CalculateLabel(code, ref i, ref number);
                                i = WhiteSpaceCommands.LF_Space_LF_Label(memory, number);
                                i--;
                                command = "";
                                break;
                            case Command.LTS:
                                number = 0;
                                i++;
                                CalculateLabel(code, ref i, ref number);
                                number = WhiteSpaceCommands.LF_Tab_Space_Label(memory, number, i);
                                if (number != i)
                                {
                                    i = number;
                                    i--;
                                }
                                command = "";
                                break;
                            case Command.LTT:
                                number = 0;
                                i++;
                                CalculateLabel(code, ref i, ref number);
                                number = WhiteSpaceCommands.LF_Tab_Tab_Label(memory, number, i);
                                if (number != i)
                                {
                                    i = number;
                                    i--;
                                }
                                command = "";
                                break;
                            case Command.LTL:
                                i = WhiteSpaceCommands.LF_Tab_LF(memory);
                                i--;
                                command = "";
                                break;
                            case Command.LLL:
                                i = code.Count;
                                command = "";
                                break;
                            case Command.TLSS:
                                output += WhiteSpaceCommands.Tab_LF_Space_Space(memory);
                                command = "";
                                break;
                            case Command.TLST:
                                output += WhiteSpaceCommands.Tab_LF_Space_Tab(memory);
                                command = "";
                                break;
                            case Command.TLTS:
                                var inputWin = new InputWindow();
                                inputWin.ShowDialog();
                                input += inputWin.InputTextBox.Text;
                                WhiteSpaceCommands.Tab_LF_Tab_Space(memory,input[0]);
                                input = "";
                                command = "";
                                break;
                            case Command.TLTT:
                                inputWin = new InputWindow();
                                inputWin.ShowDialog();
                                input += inputWin.InputTextBox.Text;
                                WhiteSpaceCommands.Tab_LF_Tab_Tab(memory,Convert.ToInt32(input));
                                input = "";
                                command = "";
                                break;
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Ошибка в интерпретации");
                }
            });

        }

        private static void CalculateLabel(System.Collections.ObjectModel.Collection<char> code, ref int i, ref int number)
        {
            for (; ; i++)
            {
                if (code[i] !='L')   
                {number++;continue;}
                break; 
            }
        }

        private static void CalculateNumber(System.Collections.ObjectModel.Collection<char> code, ref int i, ref int number)
        {
            var sign = code[i] != 'S';
            var bits = new List<bool>();
            for (; ; i++)
            {
                if (code[i] != 'L')
                { bits.Add(code[i] != 'S');  continue; }
                break;
            }
            bits.Reverse();
            var bitArray = new BitArray(bits.ToArray());
       
            number = GetIntFromBitArray(bitArray);
            if (sign)
                number = -number;
        }
        private static int GetIntFromBitArray(BitArray bitArray)
        {

            if (bitArray.Length > 32)
                throw new ArgumentException("Argument length shall be at most 32 bits.");

            int[] array = new int[1];
            bitArray.CopyTo(array, 0);
            return array[0];

        }

        private Command CheckCommand(string command)
        {
            switch (command)
            {
                case "SS":
                    return Command.SS;
                case "SLS":
                    return Command.SLS;
                case "SLT":
                    return Command.SLT;
                case "SLL":
                    return Command.SLL;
                case "STS":
                    return Command.STS;
                case "STL":
                    return Command.STL;
                case "TSSS":
                    return Command.TSSS;
                case "TSST":
                    return Command.TSST;
                case "TSSL":
                    return Command.TSSL;
                case "TSTS":
                    return Command.TSTS;
                case "TSTT":
                    return Command.TSTT;
                case "TTS":
                    return Command.TTS;
                case "TTT":
                    return Command.TTT;
                case "LSS":
                    return Command.LSS;
                case "LST":
                    return Command.LST;
                case "LSL":
                    return Command.LSL;
                case "LTS":
                    return Command.LTS;
                case "LTT":
                    return Command.LTT;
                case "LTL":
                    return Command.LTL;
                case "LLL":
                    return Command.LLL;
                case "TLSS":
                    return Command.TLSS;
                case "TLST":
                    return Command.TLST;
                case "TLTS":
                    return Command.TLTS;
                case "TLTT":
                    return Command.TLTT;
                default:
                    return Command.NONE;
            }
        }
    }
}
