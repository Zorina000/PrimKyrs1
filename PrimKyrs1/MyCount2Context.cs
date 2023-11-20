using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Media;

namespace PrimKyrs1
{
     public class MyCount2Context : DbContext
    {
        public MyCount2Context()
        {
            Database.EnsureCreated(); //Создает базу с указанным названием
        }

        //Класс сущности
        public DbSet<Count> Countries { get; set; } = null!;

        //Класс отвечает за установку параметров подключения
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

            => optionsBuilder.UseSqlServer("Data Source=DESKTOP-G559HAC\\SQLEXPRESS;Initial Catalog=MyCount2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

     }
}
