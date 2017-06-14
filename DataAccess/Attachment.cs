using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetmvc.DataAccess
{
    [Table("attachment", Schema = "public")]
    public class Attachment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int attachmentid { get; set; }

        public string filename { get; set; }
    }
}