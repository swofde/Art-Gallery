using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Drawing;
using System.ComponentModel.DataAnnotations.Schema;

namespace bizzart_proj
{
    public class Artist
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime DeathDate { get; set; }
        public String BirthPlace { get; set; }
        public String Biography { get; set; }
        public DbSet<Picture> Pictures { get; set; }
    }

    public class Picture
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public String Genre { get; set; }
        public int ArtistId { get; set; }
        [ForeignKey("ArtistId")]
        public Artist Artist { get; set; }
        public DateTime WriteDate { get; set; }
        public int Price { get; set; }
        public Point Size { get; set; }
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
        public String PictureHistory { get; set; }
        
    }

    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Gallery
    {
        [Key]
        public int Id { get; set; }
        public DbSet<Department> Departments { get; set; }
    }

    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; }
        public String Name { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime EmploymentDate { get; set; }
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
        public string Password { get; set; }
    }

    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public override string ToString()
        {
            return RoleName;
        }
    }
}
