/*
 * @author Ekzaryan Daniil 
 * @2014
 * @website http://refwarlock.blogspot.ru 
 */
using System.Threading.Tasks;

namespace WhiteSpaceInterpretator
{
    interface IInterpretator
    {
        Task Execute(string source);
    }
}
