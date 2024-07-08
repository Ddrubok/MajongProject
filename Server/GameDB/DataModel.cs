using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDB
{
    //영한 사전 vs 색인
    //clustered vs nonclustered

    [Table("Test")]
    public class TestDb
    {
        //Convention : [클래스]ID로 명명하면 PK
        public int TestDbId { get; set; }
        public string Name { get; set;}
    }

}
