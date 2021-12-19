using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{

    [Table("Photos")] // we wont add add DbSet<> in the context class becuz this table is related to AppUser 
                      //so we use this attribute to give the name we want for the table in the database 
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}