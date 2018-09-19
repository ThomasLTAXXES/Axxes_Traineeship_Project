using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Who.BL.Domain;

namespace Who.BL.IServices
{
    public interface IImageService
    {
        IEnumerable<Person> GetPeople();

        IEnumerable<Person> GetPeople(string name);
    }
}
