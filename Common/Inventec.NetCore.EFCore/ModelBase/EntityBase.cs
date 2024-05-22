using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventec.NetCore.EFCore.ModelBase
{
    public abstract class EntityBase : IEntityBase
    {
        [Key]
        [Column("ID")]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Column("CREATE_TIME")]
        public long? CreateTime { get; set; } = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
        [Column("MODIFY_TIME")]
        public long? ModifyTime { get; set; }
        [Column("CREATOR")]
        public string Creator { get; set; }
        [Column("MODIFIER")]
        public string Modifier { get; set; }
        [Column("APP_CREATOR")]
        public string AppCreator { get; set; }
        [Column("APP_MODIFIER")]
        public string AppModifier { get; set; }
        [Column("IS_ACTIVE")]
        public bool? IsActive { get; set; } = true;
        [Column("IS_DELETE")]
        public bool? IsDelete { get; set; } = false;
        [Column("GROUP_CODE")]
        public string GroupCode { get; set; }
    }
}
