using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipAssignment.Interfaces
{
    public interface IFileOperations
    {
       string ReadFromFile(string key, int lineNumber);
        void WriteToFile(IEnumerable<string> output);
    }
}
