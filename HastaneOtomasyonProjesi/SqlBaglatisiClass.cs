using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace HastaneOtomasyonProjesi
{
    class SqlBaglatisiClass
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-CP3UBIA;Initial Catalog=HastaneProje;Integrated Security=True");
            baglan.Open();
            return baglan;   //baglanti(metot adı), baglan(sql verilerimizi tutan nesnemizin adı) Bu metotu kullanmanın amacı her formda kod yoğunluğunu azaltmaktır
        }
    }
}
