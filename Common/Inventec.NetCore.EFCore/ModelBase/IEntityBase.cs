using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventec.NetCore.EFCore.ModelBase
{
    public interface IEntityBase
    {
        string Id { get; set; }
        long? CreateTime { get; set; }
        long? ModifyTime { get; set; }
        string Creator { get; set; }
        string Modifier { get; set; }
        string AppCreator { get; set; }
        string AppModifier { get; set; }
        bool? IsActive { get; set; }
        bool? IsDelete { get; set; }
        string GroupCode { get; set; }
    }
}
