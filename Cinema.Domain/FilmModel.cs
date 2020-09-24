using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cinema.Domain.Identity;

namespace Cinema.Domain
{
    [Table("Films")]
    public class FilmModel
    {
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public long ReleaseYear { get; set; }

        public string Producer { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public UserModel User { get; set; }

        public string Poster { get; set; }

        [NotMapped]
        public byte[] File { get; set; }
    }
}
