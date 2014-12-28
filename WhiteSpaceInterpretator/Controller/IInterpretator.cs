/*
 * @author Ekzaryan Daniil 
 * @2014
 * @website http://refwarlock.blogspot.ru 
 */

using System.Threading.Tasks;

namespace WhiteSpaceInterpretator.Controller
{
    interface IInterpretator
    {
        Task Execute(string source);
    }
}
