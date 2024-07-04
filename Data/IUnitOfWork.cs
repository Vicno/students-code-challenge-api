using Data.Repository.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IUnitOfWork
    {
        IClassRepository ClassRepository {  get; }
        IStudentRepository StudentRepository { get; }
    }
}
